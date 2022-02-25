using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SteelWeightCoreWebUI.Models.Entities;
using SteelWeightCoreWebUI.Models.Abstract;

namespace SteelWeightCoreWebUI.Models.Concrete {
    public class EFSteelRepository : ISteelRepository {
        public EFSteelRepository(EFDbContext dbContext) {
            _dbContext = dbContext;
        }

        public Steel Find(int uid) {
#if DEBUG
            Steel entry = _dbContext.test_steel_data.Find(uid);
#else
            Steel entry = _dbContext.steel_data.Find(uid);
#endif
            return entry;
        }

        public void SaveSteel(Steel steel) {
            if (steel.uid == 0) {
                _dbContext.Add(steel);
            }
            else {
                Steel entry = Find(steel.uid);
                if (entry != null) {
                    entry.date = steel.date;
                    entry.workroom = steel.workroom;
                    entry.weight = steel.weight;
                    entry.deleted = steel.deleted;
                }
            }
            _dbContext.SaveChanges();
        }

        public IEnumerable<Steel> Steels {
#if DEBUG
            get => _dbContext.test_steel_data;
#else
            get => _dbContext.steel_data; 
#endif
        }

        private readonly EFDbContext _dbContext;
    }
}
