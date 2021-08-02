using System;
using System.Collections.Generic;

#nullable disable

namespace Rki.ImportToSql.Models.Domain
{
    public class GsProzessdaten : BaseModel
    {
        public int Anr { get; set; }
        public int? KitaId { get; set; }
        public int? HaushaltId { get; set; }
        public int? Geburtsmonat { get; set; }
        public int? Geburtsjahr { get; set; }
        public int? Geschlecht { get; set; }
        public int? Fb3Stat { get; set; }
        public int? Fb7Stat { get; set; }
        public int? Einwilligung { get; set; }
        public int? Mna { get; set; }
        public int? Speichel { get; set; }
        public int? Blut { get; set; }
        public int? GaNcoV { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? Hb1Datum { get; set; }
        public DateTime? Hb2Datum { get; set; }
        public bool? Hb1Status { get; set; }
        public bool? Hb2Status { get; set; }
        public string Ort { get; set; }
        public string Ortsteil { get; set; }
        public string Plz { get; set; }
        public int? FarbeId { get; set; }
        public int? Rolle { get; set; }
        public DateTime? Fb3Datum { get; set; }
        public DateTime? Fb7Datum { get; set; }
        public bool IstIndexperson { get; set; }
        public bool IstAnkerperson { get; set; }
        public int? Symptomtagebuch { get; set; }
        public int? Qna { get; set; }

        public override string Hash => string.Format("{0}_{1}_{2}",Anr,KitaId,HaushaltId);

    }
}
