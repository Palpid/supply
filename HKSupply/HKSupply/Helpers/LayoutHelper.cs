using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Utils.Serializing;
using HKSupply.General;

namespace HKSupply.Helpers
{
    public class LayoutHelper
    {
        public static string SaveLayoutAsBase64String(ISupportXtraSerializer source)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    source.SaveLayoutToStream(ms);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
            catch
            {
                throw;
            }
            
        }

        public static void RestoreLayoutFromBase64String(ISupportXtraSerializer target, string base64String)
        {
            try
            {
                byte[] rawData = Convert.FromBase64String(base64String);
                using (MemoryStream ms = new MemoryStream(rawData))
                {
                    target.RestoreLayoutFromStream(ms);
                }
            }
            catch
            {
                throw;
            }
        }

        public static string GetObjectLayout(List<Models.Layout> layouts ,string layoutName, string objectName)
        {
            try
            {
                string layout = layouts
                    .Where(a => a.LayoutName.Equals(layoutName) && a.ObjectName.Equals(objectName))
                    .Select(a => a.LayoutString).FirstOrDefault();

                return layout;
            }
            catch
            {
                throw;
            }
        }

        public static List<Models.Layout> GetFormLayouts(string formName)
        {
            try
            {
                return GlobalSetting.LayoutService.GetLayouts(
                    GlobalSetting.FunctionalitiesRoles.Where(fr => fr.Functionality.FormName.Equals(formName)).Select(a => a.FunctionalityId).FirstOrDefault(),
                    GlobalSetting.LoggedUser.UserLogin).ToList();
            }
            catch
            {
                throw;
            }
        }

        public static List<Models.Layout> GetRibbonWorkSpaceLayouts(string formName, string workSpaceMenuItemName)
        {
            try
            {
                return GlobalSetting.LayoutService.GetLayouts(
                    GlobalSetting.FunctionalitiesRoles.Where(fr => fr.Functionality.FormName.Equals(formName)).Select(a => a.FunctionalityId).FirstOrDefault(),
                    workSpaceMenuItemName,
                    GlobalSetting.LoggedUser.UserLogin)
                    .ToList();
            }
            catch
            {
                throw;
            }
        }

    }
}
