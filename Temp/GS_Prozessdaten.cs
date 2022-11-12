using System;
using System.Collections.Generic;

#nullable disable

namespace Rki.ImportToSql.Temp
{
    public partial class GS_Prozessdaten
    {
        public int id { get; set; }
        public int ANR { get; set; }
        public int? KitaId { get; set; }
        public int? HaushaltId { get; set; }
        public int? Geburtsmonat { get; set; }
        public int? Geburtsjahr { get; set; }
        public int? Geschlecht { get; set; }
        public int? FB3_stat { get; set; }
        public int? FB7_stat { get; set; }
        public int? Einwilligung { get; set; }
        public int? MNA { get; set; }
        public int? Speichel { get; set; }
        public int? Blut { get; set; }
        public int? GA_NCoV { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? HB1_Datum { get; set; }
        public DateTime? HB2_Datum { get; set; }
        public bool? HB1_Status { get; set; }
        public bool? HB2_Status { get; set; }
        public string Ort { get; set; }
        public string Ortsteil { get; set; }
        public string PLZ { get; set; }
        public int? FarbeId { get; set; }
        public int? Rolle { get; set; }
        public DateTime? FB3_Datum { get; set; }
        public DateTime? FB7_Datum { get; set; }
        public bool IstIndexperson { get; set; }
        public bool IstAnkerperson { get; set; }
        public int? Symptomtagebuch { get; set; }
        public int? QNA { get; set; }
    }
}
