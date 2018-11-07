using BOM.Classes;
using BOM.General;
using BOM.Models;
using BOM.Services.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM.Services.Implementations
{
    public class BomDapper : IBomService
    {
        public List<BomBreakdown> GetBromBreakdown()
        {
            try
            {
                List<BomBreakdown> bomBreakdown;

                using (var connection = new SqlConnection(GlobalSetting.ConnectionString))
                {
                    connection.Open();

                    bomBreakdown = connection.Query<BomBreakdown>(Properties.Resources.QueryBomBreakdown).ToList();
                }
                return bomBreakdown;
            }
            catch
            {
                throw;
            }
        }

        public List<Bom> GetItemBom(string itemCode)
        {
            try
            {
                List<Bom> itemBom;

                using (var connection = new SqlConnection(GlobalSetting.ConnectionString))
                {
                    connection.Open();

                    string sql = $"{Properties.Resources.QueryBomBreakdown};{Properties.Resources.QueryDetailItemsInBom};{Properties.Resources.QueryItemBom}";

                    //IEnumerable<Bom> bom = null;
                    var bomDictionary = new Dictionary<Int64, Bom>();

                    using (var multi = connection.QueryMultiple(sql, new { item = itemCode }))
                    {
                        var breakdown = multi.Read<BomBreakdown>().ToList();
                        var detailItemList = multi.Read<OitmExt>().ToList();

                        itemBom = multi.Read<Bom, BomDetail, Bom>((head, line) =>
                            {
                                if (!bomDictionary.TryGetValue(head.Code, out Bom bomEntry))
                                {
                                    bomEntry = head;
                                    head.Lines = new List<BomDetail>();
                                    bomDictionary.Add(head.Code, bomEntry);
                                }

                                line.Breakdown = breakdown.Where(b => b.IdBomBreakdown == line.BomBreakdown).FirstOrDefault();
                                line.Item = detailItemList.Where(i => i.ItemCode == line.ItemCode).FirstOrDefault();

                                bomEntry.Lines.Add(line);
                                return bomEntry;
                            }, splitOn: nameof(BomLine.CodeBom))
                            .Distinct()
                            .ToList();
                    }

                }
                return itemBom;
            }
            catch
            {
                throw;
            }
        }

        public bool EditBom(List<Bom> itemBoms)
        {
            try
            {
                using (var connection = new SqlConnection(GlobalSetting.ConnectionString))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            foreach(Bom bom in itemBoms)
                            {
                                if (bom.Code == 0)
                                    InsertItemBom(connection, transaction, bom);
                                else
                                    UpdateBom(connection, transaction, bom);
                            }

                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        public bool ImportBom(List<BomImportTmp> bomImportRows)
        {
            try
            {
                using (var connection = new SqlConnection(GlobalSetting.ConnectionString))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            foreach(BomImportTmp row in bomImportRows)
                            {
                                //Insertamos toda la lectura del fichero en la tabla temporal

                                connection.Execute(Properties.Resources.InsertBomImportTmp, new
                                {
                                    importGUID = row.ImportGUID,
                                    factory = row.Factory,
                                    itemCode = row.ItemCode,
                                    componentCode = row.ComponentCode,
                                    bomBreakdown = row.BomBreakdown,
                                    length = row.Length,
                                    width = row.Width,
                                    height = row.Height,
                                    density = row.Density,
                                    numberOfParts = row.NumberOfParts,
                                    coefficient1 = row.Coefficient1,
                                    coefficient2 = row.Coefficient2,
                                    scrap = row.Scrap,
                                    quantity = row.Quantity
                                }, transaction: transaction);
                            }

                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }

                    //Una vez insertados los registros, llamamos al stored procedure para procesarlos (el sp ya tiene su propia transacción)
                    string sp = "ETN_sp_BOM_IMPORT";
                    string imporGuid = bomImportRows.Select(a => a.ImportGUID).FirstOrDefault();

                    connection.Execute(sql: sp,
                        param: new { guid = imporGuid, user = GlobalSetting.UserLogged},
                        commandType: System.Data.CommandType.StoredProcedure,
                        commandTimeout: 360);

                }

                return true;
            }
            catch
            {
                throw;
            }
        }

        public List<BomImportTmp> GetImportBomByGuid(string guid)
        {
            try
            {
                List<BomImportTmp> importTmp;

                using (var connection = new SqlConnection(GlobalSetting.ConnectionString))
                {
                    connection.Open();
                    importTmp = connection.Query<BomImportTmp>(Properties.Resources.QueryImportBomTmp, new { importGUID = guid }).ToList();
                }

                    return importTmp;
            }
            catch
            {
                throw;
            }
        }

        public int MassiveItemChange(string originalItemCode, string changeItemcode)
        {
            try
            {
                int bomsModified = 0;

                using(var connection = new SqlConnection(GlobalSetting.ConnectionString))
                {
                    connection.Open();

                    //string sp = "EXECUTE [ETN_sp_BOM_MASSIVE_UPDATE] @originalItem,@changeTo,@user";
                    bomsModified = connection
                        .Query<int>(sql: Properties.Resources.ExecBomMassiveUpdate
                        , param: new {
                            originalItem = originalItemCode,
                            changeTo = changeItemcode,
                            user = GlobalSetting.UserLogged })
                        .FirstOrDefault();
                }

                return bomsModified;
            }
            catch
            {
                throw;
            }
        }

        public int MassiveItemChangeFromBomList(string bomCodes, string originalItemCode, string changeItemcode)
        {
            try
            {
                int bomsModified = 0;

                using (var connection = new SqlConnection(GlobalSetting.ConnectionString))
                {
                    connection.Open();

                    bomsModified = connection
                        .Query<int>(sql: Properties.Resources.ExecListBomMassiveUpdate
                        , param: new
                        {
                            bomIdList = bomCodes,
                            originalItem = originalItemCode,
                            changeTo = changeItemcode,
                            user = GlobalSetting.UserLogged
                        })
                        .FirstOrDefault();
                }

                return bomsModified;
            }
            catch
            {
                throw;
            }
        }

        public List<BomHeadExt> GetComponentBom(string itemCodeComponent)
        {
            try
            {
                List<BomHeadExt> bomList;

                using (var connection = new SqlConnection(GlobalSetting.ConnectionString))
                {
                    connection.Open();

                    bomList = connection.Query<BomHeadExt, Ocrd, BomHeadExt>(
                        Properties.Resources.QueryBomComponent, 
                        map:(bomHeadExt, ocrd) =>
                        {
                            bomHeadExt.FactoryDet = ocrd;
                            return bomHeadExt;
                        }, 
                        splitOn: nameof(Ocrd.CardCode),
                        param: new { itemCode = itemCodeComponent })
                        .Distinct()
                        .ToList();
                }

                return bomList;
            }
            catch
            {
                throw;
            }
        }

        #region Private Methods

        private void InsertItemBom(SqlConnection connection, SqlTransaction transaction, Bom bom)
        {
            try
            {
                Int64 code = connection.Query<Int64>(Properties.Resources.InsertBomHead, new { itemCode = bom.ItemCode, factory = bom.Factory }, transaction: transaction).First();
                bom.Code = code;
                InsertBomLines(connection, transaction, bom);
            }
            catch
            {
                throw;
            }
        }

        private void UpdateBom(SqlConnection connection, SqlTransaction transaction, Bom bom)
        {
            try
            {
                connection.Execute(Properties.Resources.UpdateBomHead, new { codeBom = bom.Code}, transaction: transaction);
                connection.Execute(Properties.Resources.DeleteBomLines, new { codeBom = bom.Code }, transaction: transaction);
                InsertBomLines(connection, transaction, bom);
            }
            catch
            {
                throw;
            }
        }

        private void InsertBomLines(SqlConnection connection, SqlTransaction transaction, Bom bom)
        {
            try
            {
                foreach (BomDetail line in bom.Lines)
                {
                    connection.Execute(Properties.Resources.InsertBomLines, new
                    {
                        codeBom = bom.Code,
                        itemCode = line.ItemCode,
                        bomBreakdown = line.BomBreakdown,
                        length = line.Length,
                        width = line.Width,
                        height = line.Height,
                        density = line.Density,
                        numberOfParts = line.NumberOfParts,
                        coefficient1 = line.Coefficient1,
                        coefficient2 = line.Coefficient2,
                        scrap = line.Scrap,
                        quantity = line.Quantity
                    }, transaction: transaction);
                }

                //Log
                connection.Execute(Properties.Resources.InsertBomLog, new { codeBom = bom.Code, user = GlobalSetting.UserLogged }, transaction: transaction);
            }
            catch
            {
                throw;
            }
        }

       

        #endregion
    }
}
