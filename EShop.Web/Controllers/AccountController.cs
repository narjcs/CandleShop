using EShop.Application.Services.Interfaces;
using EShop.Data.DTOs.Account;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EShop.Web.Controllers
{
    public class AccountController : BaseController
    {
        #region CTOR
        private readonly IUserService _userService;
        private readonly ICaptchaValidator _captchaValidator;
        public AccountController(IUserService userService, ICaptchaValidator captchaValidator)
        {
            _userService = userService;
            _captchaValidator = captchaValidator;
        }
        #endregion

        #region Register or Login
        [HttpGet("register")]
        public async Task<IActionResult> RegisterOrLogin(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterOrLogin(RegisterUserDTO dto)
        {
            await _userService.RegisterOrLoginUser(dto);
            return RedirectToAction("MobileAuthorization" , new { rerutnUrl = dto.ReturnUrl });
        }
        #endregion

        #region MobileAuthorization
        [HttpGet("authorization")]
        public async Task<IActionResult> MobileAuthorization(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("authorization"), ValidateAntiForgeryToken]
        public async Task<IActionResult> MobileAuthorization(MobileActivationDTO dto)
        {
            #region Captcha Validation
            if (!await _captchaValidator.IsCaptchaPassedAsync(dto.Token))
            {
                TempData[ErrorMessage] = "اعتبارسنجی کپتچا موفقیت آمیز نبود. لطفا VPN خود را خاموش کنید.";
                return View(dto);
            }
            #endregion

            if (ModelState.IsValid)
            {
                var result = await _userService.CheckMobileAuthorization(dto);
                if (!result)
                {
                    TempData[ErrorMessage] = "کد اعتبارسنجی صحیح نمی‌ باشد.";
                    return View(dto);
                }

                var user = await _userService.GetUserByMobile(dto.Mobile);
                if (user == null) return NotFound();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name , user.MobileNumber),
                    new Claim(ClaimTypes.NameIdentifier , user.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims , CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(principal, properties);
                TempData[SuccessMessage] = "خوش آمدید!";

                if (!string.IsNullOrEmpty(dto.ReturnUrl) && Url.IsLocalUrl(dto.ReturnUrl))
                {
                    return Redirect(dto.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index" , "Home");
                }
            }

            TempData[ErrorMessage] = "لطفا خطاهای زیر را رفع کنید.";
            return View(dto);
        }
        #endregion

        [Route("Log-out")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
