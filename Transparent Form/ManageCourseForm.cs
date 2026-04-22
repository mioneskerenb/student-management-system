using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transparent_Form
{
    public partial class ManageCourseForm : Form
    {
        CourseClass course = new CourseClass();

        public ManageCourseForm()
        {
            InitializeComponent();
        }

        private async void ManageCourseForm_Load(object sender, EventArgs e)
        {
            await LoadClassesFromApi();
        }

        private async Task LoadClassesFromApi()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (string.IsNullOrEmpty(SessionManager.Token))
                    {
                        MessageBox.Show(
                            "No token found. Please login again.",
                            "Authentication Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.Token);

                    string url = "http://localhost/Student-Attendance-System01-main/api/classes.php";

                    HttpResponseMessage response = await client.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(
                            "HTTP Error: " + response.StatusCode + "\n\nResponse:\n" + json,
                            "Server Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(json))
                    {
                        MessageBox.Show(
                            "The server returned an empty response.",
                            "Server Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                    }

                    if (json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show(
                            "The API returned HTML instead of JSON.\n\nResponse:\n" + json,
                            "Server Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                    }

                    ClassApiResponse result = JsonConvert.DeserializeObject<ClassApiResponse>(json);

                    if (result != null && result.success)
                    {
                        DataGridView_class.DataSource = result.data;
                        DataGridView_class.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        DataGridView_class.ReadOnly = true;
                        DataGridView_class.AllowUserToAddRows = false;
                        DataGridView_class.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    }
                    else
                    {
                        MessageBox.Show(
                            result?.message ?? "Failed to load classes.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Connection error: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private async void button_save_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (string.IsNullOrEmpty(SessionManager.Token))
                    {
                        MessageBox.Show(
                            "No token found. Please login again.",
                            "Authentication Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(textBox_className.Text))
                    {
                        MessageBox.Show(
                            "Please enter class name.",
                            "Validation",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );
                        return;
                    }

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.Token);

                    var values = new Dictionary<string, string>
                    {
                        { "className", textBox_className.Text.Trim() }
                    };

                    var content = new FormUrlEncodedContent(values);

                    string url = "http://localhost/Student-Attendance-System01-main/api/add_class.php";

                    HttpResponseMessage response = await client.PostAsync(url, content);
                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show(
                            "HTTP Error: " + response.StatusCode + "\n\nResponse:\n" + json,
                            "Server Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(json))
                    {
                        MessageBox.Show(
                            "The server returned an empty response.",
                            "Server Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                    }

                    if (json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show(
                            "The API returned HTML instead of JSON.\n\nResponse:\n" + json,
                            "Server Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                    }

                    ApiMessageResponse result = JsonConvert.DeserializeObject<ApiMessageResponse>(json);

                    if (result != null && result.success)
                    {
                        MessageBox.Show(
                            result.message,
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        ClearFields();
                        await LoadClassesFromApi();
                    }
                    else
                    {
                        MessageBox.Show(
                            result?.message ?? "Failed to add class.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Connection error: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void DataGridView_class_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridView_class.CurrentRow != null)
            {
                textBox_className.Text = DataGridView_class.CurrentRow.Cells["className"].Value?.ToString();
            }
        }

        private void ClearFields()
        {
            textBox_className.Text = "";
            textBox_className.Focus();
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
    }

    public class ClassApiResponse
    {
        public bool success { get; set; }
        public List<ClassItem> data { get; set; }
        public string message { get; set; }
    }

    

  
}