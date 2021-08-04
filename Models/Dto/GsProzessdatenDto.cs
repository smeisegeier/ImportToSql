using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using System.Collections.Generic;

#nullable disable

namespace Rki.ImportToSql.Models.Dto
{
    public class GsProzessdatenDto
    {
        public string ANR { get; set; }
        public string KitaId { get; set; }
        public string HaushaltId { get; set; }
        public string Geburtsmonat { get; set; }
        public string Geburtsjahr { get; set; }
        public string Geschlecht { get; set; }
        public string FB3_stat { get; set; }
        public string FB7_stat { get; set; }
        public string Einwilligung { get; set; }
        public string MNA { get; set; }
        public string Speichel { get; set; }
        public string Blut { get; set; }
        public string GA_NCoV { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? HB1_Datum { get; set; }
        public DateTime? HB2_Datum { get; set; }
        public bool? HB1_Status { get; set; }
        public bool? HB2_Status { get; set; }
        public string Ort { get; set; }
        public string Ortsteil { get; set; }
        public string PLZ { get; set; }
        public string FarbeId { get; set; }
        public string Rolle { get; set; }
        public DateTime? FB3_Datum { get; set; }
        public DateTime? FB7_Datum { get; set; }
        public bool IstIndexperson { get; set; }
        public bool IstAnkerperson { get; set; }
        public string Symptomtagebuch { get; set; }
        public string QNA { get; set; }

    }
}
