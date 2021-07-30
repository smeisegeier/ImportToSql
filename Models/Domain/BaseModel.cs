using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Models.Domain
{
    // https://www.entityframeworktutorial.net/efcore/create-model-for-existing-database-in-ef-core.aspx
    public abstract class BaseModel
    {
        /// <summary>
        /// Id is Db-only and must not come from json object.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Comparer for existance of item in table. Create fitting Hash for each class.
        /// </summary>
        public abstract string Hash { get; }

        public DateTime ImportTime { get; protected set; } = DateTime.Now;

        public static string PrintList<T>(List<T> list) where T : BaseModel =>
            string.Join(Environment.NewLine, list.Select(x => x.ToString()));
    }
}
