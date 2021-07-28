﻿using Rki.ImportToSql.Models;
using Rki.ImportToSql.Models.Domain;
using Rki.ImportToSql.Models.Dto;
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
        public string IconPath { get; set; }

        public DropDownItem(string name, string iconPath = null)
        {
            Name = name;
            IconPath = iconPath;
        }

        //public static IEnumerable<DropDownItem> GetDefaultValues() => new List<DropDownItem>()
        //{
        //    new DropDownItem("Test01", typeof(Test1), "/resources/images/blau.png"),
        //    new DropDownItem("Test02", typeof(Test2),  "/resources/images/gelb.png"),
        //    new DropDownItem("Schema03", typeof(Schema03Dto),  "/resources/images/gruen.png")
        //};
    }
}
