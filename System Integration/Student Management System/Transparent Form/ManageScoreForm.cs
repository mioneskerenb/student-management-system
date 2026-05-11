using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transparent_Form
{
    public partial class ManageScoreForm : Form
    {
        CourseClass course = new CourseClass();
        ScoreClass score = new ScoreClass();

        public ManageScoreForm()
        {
            InitializeComponent();
            UITheme.ApplyFormTheme(this);
        }

        private async void ManageScoreForm_Load(object sender, EventArgs e)
        {
            await LoadAdmins();
        }

        public void showScore()
        {
            
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
          
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
        }

        private void DataGridView_course_Click(object sender, EventArgs e)
        {
            
        }

        private void button_search_Click(object sender, EventArgs e)
        {
           
        }
        private List<AdminItem> adminList = new List<AdminItem>();
        private int selectedAdminId = 0;

       
          private async Task LoadAdmins()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(
                        "http://localhost/Student-Attendance-System01-main/api/admins.php"
                    );

                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("HTTP Error: " + response.StatusCode + "\n\n" + json);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(json))
                    {
                        MessageBox.Show("Empty response from admins.php");
                        return;
                    }

                    if (json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show("admins.php returned HTML, not JSON.\n\n" + json);
                        return;
                    }

                    AdminResponse result = JsonConvert.DeserializeObject<AdminResponse>(json);

                    if (result != null && result.success)
                    {
                        adminList = result.data ?? new List<AdminItem>();

                        dataGridView_admin.DataSource = null;
                        dataGridView_admin.AutoGenerateColumns = true;
                        dataGridView_admin.DataSource = adminList;

                        FixAdminGridColumns();
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to load admins.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load admins error: " + ex.Message);
            }
        }
        
        private void FixAdminGridColumns()
        {
            dataGridView_admin.ColumnHeadersVisible = true;
            dataGridView_admin.RowHeadersVisible = false;
            dataGridView_admin.AllowUserToAddRows = false;
            dataGridView_admin.ReadOnly = true;
            dataGridView_admin.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_admin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dataGridView_admin.Columns.Contains("Id"))
                dataGridView_admin.Columns["Id"].HeaderText = "ID";

            if (dataGridView_admin.Columns.Contains("firstName"))
                dataGridView_admin.Columns["firstName"].HeaderText = "First Name";

            if (dataGridView_admin.Columns.Contains("lastName"))
                dataGridView_admin.Columns["lastName"].HeaderText = "Last Name";

            if (dataGridView_admin.Columns.Contains("emailAddress"))
                dataGridView_admin.Columns["emailAddress"].HeaderText = "Email Address";
        }
        private async void button_add_Click(object sender, EventArgs e)
        {
            string firstName = textBox_adminFname.Text.Trim();
            string lastName = textBox_adminLname.Text.Trim();
            string email = textBox_adminEmail.Text.Trim();
            string password = textBox_adminPassword.Text.Trim();
            string confirmPassword = textBox_adminConfirmPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Please fill in all required fields.",
                    "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Please enter a valid email address.",
                    "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters.",
                    "Weak Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Password and confirm password do not match.",
                    "Password Mismatch", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to add this admin?",
                "Confirm Add Admin",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
            {
                { "firstName", firstName },
                { "lastName", lastName },
                { "emailAddress", email },
                { "password", password },
                { "confirmPassword", confirmPassword }
            };

                    var content = new FormUrlEncodedContent(values);

                    HttpResponseMessage response = await client.PostAsync(
                        "http://localhost/Student-Attendance-System01-main/api/add_admin.php",
                        content
                    );

                    string json = await response.Content.ReadAsStringAsync();

                    ApiMessageResponse result =
                        JsonConvert.DeserializeObject<ApiMessageResponse>(json);

                    if (result != null && result.success)
                    {
                        MessageBox.Show(result.message,
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearAdminFields();
                        await LoadAdmins();
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to add admin.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add admin error: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void ClearAdminFields()
        {
       

            textBox_adminFname.Text = "";
            textBox_adminLname.Text = "";
            textBox_adminEmail.Text = "";
            textBox_adminPassword.Text = "";
            textBox_adminConfirmPassword.Text = "";

            textBox_adminFname.Focus();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ClearAdminFields();
        }
    }
}