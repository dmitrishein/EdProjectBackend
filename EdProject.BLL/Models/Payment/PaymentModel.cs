using EdProject.DAL.Entities.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EdProject.BLL.Models.Payment
{
    public class PaymentModel : IValidatableObject
    {
        public long Amount { get; set; }
        public CurrencyTypes Currency { get; set; }
        public string TransactionId { get; set; }
        public long OrderId { get; set; }
        public string PaymentSource { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Amount <= VariableConstant.MIN_PRICE)
            {
                errors.Add(new ValidationResult(ErrorConstant.INCORRECT_PRICE));
            }
            if (OrderId <= VariableConstant.EMPTY)
            {
                errors.Add(new ValidationResult(ErrorConstant.INCORRECT_ORDER));
            }
            if (string.IsNullOrEmpty(TransactionId))
            {
                errors.Add(new ValidationResult(ErrorConstant.INCORRECT_TRANSACTION));
            }

            if (!errors.Any())
            {
                return errors;
            }

            throw new CustomException(string.Join(",", errors), System.Net.HttpStatusCode.BadRequest);
        }
    }
}
