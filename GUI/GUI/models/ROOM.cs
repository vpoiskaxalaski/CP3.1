namespace GUI.models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Serializable]
    [Table("ROOM")]
    public partial class ROOM
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short ROOM_NUMBER { get; set; }

        [Required]
        [StringLength(20)]
        public string ROOM_TYPE { get; set; }

        [Column(TypeName = "money")]
        public decimal COST { get; set; }

        public bool? ISAVALIABLE { get; set; }

        public ROOM() { }
        public ROOM (short n, string t, decimal c)
        {
            ROOM_NUMBER = n;
            ROOM_TYPE = t;
            COST = c;
            ISAVALIABLE = true;
        }
    }
}
