using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AlineSDLiuyiSBookCarMaintenance {
    //Validation class
    internal class ValidationHelper {
        //Capitalize strings.
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

        //Check if postal code is valid.
        public static bool IsValidPostalCode(string input) {
            bool isValid = false;

            string pattern = @"^[a-zA-Z][0-9][a-zA-Z]\s?[0-9][a-zA-Z][0-9]$";
            Regex regex = new Regex(pattern);

            if (!string.IsNullOrEmpty(input) && regex.IsMatch(input.Trim())) {
                isValid = true;
            }

            return isValid;
        }

        //Check if province code is valid.
        public static bool IsValidProvinceCode(string input) {
            bool isValid = false;

            string pattern = @"^AB|BC|MB|NB|NL|NS|NT|NU|ON|PE|QC|SK|YT$";
            Regex regex = new Regex(pattern);

            if (!string.IsNullOrEmpty(input) && regex.IsMatch(input.Trim().ToUpper())) {
                isValid = true;
            }

            return isValid;
        }

        //Check if phone number is valid.
        public static bool IsValidPhoneNumber(string input) {
            bool isValid = false;

            string pattern = @"^\d{3}-?\d{3}-?\d{4}$";
            Regex regex = new Regex(pattern);

            if (!string.IsNullOrEmpty(input) && regex.IsMatch(input.Trim())) {
                isValid = true;
            }

            return isValid;
        }
    }
}
