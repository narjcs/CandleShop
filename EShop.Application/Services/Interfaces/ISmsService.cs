using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Application.Services.Interfaces
{
    public interface ISmsService
    {
        Task SendVerificationSms(string mobile, string code);

    }
}
