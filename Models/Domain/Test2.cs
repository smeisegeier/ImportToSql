using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Rki.ImportToSql.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Models.Domain
{
    public class Test2 : BaseModel
    {
        public string Text { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        public override string Hash => Text + Description + Rating.ToString();

        public Test2(string text, string description, int rating)
        {
            Text = text;
            Description = description;
            Rating = rating;
        }

        /// <summary>
        /// ctor needed for csvhelper
        /// </summary>
        public Test2() { }

        public override string ToString() => string.Format("{0}-{1}-{2}", Id, Text, Description);

        public static List<Test2> GetDefaultValues() => new List<Test2>()
        {
            new Test2("olol","very lol",5),
            new Test2("xxde","..",2),
            new Test2("hi","hi there",8)
        };
    }

    public class Test2Map : ClassMap<Test2>
    {
        public Test2Map()
        {
            Map(m => m.Text);
            Map(m => m.Description);
            Map(m => m.Rating);
        }
    }
}
