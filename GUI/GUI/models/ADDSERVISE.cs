namespace GUI.models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Serializable]
    [Table("ADDSERVISE")]
    public partial class ADDSERVISE
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string SERV_NAME { get; set; }

        [Column(TypeName = "text")]
        public string DESCR { get; set; }

        [Column(TypeName = "money")]
        public decimal COST { get; set; }

        public ADDSERVISE() { }
    }
}
