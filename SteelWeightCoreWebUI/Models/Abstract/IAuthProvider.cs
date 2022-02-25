using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteelWeightCoreWebUI.Models.Abstract {
    public interface IAuthProvider {
        bool Authenticate(string username, string password);
    }
}
