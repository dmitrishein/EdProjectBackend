﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Providers
{
    interface IEmailProvider
    {
        Task SendEmailAsync(string email, string subject, string message);

    }
}
