using EShop.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.ClientModel.Primitives;
using System.Collections.Generic;
using System.Text;

namespace EShop.Application.Services.Implementations
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration _configuration;
        public SmsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendVerificationSms(string mobile, string code)
        {
            var apiKey = _configuration["Kavenegar:ApiKey"];
            var senderApi = new Kavenegar.KavenegarApi(apiKey);
            await senderApi.VerifyLookup(mobile, code, "EShopSmsVerification"); // EShopSmsVerification is the name of the template we created in the Kaveh Negar panel

        }
    }
}