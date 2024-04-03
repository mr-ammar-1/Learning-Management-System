using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LMS.Classes
{
    class mainControllerClass
    {
        public static void showControls(Control control, Control Content)
        {
            Content.Controls.Clear();
            control.Dock = DockStyle.Fill;
            control.BringToFront();
            control.Focus();
            Content.Controls.Add(control);
        }
        public static DialogResult messageDisplay(String msg, string heading, string type)
        {
            if (type == "success")
            {
                return MessageBox.Show(msg, heading, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                return MessageBox.Show(msg, heading, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static bool checkEmpty(string t)
        {
            if (string.IsNullOrWhiteSpace(t))
            {
                return true;
            }
            return false;
        }
        public static bool IsNumeric(string value)
        {
            return int.TryParse(value, out _);
        }
        public static bool isAlphabetic(string s)
        {
            foreach (char c in s)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }
        public static void CheckForEmptyFields(UserControl userControl)
        {
            List<Control> controlsToCheck = new List<Control>();

            // Add your controls to the list dynamically
            foreach (Control control in userControl.Controls)
            {
                // Only include TextBox and ComboBox controls
                if (control is TextBox || control is ComboBox)
                {
                    controlsToCheck.Add(control);
                }
            }

            foreach (Control control in controlsToCheck)
            {
                if (control is TextBox textBox && string.IsNullOrWhiteSpace(textBox.Text))
                {
                    // Display a message box indicating the empty field
                    MessageBox.Show($"Please fill in the {textBox.Tag} field.", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Set focus to the empty field
                    textBox.Focus();

                    // Exit the function if any field is empty
                    return;
                }

                if (control is ComboBox comboBox && comboBox.SelectedIndex == -1)
                {
                    // Display a message box indicating the empty field
                    MessageBox.Show($"Please select a value for the {comboBox.Tag} field.", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Set focus to the empty field
                    comboBox.Focus();

                    // Exit the function if any field is empty
                    return;
                }
            }

            // If all fields are filled, you can proceed with your logic here
            // ...

            MessageBox.Show("All fields are filled. Processing can continue.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
