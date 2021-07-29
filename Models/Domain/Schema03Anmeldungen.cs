using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Rki.ImportToSql.Models.Dto;
using Rki.ImportToSql.Services;
using System;
using System.Collections.Generic;

#nullable disable

namespace Rki.ImportToSql.Models.Domain
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

        /// <summary>
        /// Mapping domain <-> dto
        /// </summary>
        /// <param name="input">dto</param>
        public static implicit operator Schema03Anmeldungen(Schema03Dto input)
        {
            int statuswert_Id;
            if (!int.TryParse(input.Statuswert_Id, out statuswert_Id))
            {
                Helper.StaticHelper.MyMessageBoxNotificationInfo("int vs string xD");
                return null;
            }

            return new Schema03Anmeldungen()
            {
                Dea_Id = input.Dea_Id,
                Plz = input.Plz,
                Name = input.Name,
                Statuswert_Id = statuswert_Id,
                ModifiedBy = input.ModifiedBy,
                ModifiedAt = input.ModifiedAt,
                Kommentar = input.Kommentar
            };
        }

    }
}
