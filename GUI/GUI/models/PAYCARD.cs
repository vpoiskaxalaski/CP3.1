namespace GUI.models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PAYCARD")]
    public partial class PAYCARD
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string CARD_OWNER { get; set; }

        [Required]
        [StringLength(30)]
        public string CARD_TYPE { get; set; }

        [Required]
        [StringLength(19)]
        public string CARD_NUMBER { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime EXPIRATION_DATE { get; set; }

        public short CVC { get; set; }
    }
}
