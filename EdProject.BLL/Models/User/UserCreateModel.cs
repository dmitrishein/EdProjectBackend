using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace EdProject.BLL
{
    public class UserCreateModel: IValidatableObject
    {
        public string UserName { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {   
            List<ValidationResult> errors = new List<ValidationResult>();
            if (!UserName.Any(char.IsLetterOrDigit) || string.IsNullOrWhiteSpace(UserName) || UserName.Length < 3)
            {
                errors.Add(new ValidationResult(ErrorConstant.INVALID_FIELD_USERNAME));
            }
            if (FirstName.Any(char.IsDigit) || FirstName.Any(char.IsPunctuation) || FirstName.Any(char.IsSymbol) || string.IsNullOrWhiteSpace(FirstName))
            {
                errors.Add(new ValidationResult("Invalid FirstName"));
            }
            if (LastName.Any(char.IsDigit) || LastName.Any(char.IsPunctuation) || LastName.Any(char.IsSymbol) || string.IsNullOrWhiteSpace(LastName))
            {
                errors.Add(new ValidationResult("Invalid LastName"));
            }
            if (!Regex.IsMatch(Email, VariableConstant.EMAIL_PATERN, RegexOptions.IgnoreCase))
            {
                errors.Add(new ValidationResult(ErrorConstant.INCORRECT_EMAIL));
            }
            if (!Password.Equals(ConfirmPassword))
            {
                errors.Add(new ValidationResult("Password's doesn't match"));
            }

            //throw new CustomException(string.Join(",", errors), System.Net.HttpStatusCode.BadRequest);
            return errors;
        }
    }
}
