﻿using EdProject.BLL.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Providers
{
    public interface IEmailProvider
    {
        Task SendEmailAsync(EmailModel emailModel);
    }
}
