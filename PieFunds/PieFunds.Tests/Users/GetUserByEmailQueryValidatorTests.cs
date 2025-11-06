using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using PieFunds.Application.Features.Users.Queries.GetUserByEmail;

namespace PieFunds.Tests.Users
{
    public class GetUserByEmailQueryValidatorTests
    {
        private readonly GetUserByEmailQueryValidator _validator;

        public GetUserByEmailQueryValidatorTests()
        {
            _validator = new GetUserByEmailQueryValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Empty()
        {
            // Arrange
            var query = new GetUserByEmailQuery(string.Empty);

            // Act
            var result = _validator.TestValidate(query);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                  .WithErrorMessage("Email is required.");
        }



    }
}
