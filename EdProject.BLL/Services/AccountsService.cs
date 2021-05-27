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
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;


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


        public async Task<TokenPairModel> SignInAsync(LoginModel userSignInModel)
        {
            var user = await _userManager.FindByEmailAsync(userSignInModel.Email);
            if(user is null)
            {
                throw new CustomException(ErrorConstant.USER_NOT_FOUND, HttpStatusCode.BadRequest);
            }

            var result = await _signInManager.PasswordSignInAsync(user, userSignInModel.Password, userSignInModel.RememberMe, false);
            if(!result.Succeeded)
            {
                throw new CustomException(ErrorConstant.INCORRECT_PASSWORD, HttpStatusCode.BadRequest);
            }

            TokenPairModel tokenPairModel = new TokenPairModel
            {
                AccessToken = _jwt.GenerateAccessToken(user,await _userManager.GetRolesAsync(user)),
                RefreshToken = _jwt.GenerateRefreshToken()
            };

            return tokenPairModel;
        }
        public async Task SignOutAsync()
        {              
          await _signInManager.SignOutAsync();
        }
        public async Task RegisterUserAsync(RegistrationModel userModel)
        {
            var newUser = _mapper.Map<RegistrationModel, User>(userModel);
            var result = await _userManager.CreateAsync(newUser, userModel.Password);

            if (!result.Succeeded)
            {
                throw new CustomException($"{ErrorConstant.REGISTRATION_FAILED}. {result}", HttpStatusCode.BadRequest);
            }
            await _userManager.AddToRoleAsync(newUser,UserRolesType.Client.ToString());

            var confirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var confirmLink = _conectOption.ConfirmAccountRoute;
            EmailModel emailMessage = new()
            {
                Email = newUser.Email,
                Message = string.Format(confirmLink, confirmToken, newUser.Email),
                Subject = "Account confirm"
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

            await _userManager.ConfirmEmailAsync(user, validationModel.Token);    
        }

        public async Task ResetPasswordTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = _conectOption.ResetAccountPasswordRoute;

            EmailModel emailMessage = new()
            {
                Email = email,
                Message = string.Format(resetLink, resetToken, email),
                Subject = "Reset Password"
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

            await _userManager.ResetPasswordAsync(user, changePasswordModel.Token, changePasswordModel.NewPassword);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<IList<string>> GetUserRoleAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                throw new CustomException(ErrorConstant.ITEM_NOT_FOUND, HttpStatusCode.NoContent);
            }

            return await _userManager.GetRolesAsync(user);
        }
    }
}
