using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Newtonsoft.Json;

namespace SteelWeightCoreWebUI.Models.Entities {
    public static class SteelTempDataExtensions {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class {
            object obj;
            tempData.TryGetValue(key, out obj);
            return obj == null ? null : JsonConvert.DeserializeObject<T>((string)obj);
        }
    }

    public class Steel {
        [Key]
        public int uid { get; set; }

        [Required(ErrorMessage = "날짜를 선택해주십시오")]
        public DateTime? date { get; set; }

        [Required(ErrorMessage = "작업실을 선택해주십시오")]
        public string workroom { get; set; }

        [Required(ErrorMessage = "중량을 입력해주십시오")]
        [Range(0, double.MaxValue, ErrorMessage = "음수는 허용되지 않는 값입니다")]
        public int? weight { get; set; }

        public int deleted { get; set; }
    }
}
