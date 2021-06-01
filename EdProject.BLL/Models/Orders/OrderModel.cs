using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EdProject.BLL.Models.Orders
{
    public class OrderModel : BaseModel, IValidatableObject
    {
        
        public long UserId { get; set; }
        public string Description { get; set; }
        public string StatusType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if(UserId <= VariableConstant.EMPTY)
            {
                errors.Add(new ValidationResult(ErrorConstant.USER_NOT_FOUND));
            }

            if(!errors.Any())
            {
                return errors;
            }

            throw new CustomException(string.Join(",", errors), System.Net.HttpStatusCode.BadRequest);
        }
    }
}
