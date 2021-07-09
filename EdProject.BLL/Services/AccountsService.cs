using AutoMapper;
using EdProject.BLL.Common.Options;
using EdProject.BLL.Models.User;
using EdProject.BLL.Providers;
using EdProject.BLL.Providers.Interfaces;
using EdProject.BLL.Services.Interfaces;
using EdProject.DAL.Entities;
using EdProject.DAL.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Web;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EdProject.BLL.Models.ViewModels;

namespace EdProject.BLL.Services
{
    public class AccountsService : IAccountService
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly RoutingOptions _conectOption;
        private IEmailProvider _emailService;
        private IJwtProvider _jwt;
        private IMapper _mapper;
        public AccountsService(UserManager<User> userManager, SignInManager<User> signInManager,
                               IMapper mapper, IEmailProvider emailProvider, IJwtProvider jwtProvider, IOptions<RoutingOptions> options )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _emailService = emailProvider;
            _jwt = jwtProvider;
            _conectOption = options.Value;
        }


        public async Task<TokenPairModel> SignInAsync(LoginDTOModel userSignInModel)
        {
            var user = await _userManager.FindByEmailAsync(userSignInModel.Email);
            if(user is null)
            {
                throw new CustomException(ErrorConstant.USER_NOT_FOUND, HttpStatusCode.BadRequest);
            }
            if (!user.EmailConfirmed)
            {
                throw new CustomException(ErrorConstant.UNCONFIRMED_EMAIL, HttpStatusCode.BadRequest);
            }
            var result = await _signInManager.PasswordSignInAsync(user, userSignInModel.Password, userSignInModel.RememberMe, false);
            if(!result.Succeeded)
            {
                throw new CustomException(ErrorConstant.INCORRECT_PASSWORD, HttpStatusCode.BadRequest);
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            TokenPairModel tokenPairModel = new TokenPairModel
            {       
                AccessToken = _jwt.GenerateAccessToken(user,userRoles),
                RefreshToken = _jwt.GenerateRefreshToken()
            };

            user.RefreshToken = tokenPairModel.RefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddHours(6);
            await _userManager.UpdateAsync(user);
            return tokenPairModel;
        }
        public async Task<TokenPairModel> RefreshTokensAsync(string refreshToken)
        {
            var user = _userManager.Users.Where(u => u.RefreshToken == refreshToken).FirstOrDefault();
            if (user is null || user.isRemoved)
            {
                throw new CustomException(ErrorConstant.USER_NOT_FOUND, HttpStatusCode.Forbidden);
            }
            if (user.RefreshTokenExpiryTime < DateTimeOffset.Now)
            {
                throw new CustomException(ErrorConstant.INVALID_REFRESH_TOKEN, HttpStatusCode.Forbidden);
            }


            var userRoles = await _userManager.GetRolesAsync(user);
            var accessTok = _jwt.GenerateAccessToken(user, userRoles);
            var refreshTok = _jwt.GenerateRefreshToken();
            TokenPairModel tokenPairModel = new TokenPairModel
            {
                AccessToken = accessTok,
                RefreshToken = refreshTok
            };

            user.RefreshToken = tokenPairModel.RefreshToken;
            user.RefreshTokenExpiryTime = DateTimeOffset.Now.AddHours(2);

            await _userManager.UpdateAsync(user);
            return tokenPairModel;
        }
        public async Task SignOutAsync()
        {              
          await _signInManager.SignOutAsync();
        }
        public async Task RegisterUserAsync(RegistrationModel userModel)
        {
            var newUser = _mapper.Map<User>(userModel);
            if (await _userManager.FindByEmailAsync(userModel.Email) is not null)
            {
                throw new CustomException(ErrorConstant.USER_EXIST, HttpStatusCode.BadRequest);
            }
            var result = await _userManager.CreateAsync(newUser, userModel.Password);

            if (!result.Succeeded)
            {
                throw new CustomException($"{ErrorConstant.REGISTRATION_FAILED}. {result}", HttpStatusCode.BadRequest);
            }
            await _userManager.AddToRoleAsync(newUser,UserRolesType.Client.ToString());

            var confirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var confirmLink = _conectOption.ConfirmAccountRoute;

            var uriBuilder = new UriBuilder(confirmLink);
            var paramsValues = HttpUtility.ParseQueryString(uriBuilder.Query);

            paramsValues.Add("token", confirmToken);
            paramsValues.Add("email", userModel.Email);
            uriBuilder.Query = paramsValues.ToString();

            EmailModel emailMessage = new EmailModel()
            {
                Email = newUser.Email,
                Message =  $"<a href='{uriBuilder.Uri}'> Confirm Email </a>",
                Subject = EmailMessageConstant.ACCOUNT_CONFIRM_SUBJECT
            };

            await _emailService.SendEmailAsync(emailMessage);
        }
        public async Task ConfirmEmailAsync(EmailValidationModel validationModel)
        {       
            var user = await _userManager.FindByEmailAsync(validationModel.Email);
            if(user is null)
            {
                throw new CustomException(ErrorConstant.USER_NOT_FOUND, HttpStatusCode.BadRequest);
            }

            var result = await _userManager.ConfirmEmailAsync(user, validationModel.Token);
            if(!result.Succeeded)
            {
                throw new CustomException(ErrorConstant.EMAIL_NOT_CONFIRMED, HttpStatusCode.BadRequest);
            }
        }

        public async Task ResetPasswordTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (!user.EmailConfirmed)
            {
                throw new CustomException(ErrorConstant.USER_NOT_FOUND, HttpStatusCode.BadRequest);
            }

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = _conectOption.ResetAccountPasswordRoute;

            var uriBuilder = new UriBuilder(resetLink);
            var paramsValues = HttpUtility.ParseQueryString(uriBuilder.Query);
            paramsValues.Add("token", resetToken);
            paramsValues.Add("email", email);
            uriBuilder.Query = paramsValues.ToString();

            EmailModel emailMessage = new EmailModel()
            {
                Email = email,
                Message = $"<a href='{uriBuilder.Uri}'> Reset password </a>",
                Subject = EmailMessageConstant.RESET_PASSWORD_SUBJECT
            };

            await _emailService.SendEmailAsync(emailMessage);
        }
        public async Task ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
            var user = await _userManager.FindByEmailAsync(changePasswordModel.Email);
            if(user is null)
            {
                throw new CustomException(ErrorConstant.USER_NOT_FOUND, HttpStatusCode.BadRequest);
            }
            var result = await _userManager.ResetPasswordAsync(user, changePasswordModel.Token, changePasswordModel.NewPassword);
            if(!result.Succeeded)
            {
                throw new CustomException(ErrorConstant.FAILED_TO_CHANGE_PASSWORD, HttpStatusCode.BadRequest);
            }
        }

        public async Task AdminChangePasswordAsync(string email,string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            if(!result.Succeeded)
            {
                throw new CustomException(ErrorConstant.FAILED_TO_CHANGE_PASSWORD, HttpStatusCode.BadRequest);
            }
        }
    }
}
