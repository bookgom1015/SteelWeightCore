using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteelWeightCoreWebUI.Models.Concrete {
    public class Workroom {
        static Workroom() {
            List<string> workroomList = new List<string> {
                "기계실1",
                "기계실2",
            };

            Workrooms = workroomList;
        }

        public static IEnumerable<string> Workrooms { get; }
    }
}
