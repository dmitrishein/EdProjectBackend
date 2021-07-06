using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace EdProject.BLL.Models.User
{
    public class LoginDTOModel: IValidatableObject
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (!Regex.IsMatch(Password,VariableConstant.PASSWORD_PATTERN))
            {
               errors.Add(new ValidationResult(ErrorConstant.INCORRECT_PASSWORD));
            }
            if (!Regex.IsMatch(Email, VariableConstant.EMAIL_PATERN, RegexOptions.IgnoreCase))
            {
                errors.Add(new ValidationResult(ErrorConstant.INCORRECT_EMAIL));
            }

            if(!errors.Any())
            {
                return errors;
            }

            throw new CustomException(string.Join("\n", errors), System.Net.HttpStatusCode.BadRequest);
        }
    }
}
