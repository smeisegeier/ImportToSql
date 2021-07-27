using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Models
{
    // https://www.entityframeworktutorial.net/efcore/create-model-for-existing-database-in-ef-core.aspx
    public abstract class BaseModel
    {
        // TODO wird Id erkannt im csv?
        /// <summary>
        /// Id is Db-only and must not come from json object.
        /// </summary>
        [JsonIgnore]
        public int Id { get; set; }

        /// <summary>
        /// Comparer for existance of item in table. Create fitting Hash for each class.
        /// </summary>
        [JsonIgnore]
        public abstract string Hash { get; }

        public static string PrintList<T>(List<T> list) where T : BaseModel =>
            string.Join(Environment.NewLine, list.Select(x => x.ToString()));

    }
}
