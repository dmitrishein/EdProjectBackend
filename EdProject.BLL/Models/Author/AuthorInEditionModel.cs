using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EdProject.BLL.Models.Author
{
    public class AuthorInEditionModel : IValidatableObject
    {
        public long EditionId { get; set; }
        public long AuthorId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (EditionId <= VariableConstant.EMPTY)
            {
                errors.Add(new ValidationResult(ErrorConstant.INCORRECT_EDITION));
            }
            if (AuthorId <= VariableConstant.EMPTY)
            {
                errors.Add(new ValidationResult(ErrorConstant.AUTHOR_NOT_FOUND));
            }
          

            if (!errors.Any())
            {
                return errors;
            }

            throw new CustomException(string.Join(",", errors), System.Net.HttpStatusCode.BadRequest);
        }
    }
}
