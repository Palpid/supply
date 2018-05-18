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

                    //    var bomDictionary = new Dictionary<Int64, Bom>();

                    //itemBom = connection.Query<Bom, BomDetail, Bom>(
                    //        Properties.Resources.QueryItemBom,
                    //        (head, line) =>
                    //        {
                    //            Bom bomEntry;

                    //            if (!bomDictionary.TryGetValue(head.Code, out bomEntry))
                    //            {
                    //                bomEntry = head;
                    //                head.Lines = new List<BomDetail>();
                    //                bomDictionary.Add(head.Code, bomEntry);
                    //            }

                    //            bomEntry.Lines.Add(line);
                    //            return bomEntry;
                    //        }, splitOn: nameof(BomLine.CodeBom))
                    //        .Distinct()
                    //        .ToList();

                    string sql = $"{Properties.Resources.QueryBomBreakdown};{Properties.Resources.QueryDetailItemsInBom};{Properties.Resources.QueryItemBom}";

                    //IEnumerable<Bom> bom = null;
                    var bomDictionary = new Dictionary<Int64, Bom>();

                    using (var multi = connection.QueryMultiple(sql, new { item = itemCode }))
                    {
                        var breakdown = multi.Read<BomBreakdown>().ToList();
                        var detailItemList = multi.Read<OitmExt>().ToList();

                        itemBom = multi.Read<Bom, BomDetail, Bom>((head, line) =>
                            {
                                Bom bomEntry;

                                if (!bomDictionary.TryGetValue(head.Code, out bomEntry))
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
    }
}
