using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteelWeightCoreWebUI.ViewModels {
    public class ShowViewModel {
        public SortedDictionary<int, SortedDictionary<int, SortedSet<int>>> Dates { get; set; }
        public SortedDictionary<string, SortedDictionary<int, int>> Datasets { get; set; }
        public SortedSet<int> Labels { get; set; }
        public string ChartTitle { get; set; }
        public string Suffix { get; set; }
    }
}
