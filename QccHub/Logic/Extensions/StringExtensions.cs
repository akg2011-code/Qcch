using System;
using System.Collections.Generic;
using System.Text;

namespace QccHub.Logic.Extensions
{
    public static class StringExtensions
    {
        private const string errorsSparator = "@@";
        private const string errorKeyValueSparator = "&&";

        public static string AddError(this string errorString, string errorKey, string errorValue)
        {
            errorString += string.IsNullOrEmpty(errorString) ? errorKey + errorKeyValueSparator + errorValue
            : errorsSparator + errorKey + errorKeyValueSparator + errorValue;

            return errorString;
        }

        public static string AddError(string errorKey, string errorValue)
        {
            return AddError("", errorKey, errorValue);
        }
    }
}
