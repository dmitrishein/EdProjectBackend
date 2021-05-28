﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EdProject.BLL.Models.Author
{
    public class AuthorModel:BaseModel,IValidatableObject
    {
        public string Name { get; set; }
        public string EditionsString { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(Name)||Name.Length < VariableConstant.MIN_FIELD_SIZE)
            {
                errors.Add(new ValidationResult($"{ErrorConstant.INVALID_FIELD_FIRSTNAME}. {ErrorConstant.FIELD_IS_TOO_SHORT}"));
            }

            if (string.IsNullOrWhiteSpace(Name)|| Name.Any(char.IsDigit) || Name.Any(char.IsPunctuation))
            {
                errors.Add(new ValidationResult(ErrorConstant.INVALID_FIELD_FIRSTNAME));
            }
            
            if (!errors.Any())
            {
                return errors;
            }

            throw new CustomException(string.Join(",", errors), System.Net.HttpStatusCode.BadRequest);
        }
    }
}
