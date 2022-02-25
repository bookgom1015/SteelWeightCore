using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SteelWeightCoreWebUI.Models.Entities;

namespace SteelWeightCoreWebUI.Models.Abstract {
    public interface IUserRepository {
        string Authenticate(string username, string password);

        IEnumerable<User> Users { get; }
    }
}
