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
            user.Id = 1;
            UserValidator validator = new UserValidator();

            var output = JsonSerializer.Serialize(user, options: new JsonSerializerOptions{
                WriteIndented = true
            });
            Console.WriteLine(output);

            var result = validator.Validate(user);

            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Property {error.PropertyName} failed validation. Error was: {error.ErrorMessage}");
            }
        }
    }

    public class User 
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UserValidator : AbstractValidator<User> 
    {
        public UserValidator() {
            RuleFor(user => user.FirstName).NotNull();
            RuleFor(user => user.LastName).NotNull();
            RuleFor(user => user.Id).GreaterThanOrEqualTo(0);
        }
    }
}
