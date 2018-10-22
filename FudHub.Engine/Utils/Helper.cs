using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FudHub.Engine.Utils
{
    public class Helper
    {
        public static string PrettyEx(Exception ex, string customErrorMessage = null)
        {
            if (string.IsNullOrEmpty(customErrorMessage))
                return $"Exception: {ex.InnerException?.InnerException?.Message ?? ex.Message}";
            else
                return customErrorMessage;
        }
    }
}
