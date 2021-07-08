using EdProject.BLL.Models.AuthorDTO;
using EdProject.DAL.Entities.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EdProject.BLL.Models.Editions
{
    public class CreateEditionModel:BaseModel, IValidatableObject
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public AvailableStatusType Status { get; set; }
        public CurrencyTypes Currency { get; set; }
        public EditionTypes Type { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if(string.IsNullOrWhiteSpace(Title))
            {
                errors.Add(new ValidationResult(ErrorConstant.INVALID_TITLE));
            }
            if (Title.Any(char.IsPunctuation) || Title.Any(char.IsSymbol))
            {
                errors.Add(new ValidationResult(ErrorConstant.INVALID_TITLE));
            }
            if (Title.Length < VariableConstant.MIN_FIELD_SIZE)
            {
                errors.Add(new ValidationResult(ErrorConstant.INVALID_TITLE));
            }
            if (Price < VariableConstant.MIN_PRICE)
            {
                errors.Add(new ValidationResult(ErrorConstant.INCORRECT_PRICE));
            }
            if (!errors.Any())
            {
                return errors;
            }

            throw new CustomException(string.Join(",", errors), System.Net.HttpStatusCode.BadRequest);
        }
    }
}
