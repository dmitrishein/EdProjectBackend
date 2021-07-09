using EdProject.BLL.Models.Editions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EdProject.BLL.Models.AuthorDTO
{
    public class AuthorModel : BaseModel, IValidatableObject
    {
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(Name) || Name.Length < VariableConstant.MIN_FIELD_SIZE)
            {
                errors.Add($"{ErrorConstant.INVALID_FIELD_FIRSTNAME}. {ErrorConstant.FIELD_IS_TOO_SHORT}");
            }
            if (string.IsNullOrWhiteSpace(Name) || Name.Any(char.IsDigit) || Name.Any(char.IsPunctuation))
            {
                errors.Add(ErrorConstant.INVALID_FIELD_FIRSTNAME);
            }
            if (!errors.Any())
            {
                return null;
            }

            throw new CustomException(errors, System.Net.HttpStatusCode.BadRequest);

        }
    }
}
