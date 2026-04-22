using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transparent_Form
{
    public partial class ManageStudentForm : Form
    {
        private int selectedStudentId = 0;
        private List<StudentItem> studentList = new List<StudentItem>();

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

        private async Task LoadStudentsFromApi()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (string.IsNullOrEmpty(SessionManager.Token))
                    {
                        MessageBox.Show("No token found. Please login again.",
                            "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.Token);

                    string url = "http://localhost/Student-Attendance-System01-main/api/students.php";
                    HttpResponseMessage response = await client.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("HTTP Error: " + response.StatusCode + "\n\n" + json);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show("Invalid server response:\n\n" + json);
                        return;
                    }

                    StudentApiResponse result = JsonConvert.DeserializeObject<StudentApiResponse>(json);

                    if (result != null && result.success)
                    {
                        studentList = result.data ?? new List<StudentItem>();

                        DataGridView_student.DataSource = null;
                        DataGridView_student.DataSource = studentList;
                        DataGridView_student.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        DataGridView_student.ReadOnly = true;
                        DataGridView_student.AllowUserToAddRows = false;
                        DataGridView_student.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to load students.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection error: " + ex.Message);
                }
            }
        }

        private async Task LoadClasses()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (string.IsNullOrEmpty(SessionManager.Token))
                    {
                        MessageBox.Show("No token found. Please login again.");
                        return;
                    }

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.Token);

                    string url = "http://localhost/Student-Attendance-System01-main/api/classes.php";
                    HttpResponseMessage response = await client.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode || json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show("Failed to load classes.\n\n" + json);
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
                        MessageBox.Show(result?.message ?? "Failed to load classes.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading classes: " + ex.Message);
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
                        MessageBox.Show("No token found. Please login again.");
                        return;
                    }

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.Token);

                    string url = "http://localhost/Student-Attendance-System01-main/api/classarms.php";
                    HttpResponseMessage response = await client.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode || json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show("Failed to load class arms.\n\n" + json);
                        return;
                    }

                    ClassArmApiResponse result = JsonConvert.DeserializeObject<ClassArmApiResponse>(json);

                    if (result != null && result.success)
                    {
                        comboBox_classArm.DataSource = null;
                        comboBox_classArm.DataSource = result.data;
                        comboBox_classArm.DisplayMember = "classArmName";
                        comboBox_classArm.ValueMember = "Id";
                        comboBox_classArm.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to load class arms.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading class arms: " + ex.Message);
                }
            }
        }

      
        private async void button_update_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (selectedStudentId == 0)
                    {
                        MessageBox.Show("Please double-click a student first.");
                        return;
                    }

                    if (string.IsNullOrEmpty(SessionManager.Token))
                    {
                        MessageBox.Show("No token found. Please login again.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(textBox_firstName.Text) ||
                        string.IsNullOrWhiteSpace(textBox_lastName.Text) ||
                        string.IsNullOrWhiteSpace(textBox_admissionNumber.Text) ||
                        comboBox_class.SelectedValue == null ||
                        comboBox_classArm.SelectedValue == null)
                    {
                        MessageBox.Show("Please fill in all required fields.");
                        return;
                    }

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.Token);

                    var values = new Dictionary<string, string>
                    {
                        { "id", selectedStudentId.ToString() },
                        { "firstName", textBox_firstName.Text.Trim() },
                        { "lastName", textBox_lastName.Text.Trim() },
                        { "otherName", textBox_otherName.Text.Trim() },
                        { "admissionNumber", textBox_admissionNumber.Text.Trim() },
                        { "classId", comboBox_class.SelectedValue.ToString() },
                        { "classArmId", comboBox_classArm.SelectedValue.ToString() }
                    };

                    var content = new FormUrlEncodedContent(values);

                    string url = "http://localhost/Student-Attendance-System01-main/api/update_student.php";
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("HTTP Error: " + response.StatusCode + "\n\n" + json);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show("Invalid server response:\n\n" + json);
                        return;
                    }

                    ApiMessageResponse result = JsonConvert.DeserializeObject<ApiMessageResponse>(json);

                    if (result != null && result.success)
                    {
                        MessageBox.Show(result.message);
                        ClearFields();
                        await LoadStudentsFromApi();
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to update student.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private async void button_delete_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox_search_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void DataGridView_student_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        

        private void ClearFields()
        {
            selectedStudentId = 0;
            textBox_firstName.Text = "";
            textBox_lastName.Text = "";
            textBox_otherName.Text = "";
            textBox_admissionNumber.Text = "";
            textBox_search.Text = "";
            comboBox_class.SelectedIndex = -1;
            comboBox_classArm.SelectedIndex = -1;
            textBox_firstName.Focus();
        }

        private void button_clear_Click_1(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void DataGridView_student_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (DataGridView_student.CurrentRow != null)
            {
                int.TryParse(DataGridView_student.CurrentRow.Cells["Id"].Value?.ToString(), out selectedStudentId);

                textBox_firstName.Text = DataGridView_student.CurrentRow.Cells["firstName"].Value?.ToString();
                textBox_lastName.Text = DataGridView_student.CurrentRow.Cells["lastName"].Value?.ToString();
                textBox_otherName.Text = DataGridView_student.CurrentRow.Cells["otherName"].Value?.ToString();
                textBox_admissionNumber.Text = DataGridView_student.CurrentRow.Cells["admissionNumber"].Value?.ToString();
            }
        }

        private void textBox_search_TextChanged_1(object sender, EventArgs e)
        {
            string keyword = textBox_search.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                DataGridView_student.DataSource = null;
                DataGridView_student.DataSource = studentList;
                return;
            }

            var filtered = studentList.FindAll(x =>
                (!string.IsNullOrEmpty(x.firstName) && x.firstName.ToLower().Contains(keyword)) ||
                (!string.IsNullOrEmpty(x.lastName) && x.lastName.ToLower().Contains(keyword)) ||
                (!string.IsNullOrEmpty(x.admissionNumber) && x.admissionNumber.ToLower().Contains(keyword))
            );

            DataGridView_student.DataSource = null;
            DataGridView_student.DataSource = filtered;
        }

        private async void button_delete_Click_1(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (selectedStudentId == 0)
                    {
                        MessageBox.Show("Please double-click a student first.");
                        return;
                    }

                    if (string.IsNullOrEmpty(SessionManager.Token))
                    {
                        MessageBox.Show("No token found. Please login again.");
                        return;
                    }

                    DialogResult confirm = MessageBox.Show(
                        "Are you sure you want to delete this student?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (confirm != DialogResult.Yes)
                        return;

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.Token);

                    var values = new Dictionary<string, string>
                    {
                        { "id", selectedStudentId.ToString() }
                    };

                    var content = new FormUrlEncodedContent(values);

                    string url = "http://localhost/Student-Attendance-System01-main/api/delete_student.php";
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("HTTP Error: " + response.StatusCode + "\n\n" + json);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show("Invalid server response:\n\n" + json);
                        return;
                    }

                    ApiMessageResponse result = JsonConvert.DeserializeObject<ApiMessageResponse>(json);

                    if (result != null && result.success)
                    {
                        MessageBox.Show(result.message);
                        ClearFields();
                        await LoadStudentsFromApi();
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to delete student.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private async void button_add_Click(object sender, EventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (string.IsNullOrEmpty(SessionManager.Token))
                    {
                        MessageBox.Show("No token found. Please login again.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(textBox_firstName.Text) ||
                        string.IsNullOrWhiteSpace(textBox_lastName.Text) ||
                        string.IsNullOrWhiteSpace(textBox_admissionNumber.Text) ||
                        comboBox_class.SelectedValue == null ||
                        comboBox_classArm.SelectedValue == null)
                    {
                        MessageBox.Show("Please fill in all required fields.");
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

                    string url = "http://localhost/Student-Attendance-System01-main/api/add_student.php";
                    HttpResponseMessage response = await client.PostAsync(url, content);
                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("HTTP Error: " + response.StatusCode + "\n\n" + json);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show("Invalid server response:\n\n" + json);
                        return;
                    }

                    ApiMessageResponse result = JsonConvert.DeserializeObject<ApiMessageResponse>(json);

                    if (result != null && result.success)
                    {
                        MessageBox.Show(result.message);
                        ClearFields();
                        await LoadStudentsFromApi();
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to add student.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection error: " + ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

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


}