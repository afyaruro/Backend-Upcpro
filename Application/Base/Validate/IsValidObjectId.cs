using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Base.Validate
{
    public static class IsValidObjectId
    {
        public static bool IsValid(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            return id.Length == 24 && System.Text.RegularExpressions.Regex.IsMatch(id, "^[0-9a-fA-F]{24}$");
        }
    }
}