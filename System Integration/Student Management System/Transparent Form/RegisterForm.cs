using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transparent_Form
{
    public partial class ManageTeacherForm : Form
    {
        private int selectedTeacherId = 0;
        private List<TeacherItem> teacherList = new List<TeacherItem>();

        public ManageTeacherForm()
        {
            InitializeComponent();
            UITheme.ApplyFormTheme(this);
        }

        private async void ManageTeacherForm_Load(object sender, EventArgs e)
        {
          
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

                    var response = await client.GetAsync("http://localhost/Student-Attendance-System01-main/api/classes.php");
                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode || string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show("Failed to load classes.\n\n" + json);
                        return;
                    }

                    ClassResponse result = JsonConvert.DeserializeObject<ClassResponse>(json);

                    if (result != null && result.success && result.data != null)
                    {
                        comboBox_class.DataSource = null;
                        comboBox_class.DataSource = result.data;
                        comboBox_class.DisplayMember = "className";
                        comboBox_class.ValueMember = "Id";
                        comboBox_class.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "No classes found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Load classes error: " + ex.Message);
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

                    var response = await client.GetAsync("http://localhost/Student-Attendance-System01-main/api/classarms.php");
                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode || string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show("Failed to load class arms.\n\n" + json);
                        return;
                    }

                    ClassArmApiResponse result = JsonConvert.DeserializeObject<ClassArmApiResponse>(json);

                    if (result != null && result.success && result.data != null)
                    {
                        comboBox_classArm.DataSource = null;
                        comboBox_classArm.DataSource = result.data;
                        comboBox_classArm.DisplayMember = "classArmName";
                        comboBox_classArm.ValueMember = "Id";
                        comboBox_classArm.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "No class arms found.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Load class arms error: " + ex.Message);
                }
            }
        }

        private async Task LoadTeachers()
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

                    var response = await client.GetAsync("http://localhost/Student-Attendance-System01-main/api/teachers.php");
                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode || string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show("Failed to load teachers.\n\n" + json);
                        return;
                    }

                    TeacherResponse result = JsonConvert.DeserializeObject<TeacherResponse>(json);

                    if (result != null && result.success && result.data != null)
                    {
                        teacherList = result.data;

                        dataGridView_teacher.DataSource = null;
                        dataGridView_teacher.DataSource = teacherList;
                        dataGridView_teacher.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        dataGridView_teacher.ReadOnly = true;
                        dataGridView_teacher.AllowUserToAddRows = false;
                        dataGridView_teacher.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                        dataGridView_teacher.DataSource = result.data;

                        dataGridView_teacher.ColumnHeadersVisible = true;
                        dataGridView_teacher.RowHeadersVisible = false;
                        dataGridView_teacher.AllowUserToAddRows = false;
                        dataGridView_teacher.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dataGridView_teacher.MultiSelect = false;
                        dataGridView_teacher.ReadOnly = true;
                        dataGridView_teacher.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        if (dataGridView_teacher.Columns.Contains("Id"))
                            dataGridView_teacher.Columns["Id"].HeaderText = "ID";

                        if (dataGridView_teacher.Columns.Contains("firstName"))
                            dataGridView_teacher.Columns["firstName"].HeaderText = "First Name";

                        if (dataGridView_teacher.Columns.Contains("lastName"))
                            dataGridView_teacher.Columns["lastName"].HeaderText = "Last Name";

                        if (dataGridView_teacher.Columns.Contains("emailAddress"))
                            dataGridView_teacher.Columns["emailAddress"].HeaderText = "Email Address";

                        if (dataGridView_teacher.Columns.Contains("phoneNo"))
                            dataGridView_teacher.Columns["phoneNo"].HeaderText = "Phone";

                        if (dataGridView_teacher.Columns.Contains("phone"))
                            dataGridView_teacher.Columns["phone"].HeaderText = "Phone";

                        if (dataGridView_teacher.Columns.Contains("className"))
                            dataGridView_teacher.Columns["className"].HeaderText = "Class";

                        if (dataGridView_teacher.Columns.Contains("classArmName"))
                            dataGridView_teacher.Columns["classArmName"].HeaderText = "Class Arm";

                        if (dataGridView_teacher.Columns.Contains("classId"))
                            dataGridView_teacher.Columns["classId"].Visible = false;

                        if (dataGridView_teacher.Columns.Contains("classArmId"))
                            dataGridView_teacher.Columns["classArmId"].Visible = false;

                        if (dataGridView_teacher.Columns.Contains("password"))
                            dataGridView_teacher.Columns["password"].Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Failed to load teachers.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Load teachers error: " + ex.Message);
                }
            }
        }
        private async Task LoadClassArmsByClass(int classId)
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

                    string url = "http://localhost/Student-Attendance-System01-main/api/classarms.php?classId=" + classId;

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
                        comboBox_classArm.Enabled = true;
                    }
                    else
                    {
                        comboBox_classArm.DataSource = null;
                        comboBox_classArm.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading class arms: " + ex.Message);
                }
            }
        }
        private async void button_add_Click(object sender, EventArgs e)
        {
            string firstName = textBox_fname.Text.Trim();
            string lastName = textBox_lname.Text.Trim();
            string email = textBox_email.Text.Trim();
            string phone = textBox_phone.Text.Trim();

            if (comboBox_class.SelectedValue == null || comboBox_classArm.SelectedValue == null)
            {
                MessageBox.Show("Please select class and class arm.",
                    "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int classId = Convert.ToInt32(comboBox_class.SelectedValue);
            int classArmId = Convert.ToInt32(comboBox_classArm.SelectedValue);

            if (
                string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone) ||
                classId == 0 ||
                classArmId == 0
            )
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

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
            {
                { "firstName", firstName },
                { "lastName", lastName },
                { "emailAddress", email },
                { "phoneNo", phone },
                { "classId", classId.ToString() },
                { "classArmId", classArmId.ToString() },
                { "createdBy", "2" }
            };

                    var content = new FormUrlEncodedContent(values);

                    HttpResponseMessage response = await client.PostAsync(
                        "http://localhost/Student-Attendance-System01-main/api/add_teacher.php",
                        content
                    );

                    string json = await response.Content.ReadAsStringAsync();

                    TeacherResponse result = JsonConvert.DeserializeObject<TeacherResponse>(json);

                    if (result != null && result.success)
                    {
                        MessageBox.Show(
                            "Teacher added successfully.\n\nDefault password: pass123",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        ClearFields();
                        await LoadTeachersFromApi();
                    }
                    else
                    {
                        MessageBox.Show(
                            result?.message ?? "Failed to add teacher.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding teacher: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    

        
      
        private void button_clear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            selectedTeacherId = 0;

            textBox_fname.Text = "";
            textBox_lname.Text = "";
            textBox_email.Text = "";
            textBox_phone.Text = "";

            comboBox_class.SelectedIndex = -1;

            comboBox_classArm.DataSource = null;
            comboBox_classArm.Enabled = false;

            textBox_fname.Focus();
        }

        private async void dataGridView_teacher_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = dataGridView_teacher.Rows[e.RowIndex];

            selectedTeacherId = Convert.ToInt32(row.Cells["Id"].Value);

            textBox_fname.Text = row.Cells["firstName"].Value?.ToString();
            textBox_lname.Text = row.Cells["lastName"].Value?.ToString();
            textBox_email.Text = row.Cells["emailAddress"].Value?.ToString();

            if (dataGridView_teacher.Columns.Contains("phoneNo"))
            {
                textBox_phone.Text = row.Cells["phoneNo"].Value?.ToString();
            }
            else if (dataGridView_teacher.Columns.Contains("phone"))
            {
                textBox_phone.Text = row.Cells["phone"].Value?.ToString();
            }
            else
            {
                textBox_phone.Text = "";
            }

            if (row.Cells["classId"].Value != null)
            {
                int classId = Convert.ToInt32(row.Cells["classId"].Value);
                comboBox_class.SelectedValue = classId;

                await LoadClassArmsByClass(classId);

                if (row.Cells["classArmId"].Value != null)
                {
                    comboBox_classArm.SelectedValue = Convert.ToInt32(row.Cells["classArmId"].Value);
                }
            }
        }

        
        private async void button_delete_Click_1(object sender, EventArgs e)
        {

            if (selectedTeacherId <= 0)
            {
                MessageBox.Show("Please select a teacher first.", "No Teacher Selected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this teacher?",
                "Confirm Delete",
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
                { "id", selectedTeacherId.ToString() },
                { "deletedBy", "2" }
            };

                    var content = new FormUrlEncodedContent(values);

                    HttpResponseMessage response = await client.PostAsync(
                        "http://localhost/Student-Attendance-System01-main/api/delete_teacher.php",
                        content
                    );

                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("HTTP Error: " + response.StatusCode + "\n\n" + json,
                            "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show("Invalid server response:\n\n" + json,
                            "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    ApiMessageResponse result = JsonConvert.DeserializeObject<ApiMessageResponse>(json);

                    if (result != null && result.success)
                    {
                        MessageBox.Show(result.message ?? "Teacher deleted successfully.",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearFields();
                        await LoadTeachersFromApi();
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to delete teacher.",
                            "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete teacher error: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button_update_Click(object sender, EventArgs e)
        {

            if (selectedTeacherId <= 0)
            {
                MessageBox.Show("Please select a teacher to update.", "No Teacher Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string firstName = textBox_fname.Text.Trim();
            string lastName = textBox_lname.Text.Trim();
            string email = textBox_email.Text.Trim();
            string phone = textBox_phone.Text.Trim();

            if (comboBox_class.SelectedValue == null || comboBox_classArm.SelectedValue == null)
            {
                MessageBox.Show("Please select class and class arm.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int classId = Convert.ToInt32(comboBox_class.SelectedValue);
            int classArmId = Convert.ToInt32(comboBox_classArm.SelectedValue);

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("All fields required.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
            {
                { "id", selectedTeacherId.ToString() },
                { "firstName", firstName },
                { "lastName", lastName },
                { "emailAddress", email },
                { "phoneNo", phone },
                { "classId", classId.ToString() },
                { "classArmId", classArmId.ToString() },
                { "updatedBy", "2" }
            };

                    var content = new FormUrlEncodedContent(values);

                    HttpResponseMessage response = await client.PostAsync(
                        "http://localhost/Student-Attendance-System01-main/api/update_teacher.php",
                        content
                    );

                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("HTTP Error: " + response.StatusCode + "\n\n" + json);
                        return;
                    }

                    TeacherResponse result = JsonConvert.DeserializeObject<TeacherResponse>(json);

                    if (result != null && result.success)
                    {
                        MessageBox.Show(result.message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                        selectedTeacherId = 0;
                        await LoadTeachers();
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to update teacher.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating teacher: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      private async Task LoadTeachersFromApi()
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

            string url = "http://localhost/Student-Attendance-System01-main/api/teachers.php";

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

            TeacherResponse result = JsonConvert.DeserializeObject<TeacherResponse>(json);

            if (result != null && result.success)
            {
                teacherList = result.data ?? new List<TeacherItem>();

                dataGridView_teacher.DataSource = null;
                dataGridView_teacher.AutoGenerateColumns = true;
                dataGridView_teacher.DataSource = teacherList;

                FixTeacherGridColumns();

                dataGridView_teacher.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView_teacher.ReadOnly = true;
                dataGridView_teacher.AllowUserToAddRows = false;
                dataGridView_teacher.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView_teacher.MultiSelect = false;
            }
            else
            {
                MessageBox.Show(result?.message ?? "Failed to load teachers.");
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error loading teachers: " + ex.Message);
        }
    }
}
        private void FixTeacherGridColumns()
        {
            dataGridView_teacher.ColumnHeadersVisible = true;
            dataGridView_teacher.EnableHeadersVisualStyles = false;
            dataGridView_teacher.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridView_teacher.ColumnHeadersHeight = 45;

            dataGridView_teacher.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 80, 160);
            dataGridView_teacher.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView_teacher.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold);
            dataGridView_teacher.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView_teacher.RowHeadersVisible = false;
            dataGridView_teacher.AllowUserToAddRows = false;
            dataGridView_teacher.ReadOnly = true;
            dataGridView_teacher.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView_teacher.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            if (dataGridView_teacher.Columns.Contains("Id"))
                dataGridView_teacher.Columns["Id"].HeaderText = "ID";

            if (dataGridView_teacher.Columns.Contains("firstName"))
                dataGridView_teacher.Columns["firstName"].HeaderText = "First Name";

            if (dataGridView_teacher.Columns.Contains("lastName"))
                dataGridView_teacher.Columns["lastName"].HeaderText = "Last Name";

            if (dataGridView_teacher.Columns.Contains("emailAddress"))
                dataGridView_teacher.Columns["emailAddress"].HeaderText = "Email Address";

            if (dataGridView_teacher.Columns.Contains("phoneNo"))
                dataGridView_teacher.Columns["phoneNo"].HeaderText = "Phone";

            if (dataGridView_teacher.Columns.Contains("className"))
                dataGridView_teacher.Columns["className"].HeaderText = "Class";

            if (dataGridView_teacher.Columns.Contains("classArmName"))
                dataGridView_teacher.Columns["classArmName"].HeaderText = "Class Arm";

            if (dataGridView_teacher.Columns.Contains("classId"))
                dataGridView_teacher.Columns["classId"].Visible = false;

            if (dataGridView_teacher.Columns.Contains("classArmId"))
                dataGridView_teacher.Columns["classArmId"].Visible = false;

            if (dataGridView_teacher.Columns.Contains("password"))
                dataGridView_teacher.Columns["password"].Visible = false;
        }
        private async void ManageTeacherForm_Load_1(object sender, EventArgs e)
        {
            await LoadClasses();
            await LoadClassArms();
            await LoadTeachers();

            comboBox_classArm.DataSource = null;
            comboBox_classArm.Enabled = false;

            await LoadTeachersFromApi();

         

          
        }

        private async void comboBox_class_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox_class.SelectedValue == null || comboBox_class.SelectedValue is ClassItem)
                return;

            if (int.TryParse(comboBox_class.SelectedValue.ToString(), out int classId))
            {
                comboBox_classArm.DataSource = null;
                comboBox_classArm.Enabled = false;

                await LoadClassArmsByClass(classId);
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