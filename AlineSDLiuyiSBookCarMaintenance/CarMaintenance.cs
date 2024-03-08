using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

//Aline Sathler Delfino & Liuyi Shi,  2024/03/06, Section 1, InClass 3
//Name of Project: Book Car Maintenance
//Purpose: C# Windows Form to booking car maintenance
//Revision History:
//REV00 - 2024/03/06 - Initial version

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
            //Clear error and status messages.
            lblErrors.Text = String.Empty;

            //Reset backgrounds colors.
            txtCustomerName.BackColor = SystemColors.Window;
            txtAddress.BackColor = SystemColors.Window;
            txtCity.BackColor = SystemColors.Window;
            txtProvince.BackColor = SystemColors.Window;
            txtPostalCode.BackColor = SystemColors.Window;
            txtHomePhone.BackColor = SystemColors.Window;
            txtCellPhone.BackColor = SystemColors.Window;
            txtEmail.BackColor = SystemColors.Window;
            txtMakeModel.BackColor = SystemColors.Window;
            txtYear.BackColor = SystemColors.Window;
            txtProblem.BackColor = SystemColors.Window;
        }

        private void ResetInputs () {
            //Clear input fields.
            txtCustomerName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtProvince.Text = string.Empty;
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

            string email = (txtEmail.Text).Trim();
            bool isEmail = false;

            ChangeToInitialState();

            //Change backgroud color, display error message for customer name input and capitalize it.
            if (string.IsNullOrEmpty(customerName)) {
                txtCustomerName.BackColor = Color.LightPink;
                lblErrors.Text += $"Please enter a valid customer name.\n";
            } else {
                isName = true;
            }

            if (!string.IsNullOrEmpty(email)) {
                //Check if email is a validade email address.
                try {
                    MailAddress mailAddress = new MailAddress(email);

                    isEmail = true;
                } catch (FormatException) {
                }
            }

            //If email isn't valid, check the address information.
            if (!isEmail) {
                //Change backgroud color, display error message for address input and capitalize it.
                if (string.IsNullOrEmpty(address)) {
                    txtAddress.BackColor = Color.LightPink;
                    lblErrors.Text += $"Address is required if an email isn't provided.\n";
                } else {
                    isAddress = true;
                }
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
            txtProvince.Text = "ON";
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
