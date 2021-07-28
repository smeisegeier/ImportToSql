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
        public string City { get; set; }
        public string Country { get; set; }

        public override string Hash => City + Country;


        public Test1(string city, string country)
        {
            City = city;
            Country = country;
        }

        public override string ToString() => string.Format("{0}-{1}-{2}", Id, City, Country);

        public static List<Test1> GetDefaultValues() => new List<Test1>()
        {
            new Test1("ny","amercas"),
            new Test1("berlin","germ")
        };

        public static RepoTest1 Repo { get; set; } = new();




        /* Experimental, requires NJsonSchema*/
        //public static JsonSchema Schema = JsonSchema.FromType<Test1>();
        //public static string SchemaString = @"{
        //    'Id': {'type': 'string'},
        //    'City': {'type': 'string'},
        //    'Country': {'type': 'string'}
        //    }";


    }
}
