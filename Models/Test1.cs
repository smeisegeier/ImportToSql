using NJsonSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Models
{
    public class Test1 : Base
    {
        public override string Message => "test1 here";

        public string Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Test1(string id, string city, string country)
        {
            Id = id;
            City = city;
            Country = country;
        }

        public override string ToString() => string.Format("{0}-{1}-{2}", Id, City, Country);

        public static List<Test1> GetDefaultValues() => new List<Test1>()
        {
            new Test1("9","ny","amercas"),
        };



        /* Experimental */
        public static JsonSchema Schema = JsonSchema.FromType<Test1>();
        public static string SchemaString = @"{
            'Id': {'type': 'string'},
            'City': {'type': 'string'},
            'Country': {'type': 'string'}
            }";
    }
}
