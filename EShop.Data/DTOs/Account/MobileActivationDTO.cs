using System.ComponentModel.DataAnnotations;

namespace EShop.Data.DTOs.Account
{
    public class MobileActivationDTO : CaptchaDTO
    {
        [Display(Name = "کد فعالسازی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(5, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [MinLength(5, ErrorMessage = "{0} نمی تواند کمتر از {1} کاراکتر باشد")]
        public string ActivationCode { get; set; }
        public string Mobile { get; set; }
        public string? ReturnUrl { get; set; }
    }
    public enum ActivationResult
    {
        Success,
        Error
    }
}
