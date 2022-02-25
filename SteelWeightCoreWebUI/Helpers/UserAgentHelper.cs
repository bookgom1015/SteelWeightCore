using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteelWeightCoreWebUI.Helpers {
    public class UserAgentHelper {
        public static bool IsMobile(HttpRequest request) {
            string userAgent = request.Headers["User-Agent"].ToString();
            int index = userAgent.IndexOf("mobi", 0, userAgent.Length, StringComparison.OrdinalIgnoreCase);
            return index != -1;
        }
    }
}
