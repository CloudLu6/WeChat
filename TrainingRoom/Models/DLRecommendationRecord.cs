namespace Deepleo.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;

    [Table("DLRecommendationRecord")]
    public partial class DLRecommendationRecord:IValidatableObject
    {

        [Key]
        public int RecordNo { get; set; }

        [StringLength(10)]
        public string RecommenderNo { get; set; }

        [StringLength(50)]
        public string RecommenderProject { get; set; }

        [Required]
        [StringLength(50)]
        public string ApplicantName { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"\d{17}[\d|x]|\d{15}", ErrorMessage = "身份证号码格式错误")]
        public string ApplicantIDCard { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^1[3458][0-9]{9}$", ErrorMessage = "手机号格式不正确")]
        public string ApplicantPhone { get; set; }

        public DateTime? RecordTime { get; set; }

        public int SendState { get; set; }
        private WechartDatabase db = new WechartDatabase();
        [StringLength(1)]
        public string Repeat { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DLRecommendationRecord DLRecord = validationContext.ObjectInstance as DLRecommendationRecord;
            if (null == DLRecord)
            {
                yield break;
            }
            if (!CheckIDCard18(DLRecord.ApplicantIDCard))
            {
                yield return new ValidationResult("校验未通过，不是合法的身份证号！");
            }
            else
            {
                if (!AgeGT18(DLRecord.ApplicantIDCard))
                {
                    DLRecommendationAutoAnswer AutoAnswer = db.DLRecommendationAutoAnswers.Find(3);
                    yield return new ValidationResult(AutoAnswer.AutoAuswerText);
                }
            }
            
            
           
            //  throw new NotImplementedException();
        }

        private bool CheckIDCard18(string Id)

        {
            try { 
                long n = 0;

                if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)

                {

                    return false;//数字验证

                }

                string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";

                if (address.IndexOf(Id.Remove(2)) == -1)

                {

                    return false;//省份验证

                }

                string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");

                DateTime time = new DateTime();

                if (DateTime.TryParse(birth, out time) == false)

                {

                    return false;//生日验证

                }

                string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');

                string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');

                char[] Ai = Id.Remove(17).ToCharArray();

                int sum = 0;

                for (int i = 0; i < 17; i++)

                {

                    sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());

                }

                int y = -1;

                Math.DivRem(sum, 11, out y);

                if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())

                {

                    return false;//校验码验证

                }

                return true;//符合GB11643-1999标准
            }
            catch (Exception ex)
            {
                return false;
            }

    }

        public bool AgeGT18(string IDCard)
        {
            if (CalculateAgeCorrect(IDCard) >= 18)
                return true;
            else
                return false;

        }
        /// <summary>
        /// 计算年龄
        /// </summary>
        /// <param name="str">18位身份证号码</param>
        /// <returns></returns>
        public int CalculateAgeCorrect(string str)
        {
            try
            {
                string Sub_str = str.Substring(6, 8).Insert(4, "-").Insert(7, "-");   //提取出生年月日,"1976-08-09"
                DateTime birthDate = Convert.ToDateTime(Sub_str);
                DateTime now = DateTime.Now;
                int age = now.Year - birthDate.Year;
                if (now.Month < birthDate.Month || (now.Month == birthDate.Month && now.Day < birthDate.Day)) age--;
                return age;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        
    }
}
