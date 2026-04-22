using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Transparent_Form
{
    public partial class CourseForm : Form
    {
        CourseClass course = new CourseClass();
        public CourseForm()
        {
            InitializeComponent();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
           
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            
           
        }

        private async void CourseForm_Load(object sender, EventArgs e)
        {
            await LoadClasses();
            await LoadClassArms();
        }

        private async Task LoadClasses()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (string.IsNullOrEmpty(SessionManager.Token))
                    {
                        MessageBox.Show("No token found. Please login again.",
                            "Authentication Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.Token);

                    string url = "http://localhost/Student-Attendance-System01-main/api/classes.php";
                    HttpResponseMessage response = await client.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("HTTP Error: " + response.StatusCode + "\n\nResponse:\n" + json,
                            "Server Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show("Invalid response from classes API.\n\n" + json,
                            "Server Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    ClassResponse result = JsonConvert.DeserializeObject<ClassResponse>(json);

                    if (result != null && result.success)
                    {
                        comboBox_class.DataSource = null;
                        comboBox_class.DataSource = result.data;
                        comboBox_class.DisplayMember = "className";
                        comboBox_class.ValueMember = "Id";
                        comboBox_class.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to load classes.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading classes: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private async Task LoadClassArms()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (string.IsNullOrEmpty(SessionManager.Token))
                    {
                        MessageBox.Show("No token found. Please login again.",
                            "Authentication Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.Token);

                    string url = "http://localhost/Student-Attendance-System01-main/api/classarms.php";
                    HttpResponseMessage response = await client.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("HTTP Error: " + response.StatusCode + "\n\nResponse:\n" + json,
                            "Server Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show("Invalid response from class arm API.\n\n" + json,
                            "Server Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    ClassArmApiResponse result = JsonConvert.DeserializeObject<ClassArmApiResponse>(json);

                    if (result != null && result.success)
                    {
                        DataGridView_classArm.DataSource = result.data;
                        DataGridView_classArm.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        DataGridView_classArm.ReadOnly = true;
                        DataGridView_classArm.AllowUserToAddRows = false;
                        DataGridView_classArm.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to load class arms.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading class arms: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void showData()
        {
            //to show course list on datagridview
            DataGridView_classArm.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM `course`"));
        }

        private async void button_saveClassArm_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (string.IsNullOrEmpty(SessionManager.Token))
                    {
                        MessageBox.Show("No token found. Please login again.",
                            "Authentication Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    if (comboBox_class.SelectedValue == null || string.IsNullOrWhiteSpace(textBox_classArmName.Text))
                    {
                        MessageBox.Show("Please select a class and enter a class arm name.",
                            "Validation",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.Token);

                    var values = new Dictionary<string, string>
                    {
                        { "classId", comboBox_class.SelectedValue.ToString() },
                        { "classArmName", textBox_classArmName.Text.Trim() }
                    };

                    var content = new FormUrlEncodedContent(values);

                    string url = "http://localhost/Student-Attendance-System01-main/api/add_classarm.php";
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("HTTP Error: " + response.StatusCode + "\n\nResponse:\n" + json,
                            "Server Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show("Invalid response from add class arm API.\n\n" + json,
                            "Server Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                    }

                    ApiMessageResponse result = JsonConvert.DeserializeObject<ApiMessageResponse>(json);

                    if (result != null && result.success)
                    {
                        MessageBox.Show(result.message,
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                     
                        await LoadClassArms();
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to add class arm.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving class arm: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }


        }

        public class ClassArmApiResponse
        {
            public bool success { get; set; }
            public List<ClassArmItem> data { get; set; }
            public string message { get; set; }
        }
    }
}
