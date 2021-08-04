
namespace Rki.ImportToSql.ViewModels
{
    public class DropDownItem
    {
        public string Name { get; set; }
        public string IconPath { get; set; }

        public DropDownItem(string name, string iconPath = null)
        {
            Name = name;
            IconPath = iconPath;
        }
    }
}
