namespace Deepleo.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class V_DLRecommenderInfo
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
        public string RecordTimeStr { get; set; }
        [Key]
        [Column(Order = 5)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SendState { get; set; }

        [StringLength(50)]
        public string EmployeeName { get; set; }

        [StringLength(20)]
        public string EmployeePhone { get; set; }

        [StringLength(50)]
        public string EmployeeDeparment { get; set; }

        [StringLength(50)]
        public string EmployeePosiotion { get; set; }

        [StringLength(50)]
        public string EmployeeEmail { get; set; }

        [StringLength(1)]
        public string Repeat { get; set; }
    }
}
