using NJsonSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Models
{
    public class Test
    {
        public string Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Test(string id, string city, string country)
        {
            Id = id;
            City = city;
            Country = country;
        }

        public override string ToString() => string.Format("{0}-{1}-{2}", Id, City, Country);

        public static JsonSchema Schema = JsonSchema.FromType<Test>();

        public static string SchemaString = @"{
            'Id': {'type': 'string'},
            'City': {'type': 'string'},
            'Country': {'type': 'string'}
            }";
        public static List<Test> GetDefaultValues() => new List<Test>()
        {
            new Test("1","london","uk"),
            new Test("2","springfield","sim"),
            new Test("1","london","uk")
        };

    }
}
