using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SteelWeightCoreWebUI.Models.Abstract;
using SteelWeightCoreWebUI.Models.Entities;
using SteelWeightCoreWebUI.Helpers;
using SteelWeightCoreWebUI.ViewModels;

namespace SteelWeightCoreWebUI.Controllers {
    public class ChartController : Controller {
        public ChartController(ISteelRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        public ViewResult Show(int? year = null, int? month = null, int? day = null) {
            ShowViewModel viewModel;
            ArrangeViewModel(out viewModel, year, month, day);
            if (day != null) {
                viewModel.ChartTitle = "일일 차트";
                viewModel.Suffix = "일";
            }
            else if (month != null) {
                viewModel.ChartTitle = "일간 차트";
                viewModel.Suffix = "일";
            }
            else if (year != null) {
                viewModel.ChartTitle = "월간 차트";
                viewModel.Suffix = "월";
            }
            else {
                viewModel.ChartTitle = "연간 차트";
                viewModel.Suffix = "년";
            }
            ViewBag.IsMobile = UserAgentHelper.IsMobile(Request);
            return View(viewModel);
        }

        [HttpGet]
        public PartialViewResult ChartPart(ShowViewModel viewModel) {
            return PartialView(viewModel);
        }

        [HttpGet]
        public PartialViewResult NavPart(ShowViewModel viewModel) {
            return PartialView(viewModel);
        }

        [HttpGet]
        public PartialViewResult NavPartMobile(ShowViewModel viewModel) {
            return PartialView(viewModel);
        }

        private void ArrangeViewModel(out ShowViewModel viewModel, int? year = null, int? month = null, int? day = null) {
            SortedDictionary<int, SortedDictionary<int, SortedSet<int>>> dates = new SortedDictionary<int, SortedDictionary<int, SortedSet<int>>>();
            SortedDictionary<string, SortedDictionary<int, int>> datasets = new SortedDictionary<string, SortedDictionary<int, int>>();
            SortedSet<int> labels = new SortedSet<int>();

            viewModel = new ShowViewModel {
                Dates = dates,
                Datasets = datasets,
                Labels = labels
            };

            var steels = _repository.Steels.Where(s => s.deleted == 0).ToList();

            foreach (Steel steel in steels) {
                DateTime specificDate = (DateTime)steel.date;

                int specYear = specificDate.Year;
                int specMonth = specificDate.Month;
                int specDay = specificDate.Day;
                int weight = (int)steel.weight;
                string workroom = steel.workroom;

                //
                // Dates
                //
                if (!dates.ContainsKey(specYear)) dates[specYear] = new SortedDictionary<int, SortedSet<int>>();
                var datesYear = dates[specYear];

                if (!datesYear.ContainsKey(specMonth)) datesYear[specMonth] = new SortedSet<int>();
                var datesMonth = datesYear[specMonth];

                datesMonth.Add(specDay);

                //
                // Datasets, Labels
                //
                if (day != null) {
                    if (specYear == year && specMonth == month && specDay == day) {
                        // Labels
                        labels.Add(specDay);

                        // Datasets
                        if (!datasets.ContainsKey(workroom)) datasets[workroom] = new SortedDictionary<int, int>();
                        var workrooms = datasets[workroom];

                        if (workrooms.ContainsKey(specDay)) workrooms[specDay] += weight;
                        else workrooms.Add(specDay, weight);
                    }
                }
                else if (month != null) {
                    if (specYear == year && specMonth == month) {
                        // Labels
                        labels.Add(specDay);

                        // Datasets
                        if (!datasets.ContainsKey(workroom)) datasets[workroom] = new SortedDictionary<int, int>();
                        var workrooms = datasets[workroom];

                        if (workrooms.ContainsKey(specDay)) workrooms[specDay] += weight;
                        else workrooms.Add(specDay, weight);
                    }
                }
                else if (year != null) {
                    if (specYear == year) {
                        // Labels
                        labels.Add(specMonth);

                        // Datasets
                        if (!datasets.ContainsKey(workroom)) datasets[workroom] = new SortedDictionary<int, int>();
                        var workrooms = datasets[workroom];

                        if (workrooms.ContainsKey(specMonth)) workrooms[specMonth] += weight;
                        else workrooms.Add(specMonth, weight);
                    }
                }
                else {
                    // Labels
                    labels.Add(specYear);

                    // Datasets
                    if (!datasets.ContainsKey(workroom)) datasets[workroom] = new SortedDictionary<int, int>();
                    var workrooms = datasets[workroom];

                    if (workrooms.ContainsKey(specYear)) workrooms[specYear] += weight;
                    else workrooms.Add(specYear, weight);
                }
            }
        }

        private readonly ISteelRepository _repository;
    }
}
