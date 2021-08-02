using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;

#nullable disable

namespace Rki.ImportToSql.Models.Dto
{
    public class GsProzessdatenDto
    {
        public string Anr { get; set; }
        public string KitaId { get; set; }
        public string HaushaltId { get; set; }
        public string Geburtsmonat { get; set; }
        public string Geburtsjahr { get; set; }
        public string Geschlecht { get; set; }
        public string Fb3Stat { get; set; }
        public string Fb7Stat { get; set; }
        public string Einwilligung { get; set; }
        public string Mna { get; set; }
        public string Speichel { get; set; }
        public string Blut { get; set; }
        public string GaNcoV { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? Hb1Datum { get; set; }
        public DateTime? Hb2Datum { get; set; }
        public bool? Hb1Status { get; set; }
        public bool? Hb2Status { get; set; }
        public string Ort { get; set; }
        public string Ortsteil { get; set; }
        public string Plz { get; set; }
        public string FarbeId { get; set; }
        public string Rolle { get; set; }
        public DateTime? Fb3Datum { get; set; }
        public DateTime? Fb7Datum { get; set; }
        public bool IstIndexperson { get; set; }
        public bool IstAnkerperson { get; set; }
        public string Symptomtagebuch { get; set; }
        public string Qna { get; set; }

        public static JSchema Schema { get; } = new JSchemaGenerator().Generate(typeof(List<GsProzessdatenDto>));
    }
}
