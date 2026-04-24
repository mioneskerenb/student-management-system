using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transparent_Form
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            customizeDesign();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await LoadDashboardStats();
        }

        private async Task LoadDashboardStats()
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

                    string url = "http://localhost/Student-Attendance-System01-main/api/dashboard_stats.php";

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

                    if (string.IsNullOrWhiteSpace(json) || json.TrimStart().StartsWith("<"))
                    {
                        MessageBox.Show(
                            "Invalid response from dashboard stats API.\n\n" + json,
                            "Server Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                    }

                    DashboardStatsResponse result = JsonConvert.DeserializeObject<DashboardStatsResponse>(json);

                    if (result != null && result.success && result.data != null)
                    {
                        label_studentsCount.Text = result.data.students.ToString();
                        label_classesCount.Text = result.data.classes.ToString();
                        label_classArmsCount.Text = result.data.classArms.ToString();
                        label_attendanceCount.Text = result.data.totalAttendance.ToString();
                        label_classTeachersCount.Text = result.data.classTeachers.ToString();
                        label_sessionTermsCount.Text = result.data.sessionTerms.ToString();
                        label_termsCount.Text = result.data.terms.ToString();
                    }
                    else
                    {
                        MessageBox.Show(
                            result?.message ?? "Failed to load dashboard statistics.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error loading dashboard statistics: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        private void customizeDesign()
        {
            panel_stdsubmenu.Visible = false;
            panel_courseSubmenu.Visible = false;
            panel_scoreSubmenu.Visible = false;
        }

        private void hideSubmenu()
        {
            if (panel_stdsubmenu.Visible)
                panel_stdsubmenu.Visible = false;

            if (panel_courseSubmenu.Visible)
                panel_courseSubmenu.Visible = false;

            if (panel_scoreSubmenu.Visible)
                panel_scoreSubmenu.Visible = false;
        }

        private void showSubmenu(Panel submenu)
        {
            if (!submenu.Visible)
            {
                hideSubmenu();
                submenu.Visible = true;
            }
            else
            {
                submenu.Visible = false;
            }
        }

        private void button_std_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_stdsubmenu);
        }

        #region StdSubmenu

        private void button_registration_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageTeacherForm());
            hideSubmenu();
        }

        private async void button_manageStd_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageStudentForm());
            hideSubmenu();
            await LoadDashboardStats();
        }

        private void button_status_Click(object sender, EventArgs e)
        {
            hideSubmenu();
        }

        private void button_stdPrint_Click(object sender, EventArgs e)
        {
            openChildForm(new PrintStudent());
            hideSubmenu();
        }

        #endregion

        private void button_course_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_courseSubmenu);
        }

        #region CourseSubmenu

        private async void button_newCourse_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageCourseForm());
            hideSubmenu();
            await LoadDashboardStats();
        }

        private async void button_manageCourse_Click(object sender, EventArgs e)
        {
            openChildForm(new CourseForm());
            hideSubmenu();
            await LoadDashboardStats();
        }

        private void button_coursePrint_Click(object sender, EventArgs e)
        {
            openChildForm(new PrintCourseForm());
            hideSubmenu();
        }

        #endregion

        private void button_score_Click(object sender, EventArgs e)
        {
            showSubmenu(panel_scoreSubmenu);
        }

        #region ScoreSubmenu

        private void button_newScore_Click(object sender, EventArgs e)
        {
            openChildForm(new ScoreForm());
            hideSubmenu();
        }

        private void button_manageScore_Click(object sender, EventArgs e)
        {
            openChildForm(new ManageScoreForm());
            hideSubmenu();
        }

        private void button_scorePrint_Click(object sender, EventArgs e)
        {
            openChildForm(new PrintScoreForm());
            hideSubmenu();
        }

        #endregion

        private Form activeForm = null;

        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_main.Controls.Add(childForm);
            panel_main.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void button_exit_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
                activeForm.Close();

            panel_main.Controls.Add(panel_cover);
            panel_cover.BringToFront();
        }

        private void button_exit_Click_1(object sender, EventArgs e)
        {

            LoginForm login = new LoginForm();
            SessionManager.Token = null;
            SessionManager.UserType = null;
            SessionManager.UserName = null;

        
            this.Hide();
            login.Show();
            this.Hide();
            login.Show();
        }

        private void label12_Click(object sender, EventArgs e)
        {
        }

        private void panel_scoreSubmenu_Paint(object sender, PaintEventArgs e)
        {
        }
    }

    public class DashboardStatsResponse
    {
        public bool success { get; set; }
        public DashboardStatsData data { get; set; }
        public string message { get; set; }
    }

    public class DashboardStatsData
    {
        public int students { get; set; }
        public int classes { get; set; }
        public int classArms { get; set; }
        public int totalAttendance { get; set; }
        public int classTeachers { get; set; }
        public int sessionTerms { get; set; }
        public int terms { get; set; }
    }
}