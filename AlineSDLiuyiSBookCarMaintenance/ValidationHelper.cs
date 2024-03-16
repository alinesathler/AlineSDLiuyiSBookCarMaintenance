using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlineSDLiuyiSBookCarMaintenance {
    /// <summary>
    /// Validate and format the inputs.
    /// </summary>
    internal class ValidationHelper {
        /// <summary>
        /// Capitalize strings.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>A string capitalized.</returns>
        public static string Capitalize (string input) {
            string output = input.Trim();

            if (string.IsNullOrEmpty(output)) {
                output = string.Empty;
            } else {
                output = output.ToLower();

                string[] outputArray = output.Split(' ');

                for (int i = 0; i < outputArray.Length; i++) {
                    if (outputArray[i].Length > 1 || i == 0) {
                        outputArray[i] = char.ToUpper(outputArray[i][0]) + outputArray[i].Substring(1);
                    }
                }

                output = string.Join(" ", outputArray);
            }

            return output;
        }

        /// <summary>
        /// Check if postal code is valid.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True if the parameter is a valid postal code; otherwise, false.</returns>
        public static bool IsValidPostalCode(string input) {
            bool isValid = false;

            string pattern = @"^[a-zA-Z][0-9][a-zA-Z]\s?[0-9][a-zA-Z][0-9]$";
            Regex regex = new Regex(pattern);

            if (!string.IsNullOrEmpty(input) && regex.IsMatch(input.Trim())) {
                isValid = true;
            }

            return isValid;
        }

        /// <summary>
        /// Check if province code is valid.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True if the parameter is a valid canadian province code; otherwise, false.</returns>
        public static bool IsValidProvinceCode(string input) {
            bool isValid = false;

            string pattern = @"^AB|BC|MB|NB|NL|NS|NT|NU|ON|PE|QC|SK|YT$";
            Regex regex = new Regex(pattern);

            if (!string.IsNullOrEmpty(input) && regex.IsMatch(input.Trim().ToUpper())) {
                isValid = true;
            }

            return isValid;
        }

        /// <summary>
        /// Check if phone number is valid.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True if the parameter is a valid phone number; otherwise, false.</returns>
        public static bool IsValidPhoneNumber(string input) {
            bool isValid = false;

            string pattern = @"^\d{3}-?\d{3}-?\d{4}$";
            Regex regex = new Regex(pattern);

            if (!string.IsNullOrEmpty(input) && regex.IsMatch(input.Trim())) {
                isValid = true;
            }

            return isValid;
        }

        /// <summary>
        /// Insert an empty space in postal code if it doesn't already have it.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>A string in postal code format with white space.</returns>
        public static string FormatPostalCode(string input) {
            string output = input.Trim();

            if (!output.Contains(' ') && IsValidPostalCode(output)) {
                output = input.Substring(0, 3) + " " + input.Substring(3);
            }

            return output;
        }

        /// <summary>
        /// Insert dashes in phone number if it doesn't already have it.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>A string in phone number format with dashes.</returns>
        public static string FormatPhone(string input) {
            string output = input.Trim();

            if (IsValidPhoneNumber(output)) {
                if (output[3] != '-') {
                    output = output.Substring(0, 3) + "-" + output.Substring(3);
                }

                if (output[7] != '-') {
                    output = output.Substring(0, 7) + "-" + output.Substring(7);
                }
            }

            return output;
        }
    }
}
