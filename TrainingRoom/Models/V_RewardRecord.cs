namespace Deepleo.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V_RewardRecord
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RecordNo { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string RecommenderNo { get; set; }

        [StringLength(50)]
        public string RecommenderProject { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(50)]
        public string ApplicantName { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(20)]
        public string ApplicantIDCard { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(20)]
        public string ApplicantPhone { get; set; }

        public DateTime? RecordTime { get; set; }

        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SendState { get; set; }

        [Key]
        [Column(Order = 6)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RewardID { get; set; }

        [Key]
        [Column(Order = 7, TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Key]
        [Column(Order = 8, TypeName = "date")]
        public DateTime EndDate { get; set; }

        [StringLength(10)]
        public string ModifyUser { get; set; }

        public DateTime? ModifyTime { get; set; }

        public int? flag { get; set; }

        public string RewardText { get; set; }
    }
}
