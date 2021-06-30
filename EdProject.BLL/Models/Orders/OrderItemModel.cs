using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace EdProject.BLL.Models.User
{
    public class OrderItemModel : IValidatableObject
    {
        public long EditionId { get; set; }
        public string BookType { get; set; }
        public string Title { get; set; }
        public int ItemsCount { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (EditionId <= VariableConstant.EMPTY)
            {
                errors.Add(new ValidationResult(ErrorConstant.INCORRECT_EDITION));
            }
            if (ItemsCount < VariableConstant.EMPTY)
            {
               errors.Add(new ValidationResult(ErrorConstant.INCORRECT_ITEMS_COUNT));
            }

            if (!errors.Any())
            {
                return errors;
            }

            throw new CustomException(string.Join(",", errors), HttpStatusCode.BadRequest);

        }
    }
}