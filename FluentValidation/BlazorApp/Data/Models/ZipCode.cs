using FluentValidation;

namespace BlazorApp.Data.Models
{
    public class ZipCode
    {
        public string Code { get; set; }
    }

    public class ZipCodeValidator : AbstractValidator<ZipCode>
    {
        
    }
}