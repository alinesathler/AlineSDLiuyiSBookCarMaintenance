using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

//Aline Sathler Delfino & Liuyi Shi,  2024/03/06, Section 1, InClass 3
//Name of Project: Book Car Maintenance
//Purpose: C# Windows Form to booking car maintenance
//Revision History:
//REV00 - 2024/03/06 - Initial version
//REV01 - 2024/03/07 - Validation helper class: postal code, province code and phone number. Close and Reset buttons. Name, address and email inputs.
//REV02 - 2024/03/08 - City, province code, postal code, home phone and cell phone inputs.
//REV03 - 2024/03/08 - Make and model, year and appointment date inputs. Set focus to first input with an error.
//REV04 - 2024/03/08 - Save appointment to file.

namespace AlineSDLiuyiSBookCarMaintenance {
    public partial class CarMaintenance : Form {
        public CarMaintenance() {
            InitializeComponent();

            //Costumize the format of the date time picker.
            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.CustomFormat = "dd MMM yyyy";

            ResetInputs();
        }

        //Clean error message and background colors for the default.
        private void ChangeToInitialState() {
            //Clear errors.
            lblErrors.Text = String.Empty;

            //Clear status.
            toolStripStatusLabel.Text = String.Empty;

            //Reset backgrounds colors.
            txtCustomerName.BackColor = SystemColors.Window;
            txtAddress.BackColor = SystemColors.Window;
            txtCity.BackColor = SystemColors.Window;
            txtProvinceCode.BackColor = SystemColors.Window;
            txtPostalCode.BackColor = SystemColors.Window;
            txtHomePhone.BackColor = SystemColors.Window;
            txtCellPhone.BackColor = SystemColors.Window;
            txtEmail.BackColor = SystemColors.Window;
            txtMakeModel.BackColor = SystemColors.Window;
            txtYear.BackColor = SystemColors.Window;
            txtProblem.BackColor = SystemColors.Window;
        }

        private void ResetInputs() {
            //Clear input fields.
            txtCustomerName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtProvinceCode.Text = string.Empty;
            txtPostalCode.Text = string.Empty;
            txtHomePhone.Text = string.Empty;
            txtCellPhone.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMakeModel.Text = string.Empty;
            txtYear.Text = string.Empty;
            txtProblem.Text = string.Empty;
            dtpDate.Value = DateTime.Today.Date;

            ChangeToInitialState();
        }

        private void btnBook_Click(object sender, EventArgs e) {
            string customerName = ValidationHelper.Capitalize((txtCustomerName.Text).Trim());
            bool isName = false;

            string address = ValidationHelper.Capitalize((txtAddress.Text).Trim());
            bool isAddress = false;

            string city = ValidationHelper.Capitalize((txtCity.Text).Trim());
            bool isCity = false;

            string provinceCode = (txtProvinceCode.Text).Trim().ToUpper();
            bool isProvinceCode = ValidationHelper.IsValidProvinceCode(provinceCode);

            string postalCode = (txtPostalCode.Text).Trim().ToUpper();
            bool isPostalCode = ValidationHelper.IsValidPostalCode(postalCode);

            string homePhone = (txtHomePhone.Text).Trim();
            bool isHomePhone = ValidationHelper.IsValidPhoneNumber(homePhone);

            string cellPhone = (txtCellPhone.Text).Trim();
            bool isCellPhone = ValidationHelper.IsValidPhoneNumber(cellPhone);

            string email = (txtEmail.Text).Trim().ToLower();
            bool isEmail = false;

            string makeModel = ValidationHelper.Capitalize((txtMakeModel.Text).Trim());
            bool isMakeModel = false;

            string year = (txtYear.Text).Trim();
            bool isYear = true;

            DateTime appointmentDate = dtpDate.Value.Date;
            bool isAppointmentDate = false;

            string problem = (txtProblem.Text).Trim();

            //Instanciating a list with controls that have errors.
            List <Control> controlErros = new List<Control>();

            ChangeToInitialState();

            //Insert an empty space in postal code if it doesn't already have.
            if (!postalCode.Contains(' ') && isPostalCode) {
                postalCode = postalCode.Substring(0, 3) + " " + postalCode.Substring(3);
            }

            //Insert dashes in home phone if it doesn't already have.
            if (!homePhone.Contains('-') && isHomePhone) {
                homePhone = homePhone.Substring(0, 3) + "-" + homePhone.Substring(3, 3) + "-" + homePhone.Substring(6);
            }

            //Insert dashes in cell phone if it doesn't already have.
            if (!cellPhone.Contains('-') && isCellPhone) {
                cellPhone = cellPhone.Substring(0, 3) + "-" + cellPhone.Substring(3, 3) + "-" + cellPhone.Substring(6);
            }

            //Change backgroud color, display error message for customer name input and add control to the list of errors.
            if (string.IsNullOrEmpty(customerName)) {
                txtCustomerName.BackColor = Color.LightPink;
                lblErrors.Text += $"Please enter a customer name.\n";

                controlErros.Add(txtCustomerName);
            } else {
                isName = true;
            }

            //Check if email is a valid email address.
            if (!string.IsNullOrEmpty(email)) {
                try {
                    MailAddress mailAddress = new MailAddress(email);

                    isEmail = true;
                } catch (FormatException) {
                }
            }

            //If email isn't valid, check the address information.
            if (!isEmail) {
                //Change backgroud color, display error message for address input and add control to the list of errors.
                if (string.IsNullOrEmpty(address)) {
                    txtAddress.BackColor = Color.LightPink;
                    lblErrors.Text += $"Address is required if an email isn't provided.\n";

                    controlErros.Add(txtAddress);
                } else {
                    isAddress = true;
                }

                //Change backgroud color, display error message for city input and add control to the list of errors.
                if (string.IsNullOrEmpty(city)) {
                    txtCity.BackColor = Color.LightPink;
                    lblErrors.Text += $"City is required if an email isn't provided.\n";

                    controlErros.Add(txtCity);
                } else {
                    isCity = true;
                }

                //Change backgroud color, display error message for province code input and add control to the list of errors.
                if (string.IsNullOrEmpty(provinceCode)) {
                    txtProvinceCode.BackColor = Color.LightPink;
                    lblErrors.Text += $"Province code is required if an email isn't provided.\n";

                    controlErros.Add(txtProvinceCode);
                } else if (!isProvinceCode) {
                    txtProvinceCode.BackColor = Color.LightPink;
                    lblErrors.Text += $"Enter a valid province code.\n";

                    controlErros.Add(txtProvinceCode);
                }

                //Change backgroud color, display error message for postal code input and add control to the list of errors.
                if (string.IsNullOrEmpty(postalCode)) {
                    txtPostalCode.BackColor = Color.LightPink;
                    lblErrors.Text += $"Postal code is required if an email isn't provided.\n";

                    controlErros.Add(txtPostalCode);
                } else if (!isPostalCode) {
                    txtPostalCode.BackColor = Color.LightPink;
                    lblErrors.Text += $"Enter a valid postal code.\n";

                    controlErros.Add(txtPostalCode);
                }
            }

            //Change backgroud color, display error message for home phone and cell phone inputs and add control to the list of errors.
            if (string.IsNullOrEmpty(homePhone) && string.IsNullOrEmpty(cellPhone)) {
                txtHomePhone.BackColor = Color.LightPink;
                txtCellPhone.BackColor = Color.LightPink;
                lblErrors.Text += $"A contact phone number must be provided.\n";

                controlErros.Add(txtHomePhone);
            } else if (!isHomePhone && !isCellPhone) {
                txtHomePhone.BackColor = Color.LightPink;
                txtCellPhone.BackColor = Color.LightPink;
                lblErrors.Text += $"Please enter a valid home or cell phone number.\n";

                controlErros.Add(txtHomePhone);
            }

            //Change backgroud color, display error message for make and model input and add control to the list of errors.
            if (string.IsNullOrEmpty(makeModel)) {
                txtMakeModel.BackColor = Color.LightPink;
                lblErrors.Text += $"Please enter a make and model.\n";

                controlErros.Add(txtMakeModel);
            } else {
                isMakeModel = true;
            }

            //Change backgroud color, display error message for year input and add control to the list of errors.
            if (!string.IsNullOrEmpty(year)) {
                isYear = int.TryParse(year, out int yearNumber);

                if (!isYear || yearNumber < 1900 || yearNumber > DateTime.Today.Year + 1) {
                    txtYear.BackColor = Color.LightPink;
                    lblErrors.Text += $"Please enter a valid year.\n";

                    controlErros.Add(txtYear);

                    isYear = false;
                }
            }

            //Display error message for appointment date input and add control to the list of errors.
            if (appointmentDate < DateTime.Now.Date) {
                lblErrors.Text += "Please enter a valid date.\n";

                controlErros.Add(dtpDate);
            } else {
                isAppointmentDate = true;
            }

            //Check the first input with an error and seet focus.
            if (controlErros.Count > 0) {
                controlErros[0].Focus();
            }
            
            //If data is valid, save it in the file.
            if (isName && (isEmail || (isAddress && isCity && isProvinceCode && isPostalCode)) && (isHomePhone || isCellPhone) && isMakeModel && isYear && isAppointmentDate) {
                using (StreamWriter textOut = new StreamWriter(new FileStream(Path.GetDirectoryName(Application.ExecutablePath) + @"\Appointments.txt", FileMode.Append))) {
                    textOut.Write(customerName + "|");
                    textOut.Write(address + "|");
                    textOut.Write(city + "|");
                    textOut.Write(provinceCode + "|");
                    textOut.Write(postalCode + "|");
                    textOut.Write(homePhone + "|");
                    textOut.Write(cellPhone + "|");
                    textOut.Write(email + "|");
                    textOut.Write(makeModel + "|");
                    textOut.Write(year + "|");
                    textOut.Write(appointmentDate.ToString("dd/MM/yyyy") + "|");
                    textOut.WriteLine(problem + "|");
                }


                //Inform user that appointment was saved.
                toolStripStatusLabel.Text = "Appointment saved.";
            }
        }

        private void btnReset_Click(object sender, EventArgs e) {
            ResetInputs();
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void btnPreFill_Click(object sender, EventArgs e) {
            ResetInputs();

            txtCustomerName.Text = "Homer Simpson";
            txtAddress.Text = "742 Evergreen Terrace";
            txtCity.Text = "Springfield";
            txtProvinceCode.Text = "ON";
            txtPostalCode.Text = "N2L 2R7";
            txtHomePhone.Text = "123-123-1234";
            txtCellPhone.Text = "123-123-1234";
            txtEmail.Text = "test@test.com";
            txtMakeModel.Text = "Honda Civic";
            txtYear.Text = "2021";
            txtProblem.Text = "Bla bla bla";
            dtpDate.Value = DateTime.Today.Date;
        }
    }
}
