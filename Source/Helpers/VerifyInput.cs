using System.Text.RegularExpressions;

namespace HomeControl.Source.Helpers;

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

    public static string VerifyTextAlphaNumericSpace(string value) {
        if (!string.IsNullOrEmpty(value)) {
            value = Regex.Replace(value, @"[^a-zA-Z0-9\s]+", "");
        }

        return value;
    }

    public static bool HasValue(this double value) {
        return !double.IsNaN(value) && !double.IsInfinity(value);
    }
}