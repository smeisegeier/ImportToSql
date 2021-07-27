using Rki.ImportToSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.ViewModels
{
    public class DropDownItem
    {
        public string Name { get; set; }
        public Type TypeName { get; set; }
        public string IconPath { get; set; }

        public DropDownItem(string name, Type typeName, string iconPath = null)
        {
            Name = name;
            TypeName = typeName;
            IconPath = iconPath;
        }

        public static List<DropDownItem> GetDefaultValues() => new List<DropDownItem>()
        {
            new DropDownItem("Test01", typeof(Test1), "/resources/images/blau.png"),
            new DropDownItem("Test02", typeof(Test2),  "/resources/images/gelb.png"),
            new DropDownItem("Schema03", typeof(Schema03Anmeldungen),  "/resources/images/gruen.png")
        };
    }
}
