using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Transparent_Form
{
    public partial class ManageStudentForm : Form
    {
        public ManageStudentForm()
        {
            InitializeComponent();
        }

        private async void ManageStudentForm_Load(object sender, EventArgs e)
        {
            await LoadClasses();
            await LoadClassArms();
            await LoadStudentsFromApi();
        }

        private async System.Threading.Tasks.Task LoadStudentsFromApi()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (string.IsNullOrEmpty(SessionManager.Token))
                    {
                        MessageBox.Show("No token found. Please login again.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.Token);

                    string url = "http://localhost/Student-Attendance-System01-main/Student-Attendance-System01-main/api/students.php";

                    HttpResponseMessage response = await client.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();

                    StudentApiResponse result = JsonConvert.DeserializeObject<StudentApiResponse>(json);

                    if (result != null && result.success)
                    {
                        DataGridView_student.DataSource = result.data;
                        DataGridView_student.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        DataGridView_student.ReadOnly = true;
                        DataGridView_student.AllowUserToAddRows = false;
                        DataGridView_student.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to load students.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async System.Threading.Tasks.Task LoadClasses()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (string.IsNullOrEmpty(SessionManager.Token))
                    {
                        return;
                    }

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.Token);

                    string url = "http://localhost/Student-Attendance-System01-main/Student-Attendance-System01-main/api/classes.php";
                    HttpResponseMessage response = await client.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();

                    ClassResponse result = JsonConvert.DeserializeObject<ClassResponse>(json);

                    if (result != null && result.success)
                    {
                        comboBox_class.DataSource = result.data;
                        comboBox_class.DisplayMember = "className";
                        comboBox_class.ValueMember = "Id";
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to load classes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load classes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async System.Threading.Tasks.Task LoadClassArms()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (string.IsNullOrEmpty(SessionManager.Token))
                    {
                        return;
                    }

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.Token);

                    string url = "http://localhost/Student-Attendance-System01-main/Student-Attendance-System01-main/api/classarms.php";
                    HttpResponseMessage response = await client.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();

                    ClassArmResponse result = JsonConvert.DeserializeObject<ClassArmResponse>(json);

                    if (result != null && result.success)
                    {
                        comboBox_classArm.DataSource = result.data;
                        comboBox_classArm.DisplayMember = "classArmName";
                        comboBox_classArm.ValueMember = "Id";
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to load class arms.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load class arms: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void button_refresh_Click(object sender, EventArgs e)
        {
            await LoadStudentsFromApi();
        }

        private async void button_add_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (string.IsNullOrEmpty(SessionManager.Token))
                    {
                        MessageBox.Show("No token found. Please login again.", "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(textBox_firstName.Text) ||
                        string.IsNullOrWhiteSpace(textBox_lastName.Text) ||
                        string.IsNullOrWhiteSpace(textBox_admissionNumber.Text) ||
                        comboBox_class.SelectedValue == null ||
                        comboBox_classArm.SelectedValue == null)
                    {
                        MessageBox.Show("Please fill in all required fields.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.Token);

                    var values = new Dictionary<string, string>
                    {
                        { "firstName", textBox_firstName.Text.Trim() },
                        { "lastName", textBox_lastName.Text.Trim() },
                        { "otherName", textBox_otherName.Text.Trim() },
                        { "admissionNumber", textBox_admissionNumber.Text.Trim() },
                        { "classId", comboBox_class.SelectedValue.ToString() },
                        { "classArmId", comboBox_classArm.SelectedValue.ToString() }
                    };

                    var content = new FormUrlEncodedContent(values);

                    string url = "http://localhost/Student-Attendance-System01-main/Student-Attendance-System01-main/api/add_student.php";

                    HttpResponseMessage response = await client.PostAsync(url, content);
                    string json = await response.Content.ReadAsStringAsync();

                    ApiMessageResponse result = JsonConvert.DeserializeObject<ApiMessageResponse>(json);

                    if (result != null && result.success)
                    {
                        MessageBox.Show(result.message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        await LoadStudentsFromApi();
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to add student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DataGridView_student_Click(object sender, EventArgs e)
        {
            if (DataGridView_student.CurrentRow != null)
            {
                textBox_id.Text = DataGridView_student.CurrentRow.Cells["Id"].Value?.ToString();
                textBox_firstName.Text = DataGridView_student.CurrentRow.Cells["firstName"].Value?.ToString();
                textBox_lastName.Text = DataGridView_student.CurrentRow.Cells["lastName"].Value?.ToString();
                textBox_otherName.Text = DataGridView_student.CurrentRow.Cells["otherName"].Value?.ToString();
                textBox_admissionNumber.Text = DataGridView_student.CurrentRow.Cells["admissionNumber"].Value?.ToString();
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            textBox_id.Clear();
            textBox_firstName.Clear();
            textBox_lastName.Clear();
            textBox_otherName.Clear();
            textBox_admissionNumber.Clear();

            if (comboBox_class.Items.Count > 0)
                comboBox_class.SelectedIndex = 0;

            if (comboBox_classArm.Items.Count > 0)
                comboBox_classArm.SelectedIndex = 0;

            textBox_firstName.Focus();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Update feature is next.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Delete feature is next.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public class StudentApiResponse
    {
        public bool success { get; set; }
        public List<StudentItem> data { get; set; }
        public string message { get; set; }
    }

    public class StudentItem
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string otherName { get; set; }
        public string admissionNumber { get; set; }
        public string className { get; set; }
        public string classArmName { get; set; }
        public string dateCreated { get; set; }
    }

    public class ApiMessageResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
    }

    public class ClassResponse
    {
        public bool success { get; set; }
        public List<ClassItem> data { get; set; }
        public string message { get; set; }
    }

    public class ClassItem
    {
        public int Id { get; set; }
        public string className { get; set; }
    }

    public class ClassArmResponse
    {
        public bool success { get; set; }
        public List<ClassArmItem> data { get; set; }
        public string message { get; set; }
    }

    public class ClassArmItem
    {
        public int Id { get; set; }
        public string classArmName { get; set; }
    }
}