using EdProject.DAL.Entities.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace EdProject.BLL.Models.User
{
    public class OrderItemModel : IValidatableObject
    {
        public long EditionId { get; set; }
        public long OrderId { get; set; }
        public int ItemsCount { get; set; }
        public decimal Amount { get; set; }
        public CurrencyTypes Currency { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (EditionId <= VariableConstant.EMPTY)
            {
                errors.Add(new ValidationResult(ErrorConstant.INCORRECT_EDITION));
            }
            if (OrderId <= VariableConstant.EMPTY)
            {
                errors.Add(new ValidationResult(ErrorConstant.INCORRECT_ORDER));
            }
            if (ItemsCount <= VariableConstant.EMPTY)
            {
               errors.Add(new ValidationResult(ErrorConstant.INCORRECT_ITEMS_COUNT));
            }

            if (!errors.Any())
            {
                return errors;
            }

            throw new CustomException(string.Join(",", errors), System.Net.HttpStatusCode.BadRequest);

        }
    }
}