using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SteelWeightCoreWebUI.Models.Entities {
    public class User {
        [Key, Column(Order = 0)]
        [Required(ErrorMessage = "아이디를 입력해주십시오")]
        public string user_name { get; set; }

        [Key, Column(Order = 1)]
        [Required(ErrorMessage = "비밀번호를 입력해주십시오")]
        public string user_pwd { get; set; }

        public string user_priv { get; set; }
    }
}
