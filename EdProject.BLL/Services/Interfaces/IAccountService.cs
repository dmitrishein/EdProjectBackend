﻿using EdProject.BLL.Models.User;
using EdProject.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EdProject.BLL.Services.Interfaces
{
    public interface IAccountService
    {
        public Task RegisterUserAsync(RegistrationModel newUser);
        public Task<TokenPairModel> SignInAsync(LoginDTOModel userSignInModel);
        public Task Login(LoginDTOModel login);
        public Task<TokenPairModel> RefreshTokensAsync(string refreshToken);
        public Task SignOutAsync();
        public Task ConfirmEmailAsync(EmailValidationModel validationModel);
        public Task ResetPasswordTokenAsync(string email);
        public Task ChangePasswordAsync(ChangePasswordModel resetPasswordModel);
    }
}
