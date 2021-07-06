using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace EdProject.BLL.Models.User
{
    public class ChangePasswordModel:IValidatableObject
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (!Regex.IsMatch(Email, VariableConstant.EMAIL_PATERN, RegexOptions.IgnoreCase))
            {
                errors.Add(new ValidationResult(ErrorConstant.INCORRECT_EMAIL));
            }
            if (!errors.Any())
            {
                return errors;
            }
            throw new CustomException(string.Join(", ", errors), HttpStatusCode.BadRequest);
        }
    }
}
