namespace Deepleo.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
 

    [Table("DLRecommendationRewardPeriod")]
    public partial class DLRecommendationRewardPeriod:IValidatableObject
    {
       
        [Key]
        public int RewardID { get; set; }
        public int flag { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        
        public DateTime EndDate { get; set; }

        [StringLength(10)]
        public string ModifyUser { get; set; }

        public DateTime? ModifyTime { get; set; }

        public string RewardText { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DLRecommendationRewardPeriod DLReward = validationContext.ObjectInstance as DLRecommendationRewardPeriod;
            if (null == DLReward)
            {
                yield break;
            }

            if (string.IsNullOrEmpty(DLReward.StartDate.ToString()))
            {
                yield return new ValidationResult("�����뿪ʼ���ڣ�");
            }

            if (string.IsNullOrEmpty(DLReward.EndDate.ToString()))
            {
                yield return new ValidationResult("������������ڣ�");
            }

            if (DLReward.StartDate >= DLReward.EndDate)
            {
                yield return new ValidationResult("�������ڱ�����ڿ�ʼ���ڣ�");
            }

     
          //  throw new NotImplementedException();
        }

    }

}
