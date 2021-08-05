using CsvHelper.Configuration;
using Rki.ImportToSql.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Models.Domain
{
    public class Test1 : BaseModel
    {
        public static RepoTest1 SingletonRepo { get; } = new RepoTest1();

        public string City { get; set; }
        public string Country { get; set; }

        public override string Hash => City + Country;

        public Test1() { }

        public Test1(string city, string country)
        {
            City = city;
            Country = country;
        }

        public static List<Test1> GetDefaultValues() => new List<Test1>()
        {
            new Test1("ny","amercas"),
            new Test1("berlin","germ")
        };

    }


    public class Test1Map : ClassMap<Test1>
    {
        public Test1Map()
        {
            Map(m => m.City);
            Map(m => m.Country);
        }
    }

}
