using BOM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOM.Classes
{
    public class Supplier : Ocrd
    {
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(CardFName) == false)
                    return CardFName;
                else
                    return CardName;
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
