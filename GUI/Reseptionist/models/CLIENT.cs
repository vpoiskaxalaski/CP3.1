namespace Reseptionist.models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CLIENT")]
    public partial class CLIENT
    {
        public int ID { get; set; }

        [StringLength(30)]
        public string FIRST_NAME { get; set; }

        [Required]
        [StringLength(50)]
        public string MAIL { get; set; }

        [Required]
        [MaxLength(256)]
        public byte[] PASWRD { get; set; }

        public CLIENT() { }
    }
}
