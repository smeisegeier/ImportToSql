using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rki.ImportToSql.Models.Dto
{
    public class Schema03Dto
    {
        public string Dea_Id { get; set; }
        public string Plz { get; set; }
        public string Name { get; set; }
        public string Statuswert_Id { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string Kommentar { get; set; }

        public static JSchema Schema { get; } = new JSchemaGenerator().Generate(typeof(List<Schema03Dto>));
    }
}
