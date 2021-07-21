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

        public override string ToString() => string.Format("{0}-{1}-{2}", Id, City, Country);

        public static JsonSchema Schema = JsonSchema.FromType<Test>();
        
        public static string SchemaString = @"{
            'Id': {'type': 'string'},
            'City': {'type': 'string'},
            'Country': {'type': 'string'}
            }";

    }
}
