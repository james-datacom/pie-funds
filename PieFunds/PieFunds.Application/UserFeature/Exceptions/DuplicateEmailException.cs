using System;

namespace PieFunds.Application.UserFeature.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException(string email)
            : base($"A user with the email '{email}' already exists.")
        {
        }
    }
}