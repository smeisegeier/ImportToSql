using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Rki.ImportToSql.Services;
using System;
using System.Collections.Generic;

#nullable disable

namespace Rki.ImportToSql.Models
{
    public partial class Schema03Anmeldungen : BaseModel
    {
        public string Dea_Id { get; set; }
        public string Plz { get; set; }
        public string Name { get; set; }
        public int Statuswert_Id { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string Kommentar { get; set; }

        public override string Hash => Dea_Id;

        public static RepoSchema03 Repo { get; set; } = new();

        //* experimental */
        public static JSchema Schema = new JSchemaGenerator().Generate(typeof(List<Schema03Anmeldungen>));


        /*
         {
  "type": "array",
  "items": {
    "type": [
      "object",
      "null"
    ],
    "properties": {
      "Dea_Id": {
        "type": [
          "string",
          "null"
        ]
      },
      "Plz": {
        "type": [
          "string",
          "null"
        ]
      },
      "Name": {
        "type": [
          "string",
          "null"
        ]
      },
      "Statuswert_Id": {
        "type": "integer"
      },
      "ModifiedBy": {
        "type": [
          "string",
          "null"
        ]
      },
      "ModifiedAt": {
        "type": "string",
        "format": "date-time"
      },
      "Kommentar": {
        "type": [
          "string",
          "null"
        ]
      },
      "Hash": {
        "type": [
          "string",
          "null"
        ]
      },
      "Id": {
        "type": "integer"
      }
    },
    "required": [
      "Dea_Id",
      "Plz",
      "Name",
      "Statuswert_Id",
      "ModifiedBy",
      "ModifiedAt",
      "Kommentar",
      "Hash",
      "Id"
    ]
  }
}
         
         */
    }
}
