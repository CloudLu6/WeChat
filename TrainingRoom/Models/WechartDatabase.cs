namespace Deepleo.Web.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class WechartDatabase : DbContext 
    {
        public WechartDatabase()
            : base("name=WechartDatabase")
        {
        }
     
        public System.Data.Entity.DbSet<Deepleo.Web.Models.EmployeeInfo> EmployeeInfo { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
        public virtual DbSet<V_DLRecommenderInfo> V_DLRecommenderInfo { get; set; }
        public virtual DbSet<V_RewardRecord> V_RewardRecord { get; set; }

        public System.Data.Entity.DbSet<Deepleo.Web.Models.DLRecommendationRewardPeriod> DLRecommendationRewardPeriods { get; set; }

        public System.Data.Entity.DbSet<Deepleo.Web.Models.DLRecommendationRecord> DLRecommendationRecords { get; set; }

        public System.Data.Entity.DbSet<Deepleo.Web.Models.DLRecommendationAutoAnswer> DLRecommendationAutoAnswers { get; set; }
       
    }
}
