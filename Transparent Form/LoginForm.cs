using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Transparent_Form
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            textBox_password.PasswordChar = '*';
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_MouseEnter(object sender, EventArgs e)
        {
            label6.ForeColor = Color.Red;
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            label6.ForeColor = Color.White;
        }

        private async void button_login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_usrname.Text) || string.IsNullOrWhiteSpace(textBox_password.Text))
            {
                MessageBox.Show("Please enter username and password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string username = textBox_usrname.Text.Trim();
            string password = textBox_password.Text.Trim();
            string userType = "Administrator";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var values = new Dictionary<string, string>
                    {
                        { "username", username },
                        { "password", password },
                        { "userType", userType }
                    };

                    var content = new FormUrlEncodedContent(values);

                    HttpResponseMessage response = await client.PostAsync(
                        "http://localhost/Student-Attendance-System01-main/Student-Attendance-System01-main/api/login.php",
                        content
                    );

                    string json = await response.Content.ReadAsStringAsync();
                 

                    ApiLoginResponse result = JsonConvert.DeserializeObject<ApiLoginResponse>(json);

                    if (result != null && result.success)
                    {
                        SessionManager.Token = result.token;
                        SessionManager.UserType = result.userType;

                        if (result.data != null)
                        {
                            SessionManager.UserName = result.data.firstName + " " + result.data.lastName;
                        }

                        //   MessageBox.Show("Login successful!\n\nToken:\n" + SessionManager.Token, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        MainForm main = new MainForm();
                        this.Hide();
                        main.Show();
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Login failed.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkBox_showpass_CheckedChanged(object sender, EventArgs e)
        {
            textBox_password.PasswordChar = checkBox_showpass.Checked ? '\0' : '*';
        }
    }

    public class ApiLoginResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public string userType { get; set; }
        public UserData data { get; set; }
    }

    public class UserData
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
    }
}