using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EdProject.BLL.Models.Base
{
    public class FilterPageModel: IValidatableObject
    {
        public int ElementsAmount { get; set; }
        public int PageNumber { get; set; }
        public string SearchString { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (PageNumber is VariableConstant.EMPTY || ElementsAmount is VariableConstant.EMPTY)
            {
                errors.Add(new ValidationResult(ErrorConstant.INCORRECT_PAGEMODEL));
            }
           
            if (!errors.Any())
            {
                return errors;
            }

            throw new CustomException(string.Join(",", errors), System.Net.HttpStatusCode.BadRequest);
        }
    }
}
