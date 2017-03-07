using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace HKSupply.Helpers
{
    public static class CustomExtensions
    {

        public static T Clone<T>(this T source)
        {
            var dcs = new DataContractSerializer(typeof(T));
            using (var ms = new System.IO.MemoryStream())
            {
                dcs.WriteObject(ms, source);
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                return (T)dcs.ReadObject(ms);
            }
        }
    }
}
