using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace lexicon_university.Web.Validations
{
    public class CheckStreetNr : ValidationAttribute, IClientModelValidator
    {
        private readonly int max;

        public CheckStreetNr(int max)
        {
            this.max = max;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-streetnr", "Not a valid street nr");
            context.Attributes.Add("data-val-streetnr-max", $"{max}");
        }

        public override bool IsValid(object? value)
        {
            if (value is string input)
            {
                var num = input.Trim().Split().Last();
                return int.TryParse(num, out int res) & res <= max;
            }
            return false;
        }
    }
}
