﻿using System;


namespace Rki.ImportToSql.Models.Domain
{
    public class Schema03Anmeldungen : BaseModel
    {
        public string Dea_Id { get; set; }
        public string Plz { get; set; }
        public string Name { get; set; }
        public int Statuswert_Id { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string Kommentar { get; set; }

        public override string Hash => Dea_Id;
    }
}
