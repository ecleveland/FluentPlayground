using System;
using System.Text.Json;

using FluentValidation;

namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Beginning Fluent Validation proof of concept.");

            User user = new User();
            user.FirstName = "Fool";
            user.Id = 1;
            UserValidator simpleValidator = new UserValidator();

            // Could talk about the JsonSearializer in the new System.Text.Json
            var output = JsonSerializer.Serialize(user, options: new JsonSerializerOptions{
                WriteIndented = true
            });

            Console.WriteLine(output);

            var simpleValidation = simpleValidator.Validate(user);

            foreach (var error in simpleValidation.Errors)
            {
                Console.WriteLine($"Property {error.PropertyName} failed validation. Error was: {error.ErrorMessage}");
            }
            

            // try
            // {
            //     sValidate.ValidateAndThrow(user);
            // }
            // catch (ValidationException e)
            // {
            //     foreach (var error in e.Errors)
            //     {
            //         Console.WriteLine($"{error.ErrorCode}");
            //     }
            // }
        }
    }

    public class User 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
    }

    public class UserValidator : AbstractValidator<User> 
    {
        public UserValidator() {
            RuleFor(user => user.FirstName).NotNull();
            RuleFor(user => user.LastName).NotNull();
            RuleFor(user => user.Id).GreaterThanOrEqualTo(0);
            //RuleFor(customer => customer.Address).SetValidator(new AddressValidator());
            RuleFor(customer => customer.Address.ZipCode).NotNull().When(customer => customer.Address != null);
        }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
    }

    // class AddressValidator : AbstractValidator<Address>
    // {
    //     public AddressValidator()
    //     {
    //         RuleFor(address => address.ZipCode).NotNull().NotEmpty();
    //     }
    // }
}
