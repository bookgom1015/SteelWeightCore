using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SteelWeightCoreWebUI.Models.Abstract;
using SteelWeightCoreWebUI.Models.Entities;

namespace SteelWeightCoreWebUI.Models.Concrete {
    public class EFUserRepository : IUserRepository {
        public EFUserRepository(EFDbContext dbContext) {
            _dbContext = dbContext;
        }

        public string Authenticate(string username, string password) {
            var user = _dbContext.users.Find(username, password);
            if (user != null) return user.user_priv;
            return null;
        }

        public IEnumerable<User> Users { get => _dbContext.users; }

        private readonly EFDbContext _dbContext;
    }
}
