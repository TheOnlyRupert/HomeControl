using System.Text.RegularExpressions;

namespace HomeControl.Source.Helpers;

public static class VerifyInput {
    // General method to sanitize input based on a regex pattern
    private static string SanitizeInput(string value, string pattern) {
        return string.IsNullOrWhiteSpace(value) ? value : Regex.Replace(value, pattern, string.Empty);
    }

    // Verifies the text to contain only numeric characters
    public static string VerifyTextNumeric(string value) {
        return SanitizeInput(value, @"[^0-9]+");
    }

    // Verifies the text to contain only alphanumeric characters
    public static string VerifyTextAlphaNumeric(string value) {
        return SanitizeInput(value, @"[^a-zA-Z0-9]+");
    }

    // Verifies the text to contain only alphanumeric characters and spaces
    public static string VerifyTextAlphaNumericSpace(string value) {
        return SanitizeInput(value, @"[^a-zA-Z0-9\s]+");
    }

    // Checks if a numeric value is valid (not NaN or Infinity)
    public static bool HasValue(this double value) {
        return !double.IsNaN(value) && !double.IsInfinity(value);
    }

    // Extension method for float type
    public static bool HasValue(this float value) {
        return !float.IsNaN(value) && !float.IsInfinity(value);
    }

    // Extension method for decimal type
    public static bool HasValue(this decimal value) {
        // Assuming decimal always has a valid value (no NaN/Infinity)
        return true;
    }
}