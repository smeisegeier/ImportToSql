using Rki.ImportToSql.Models.Dto;
using System;
using System.Collections.Generic;

#nullable disable

namespace Rki.ImportToSql.Models.Domain
{
    public class GsProzessdaten : BaseModel
    {
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

        public override string Hash => string.Format("{0}_{1}_{2}",ANR,KitaId,HaushaltId);

        /// <summary>
        /// Mapping domain <- dto
        /// </summary>
        /// <param name="i">dto</param>
        public static implicit operator GsProzessdaten(GsProzessdatenDto i)
        {
            // TODO bug: tryParse fails on nullable props
            if (!int.TryParse(i.ANR, out int anr))
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo("int vs string xD");
                return null;
            }
            if (!int.TryParse(i.KitaId, out int kitaId))
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo("int vs string xD");
                return null;
            }
            if (!int.TryParse(i.HaushaltId, out int haushaltId))
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo("int vs string xD");
                return null;
            }
            if (!int.TryParse(i.Geburtsmonat, out int gebmon))
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo("int vs string xD");
                return null;
            }
            if (!int.TryParse(i.Geburtsjahr, out int gebjahr))
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo("int vs string xD");
                return null;
            }
            if (!int.TryParse(i.Geschlecht, out int geschlecht))
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo("int vs string xD");
                return null;
            }
            if (!int.TryParse(i.FB3_stat, out int fb3Stat))
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo("int vs string xD");
                return null;
            }
            if (!int.TryParse(i.FB7_stat, out int fb7Stat))
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo("int vs string xD");
                return null;
            }
            if (!int.TryParse(i.Einwilligung, out int einwilligung))
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo("int vs string xD");
                return null;
            }
            if (!int.TryParse(i.MNA, out int mna))
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo("int vs string xD");
                return null;
            }
            if (!int.TryParse(i.Speichel, out int speichel))
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo("int vs string xD");
                return null;
            }
            if (!int.TryParse(i.Blut, out int blut))
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo("int vs string xD");
                return null;
            }
            if (!int.TryParse(i.GA_NCoV, out int gancov))
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo("int vs string xD");
                return null;
            }
            if (!int.TryParse(i.FarbeId, out int farbeId))
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo("int vs string xD");
                return null;
            }
            if (!int.TryParse(i.Rolle, out int rolle))
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo("int vs string xD");
                return null;
            }
            if (!int.TryParse(i.Symptomtagebuch, out int symptom))
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo("int vs string xD");
                return null;
            }
            if (!int.TryParse(i.QNA, out int qna))
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo("int vs string xD");
                return null;
            }


            return new GsProzessdaten()
            {
                ANR = anr,
                Blut = blut,
                CreatedAt = i.CreatedAt,
                Einwilligung = einwilligung,
                FarbeId = farbeId,
                FB3_Datum = i.FB3_Datum,
                FB3_stat = fb3Stat,
                FB7_Datum = i.FB7_Datum,
                FB7_stat = fb7Stat,
                GA_NCoV = gancov,
                Geburtsjahr = gebjahr,
                Geburtsmonat = gebmon,
                Geschlecht = geschlecht,
                HaushaltId = haushaltId,
                HB1_Datum = i.HB1_Datum,
                HB1_Status = i.HB1_Status,
                HB2_Datum = i.HB2_Datum,
                HB2_Status = i.HB2_Status,
                IstAnkerperson = i.IstAnkerperson,
                IstIndexperson = i.IstIndexperson,
                KitaId = kitaId,
                MNA = mna,
                ModifiedOn = i.ModifiedOn,
                Ort = i.Ort,
                Ortsteil = i.Ortsteil,
                PLZ = i.PLZ,
                QNA = qna,
                Rolle = rolle,
                Speichel = speichel,
                Symptomtagebuch = symptom
            };
        }

    }
}
