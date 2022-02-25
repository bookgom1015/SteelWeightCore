using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SteelWeightCoreWebUI.Models.Entities;

namespace SteelWeightCoreWebUI.Models.Abstract {
    public interface ISteelRepository {
        Steel Find(int uid);
        void SaveSteel(Steel steel);

        IEnumerable<Steel> Steels { get; }
    }
}
