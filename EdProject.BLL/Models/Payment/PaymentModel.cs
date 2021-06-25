using EdProject.DAL.Entities.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace EdProject.BLL.Models.Payment
{
    public class PaymentModel : IValidatableObject
    {
        public long OrderId { get; set; }
        public string PaymentSource { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            //if (OrderId <= VariableConstant.EMPTY)
            //{
            //    errors.Add(new ValidationResult(ErrorConstant.INCORRECT_ORDER));
            //}
           
            if (!errors.Any())
            {
                return errors;
            }

            throw new CustomException(string.Join(",", errors), HttpStatusCode.BadRequest);
        }
    }
}
