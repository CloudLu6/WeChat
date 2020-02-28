namespace Deepleo.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DLRecommendationAutoAnswer")]
    public partial class DLRecommendationAutoAnswer
    {
        [Key]
        public int AutoAnswerId { get; set; }

        [StringLength(50)]
        public string Describe { get; set; }

        public string AutoAuswerText { get; set; }
    }
}
