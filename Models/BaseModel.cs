using NJsonSchema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Models
{
    public abstract class BaseModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        // HACK create better hash
        [JsonIgnore]
        public abstract string Hash { get; }

        public static string PrintList<T>(List<T> list) where T : BaseModel =>
            string.Join(Environment.NewLine, list.Select(x => x.ToString()));
    }
}
