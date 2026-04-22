using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

                    if (string.IsNullOrWhiteSpace(textBox_fname.Text) ||
                        string.IsNullOrWhiteSpace(textBox_lname.Text) ||
                        string.IsNullOrWhiteSpace(textBox_email.Text) ||
                        string.IsNullOrWhiteSpace(textBox_password.Text))
                    {
                        MessageBox.Show("Please fill in all required fields.");
                        return;
                    }

                    if (comboBox_class.SelectedValue == null)
                    {
                        MessageBox.Show("Please select a class.");
                        return;
                    }

                    if (comboBox_classArm.SelectedValue == null)
                    {
                        MessageBox.Show("Please select a class arm.");
                        return;
                    }

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.Token);

                    var values = new Dictionary<string, string>
                    {
                        { "firstName", textBox_fname.Text.Trim() },
                        { "lastName", textBox_lname.Text.Trim() },
                        { "emailAddress", textBox_email.Text.Trim() },
                        { "password", textBox_password.Text.Trim() },
                        { "classId", comboBox_class.SelectedValue.ToString() },
                        { "classArmId", comboBox_classArm.SelectedValue.ToString() }
                    };

                    var response = await client.PostAsync(
                        "http://localhost/Student-Attendance-System01-main/api/add_teacher.php",
                        new FormUrlEncodedContent(values)
                    );

                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode || string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show("Add teacher failed.\n\n" + json);
                        return;
                    }

                    ApiMessageResponse result = JsonConvert.DeserializeObject<ApiMessageResponse>(json);

                    if (result != null && result.success)
                    {
                        MessageBox.Show(result.message);
                        ClearFields();
                        await LoadTeachers();
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to add teacher.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Add teacher error: " + ex.Message);
                }
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
            textBox_password.Text = "";
           
            comboBox_class.SelectedIndex = -1;
            comboBox_classArm.SelectedIndex = -1;
            textBox_fname.Focus();
        }

        private void dataGridView_teacher_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_teacher.CurrentRow != null)
            {
                int.TryParse(dataGridView_teacher.CurrentRow.Cells["Id"].Value?.ToString(), out selectedTeacherId);

                textBox_fname.Text = dataGridView_teacher.CurrentRow.Cells["firstName"].Value?.ToString();
                textBox_lname.Text = dataGridView_teacher.CurrentRow.Cells["lastName"].Value?.ToString();
                textBox_email.Text = dataGridView_teacher.CurrentRow.Cells["emailAddress"].Value?.ToString();

                string gridClassName = dataGridView_teacher.CurrentRow.Cells["className"].Value?.ToString();
                string gridClassArmName = dataGridView_teacher.CurrentRow.Cells["classArmName"].Value?.ToString();

                if (!string.IsNullOrEmpty(gridClassName))
                    comboBox_class.Text = gridClassName;

                if (!string.IsNullOrEmpty(gridClassArmName))
                    comboBox_classArm.Text = gridClassArmName;
            }
        }

        private void textBox_search_TextChanged_1(object sender, EventArgs e)
        {
            string keyword = textBox_search.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                dataGridView_teacher.DataSource = null;
                dataGridView_teacher.DataSource = teacherList;
                return;
            }

            var filtered = teacherList.FindAll(x =>
                (!string.IsNullOrEmpty(x.firstName) && x.firstName.ToLower().Contains(keyword)) ||
                (!string.IsNullOrEmpty(x.lastName) && x.lastName.ToLower().Contains(keyword)) ||
                (!string.IsNullOrEmpty(x.emailAddress) && x.emailAddress.ToLower().Contains(keyword))
            );

            dataGridView_teacher.DataSource = null;
            dataGridView_teacher.DataSource = filtered;
        }

        private async void button_delete_Click_1(object sender, EventArgs e)
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (selectedTeacherId == 0)
                    {
                        MessageBox.Show("Please double-click a teacher first.");
                        return;
                    }

                    if (string.IsNullOrEmpty(SessionManager.Token))
                    {
                        MessageBox.Show("No token found. Please login again.");
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

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.Token);

                    var values = new Dictionary<string, string>
                    {
                        { "id", selectedTeacherId.ToString() }
                    };

                    var response = await client.PostAsync(
                        "http://localhost/Student-Attendance-System01-main/api/delete_teacher.php",
                        new FormUrlEncodedContent(values)
                    );

                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode || string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show("Delete teacher failed.\n\n" + json);
                        return;
                    }

                    ApiMessageResponse result = JsonConvert.DeserializeObject<ApiMessageResponse>(json);

                    if (result != null && result.success)
                    {
                        MessageBox.Show("Teacher deleted.");
                        ClearFields();
                        await LoadTeachers();
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to delete teacher.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Delete teacher error: " + ex.Message);
                }
            }
        }

        private async void button_update_Click(object sender, EventArgs e)
        {

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    if (selectedTeacherId == 0)
                    {
                        MessageBox.Show("Please double-click a teacher first.");
                        return;
                    }

                    if (string.IsNullOrEmpty(SessionManager.Token))
                    {
                        MessageBox.Show("No token found. Please login again.");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(textBox_fname.Text) ||
                        string.IsNullOrWhiteSpace(textBox_lname.Text) ||
                        string.IsNullOrWhiteSpace(textBox_email.Text))
                    {
                        MessageBox.Show("Please fill in required fields.");
                        return;
                    }

                    if (comboBox_class.SelectedValue == null)
                    {
                        MessageBox.Show("Please select a class.");
                        return;
                    }

                    if (comboBox_classArm.SelectedValue == null)
                    {
                        MessageBox.Show("Please select a class arm.");
                        return;
                    }

                    client.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue("Bearer", SessionManager.Token);

                    var values = new Dictionary<string, string>
            {
                { "id", selectedTeacherId.ToString() },
                { "firstName", textBox_fname.Text.Trim() },
                { "lastName", textBox_lname.Text.Trim() },
                { "emailAddress", textBox_email.Text.Trim() },
                { "classId", comboBox_class.SelectedValue.ToString() },
                { "classArmId", comboBox_classArm.SelectedValue.ToString() }
            };

                    var response = await client.PostAsync(
                        "http://localhost/Student-Attendance-System01-main/api/update_teacher.php",
                        new FormUrlEncodedContent(values)
                    );

                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode || string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show("Update teacher failed.\n\n" + json);
                        return;
                    }

                    ApiMessageResponse result = JsonConvert.DeserializeObject<ApiMessageResponse>(json);

                    if (result != null && result.success)
                    {
                        MessageBox.Show(result.message);
                        ClearFields();
                        await LoadTeachers();
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to update teacher.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Update teacher error: " + ex.Message);
                }
            }
        }

        private async void ManageTeacherForm_Load_1(object sender, EventArgs e)
        {
            await LoadClasses();
            await LoadClassArms();
            await LoadTeachers();
        }
    }

    public class TeacherItem
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string emailAddress { get; set; }
        public string className { get; set; }
        public string classArmName { get; set; }
    }

    public class TeacherResponse
    {
        public bool success { get; set; }
        public List<TeacherItem> data { get; set; }
    }

    public class ClassArmApiResponse
    {
        public bool success { get; set; }
        public List<ClassArmItem> data { get; set; }
        public string message { get; set; }
    }
}