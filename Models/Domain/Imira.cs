using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Rki.ImportToSql.Models.Domain
{
    public class Imira : BaseModel
    {
        public override string Hash => Lfdnr.ToString();

        public int Lfdnr { get; set; }
        public int Tranche { get; set; }
        public int Point { get; set; }
        public string Titel { get; set; }
        public string Name { get; set; }

        public string Vorname { get; set; }
        public string Strasse_HNR { get; set; }
        public string Adresszusatz { get; set; }
        public string Sex { get; set; }

        // DateTime
        public string GebDat { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string Ortsteil { get; set; }
        public string Geburtsland_original { get; set; }
        public string Geburtsland { get; set; }
        public string Geburtsort { get; set; }
        public string Familienstand { get; set; }
        public string Staat1_original { get; set; }
        public string Staat1 { get; set; }
        public string Staat2_original { get; set; }
        public string Staat2 { get; set; }
        public string Staat3_original { get; set; }
        public string Staat3 { get; set; }
        public string Staat_agg { get; set; }
        public int Sperrvermerk { get; set; }
        public int Gebdat_gesetzt { get; set; }

    }

    public class ImiraMap : ClassMap<Imira>
    {
        public ImiraMap()
        {
            Map(m => m.Adresszusatz);
            Map(m => m.Familienstand);
            Map(m => m.GebDat);
            Map(m => m.Gebdat_gesetzt);
            Map(m => m.Geburtsland);
            Map(m => m.Geburtsland_original);
            Map(m => m.Geburtsort);
            Map(m => m.Lfdnr);
            Map(m => m.Name);
            Map(m => m.Ort);
            Map(m => m.Ortsteil);
            Map(m => m.PLZ);
            Map(m => m.Point);
            Map(m => m.Sex);
            Map(m => m.Sperrvermerk);
            Map(m => m.Staat1);
            Map(m => m.Staat1_original);
            Map(m => m.Staat2);
            Map(m => m.Staat2_original);
            Map(m => m.Staat3);
            Map(m => m.Staat3_original);
            Map(m => m.Staat_agg);
            Map(m => m.Strasse_HNR);
            Map(m => m.Titel);
            Map(m => m.Tranche).Validate(f =>
            {
                int x = int.Parse(f.Field);
                return (x >= 1 && x <= 2);
            });

            Map(m => m.Vorname).Validate(f => !string.IsNullOrEmpty(f.Field.ToString()));
        }
    }
}
