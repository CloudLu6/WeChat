namespace Deepleo.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeInfo")]
    public partial class EmployeeInfo
    {
        [Key]
        [StringLength(10)]
        public string EmployeeNo { get; set; }

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
    }
}
