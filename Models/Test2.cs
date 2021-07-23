using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Models
{
    public class Test2 : Base
    {
        public override string Message => "hi im test2";

        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }

        public Test2(int id, string text, string description, int rating)
        {
            Id = id;
            Text = text;
            Description = description;
            Rating = rating;
        }

        public override string ToString() => string.Format("{0}-{1}-{2}", Id, Text, Description);

        public static List<Test2> GetDefaultValues() => new List<Test2>()
        {
            new Test2(1000,"olol","very lol",5),
            new Test2(2000,"xxde","..",2),
            new Test2(3000,"hi","hi there",8)
        };
    }
}
