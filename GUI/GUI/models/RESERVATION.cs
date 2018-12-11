namespace GUI.models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RESERVATION")]
    public partial class RESERVATION
    {
        public int ID { get; set; }

        public short? ROOM_NUMBER { get; set; }

        public DateTime COMMING { get; set; }

        public DateTime LEAVING { get; set; }

        public int? CLIENT_ID { get; set; }
    }
}
