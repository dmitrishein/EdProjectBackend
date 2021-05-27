using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdProject.BLL.Models.Author
{
    public class AuthorInEditionsList : IValidatableObject
    {
        public string Editions { get; set; }
        public long AuthorId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(Editions))
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
