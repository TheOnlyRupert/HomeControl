using System.Text.RegularExpressions;

namespace HomeControl.Source.Reference {
    public static class VerifyInput {
        public static string VerifyTextNumeric(string value) {
            if (!string.IsNullOrEmpty(value)) {
                value = Regex.Replace(value, @"[^0-9]+", "");
            }

            return value;
        }

        public static string VerifyTextAlphaNumeric(string value) {
            if (!string.IsNullOrEmpty(value)) {
                value = Regex.Replace(value, @"[^a-zA-Z0-9]+", "");
            }

            return value;
        }
    }
}