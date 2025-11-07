using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PieFunds.Application.Interfaces;
using PieFunds.Domain.Entities;

namespace PieFunds.Infrastructure.Persistence
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static readonly List<User> _users =
        [
            new() { Id = Guid.Parse("733F801B-3C80-4574-AB78-F09D0717C5EB"), Name = "John Doe", Email = "johndoe@email.com" },
            new() { Id = Guid.Parse("1C9B7137-296A-4EFF-9F2D-A2DA27BABC73"), Name = "Jane Doe", Email = "janedoe@email.com" }
        ];

        public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = _users.FirstOrDefault(x => x.Email == email);
            return Task.FromResult<User?>(user);
        }

        public Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }
        public void Clear()
        {
            _users.Clear();
        }
    }
}
