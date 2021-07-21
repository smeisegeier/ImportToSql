using NJsonSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Models
{
    public class TestList
    {
        public List<Test> Tests { get; set; }

        public static JsonSchema Schema = JsonSchema.FromType<TestList>();
    }
}
