using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transparent_Form
{
    public partial class ScoreForm : Form
    {
        private int selectedId = 0;

        // CHANGE THIS to your real project folder in XAMPP
        private readonly string apiBaseUrl = "http://localhost/Student-Attendance-System01-main/api/";

        public ScoreForm()
        {
            InitializeComponent();
        }

        

        private async Task LoadTerms()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = apiBaseUrl + "getTerms.php";
                    string json = await client.GetStringAsync(url);

                    TermApiResponse result = JsonConvert.DeserializeObject<TermApiResponse>(json);

                    if (result != null && result.success && result.data != null)
                    {
                        comboBox_term.DataSource = result.data;
                        comboBox_term.DisplayMember = "termName";
                        comboBox_term.ValueMember = "Id";
                        comboBox_term.SelectedIndex = -1;
                    }
                    else
                    {
                        MessageBox.Show("Failed to load terms.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading terms: " + ex.Message);
            }
        }

        private async Task LoadSessionTerms()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = apiBaseUrl + "getSessionTerm.php";
                    string json = await client.GetStringAsync(url);

                    SessionTermApiResponse result = JsonConvert.DeserializeObject<SessionTermApiResponse>(json);

                    if (result != null && result.success && result.data != null)
                    {
                        dataGridView_sessionTerm.DataSource = null;
                        dataGridView_sessionTerm.AutoGenerateColumns = true;
                        dataGridView_sessionTerm.DataSource = result.data;

                        dataGridView_sessionTerm.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dataGridView_sessionTerm.MultiSelect = false;
                        dataGridView_sessionTerm.ReadOnly = true;
                        dataGridView_sessionTerm.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        if (dataGridView_sessionTerm.Columns["Id"] != null)
                            dataGridView_sessionTerm.Columns["Id"].Visible = false;

                        if (dataGridView_sessionTerm.Columns["termId"] != null)
                            dataGridView_sessionTerm.Columns["termId"].Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Failed to load session and term records.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading session and term records: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            textBox_sessionName.Clear();
            comboBox_term.SelectedIndex = -1;
            selectedId = 0;

            button_save.Enabled = true;
            button_update.Enabled = false;
            button_activate.Enabled = false;
            button_delete.Enabled = false;
        }

       

      
       

     
        private void button_clear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private async void button_activate_Click_1(object sender, EventArgs e)
        {
            if (selectedId == 0)
            {
                MessageBox.Show("Please select a record first.");
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var values = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("id", selectedId.ToString())
                    });

                    string url = apiBaseUrl + "activateSessionTerm.php";
                    HttpResponseMessage response = await client.PostAsync(url, values);
                    string json = await response.Content.ReadAsStringAsync();

                    BasicApiResponse result = JsonConvert.DeserializeObject<BasicApiResponse>(json);

                    if (result != null && result.success)
                    {
                        MessageBox.Show("Session and term activated successfully.");
                        await LoadSessionTerms();
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to activate session and term.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error activating session and term: " + ex.Message);
            }
        }

        private async void ScoreForm_Load(object sender, EventArgs e)
        {
            await LoadTerms();
            await LoadSessionTerms();
            ClearFields();
        }

        private async void button_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_sessionName.Text))
            {
                MessageBox.Show("Please enter session name.");
                return;
            }

            if (comboBox_term.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a term.");
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var values = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("sessionName", textBox_sessionName.Text.Trim()),
                        new KeyValuePair<string, string>("termId", comboBox_term.SelectedValue.ToString())
                    });

                    string url = apiBaseUrl + "addSessionTerm.php";
                    HttpResponseMessage response = await client.PostAsync(url, values);
                    string json = await response.Content.ReadAsStringAsync();

                    BasicApiResponse result = JsonConvert.DeserializeObject<BasicApiResponse>(json);

                    if (result != null && result.success)
                    {
                        MessageBox.Show("Session and term saved successfully.");
                        await LoadSessionTerms();
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to save session and term.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving session and term: " + ex.Message);
            }
        }

        private void dataGridView_sessionTerm_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridView_sessionTerm.Rows[e.RowIndex];

            if (row.Cells["Id"].Value != null)
                selectedId = Convert.ToInt32(row.Cells["Id"].Value);

            if (row.Cells["sessionName"].Value != null)
                textBox_sessionName.Text = row.Cells["sessionName"].Value.ToString();

            if (row.Cells["termId"].Value != null)
                comboBox_term.SelectedValue = row.Cells["termId"].Value;

            button_save.Enabled = false;
            button_update.Enabled = true;
            button_activate.Enabled = true;
            button_delete.Enabled = true;
        }

        private async void button_update_Click(object sender, EventArgs e)
        {
            if (selectedId == 0)
            {
                MessageBox.Show("Please select a record first.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBox_sessionName.Text))
            {
                MessageBox.Show("Please enter session name.");
                return;
            }

            if (comboBox_term.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a term.");
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var values = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("id", selectedId.ToString()),
                        new KeyValuePair<string, string>("sessionName", textBox_sessionName.Text.Trim()),
                        new KeyValuePair<string, string>("termId", comboBox_term.SelectedValue.ToString())
                    });

                    string url = apiBaseUrl + "updateSessionTerm.php";
                    HttpResponseMessage response = await client.PostAsync(url, values);
                    string json = await response.Content.ReadAsStringAsync();

                    BasicApiResponse result = JsonConvert.DeserializeObject<BasicApiResponse>(json);

                    if (result != null && result.success)
                    {
                        MessageBox.Show("Session and term updated successfully.");
                        await LoadSessionTerms();
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to update session and term.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating session and term: " + ex.Message);
            }
        }

        private async void button_delete_Click(object sender, EventArgs e)
        {
            if (selectedId == 0)
            {
                MessageBox.Show("Please select a record first.");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this record?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var values = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("id", selectedId.ToString())
                    });

                    string url = apiBaseUrl + "deleteSessionTerm.php";
                    HttpResponseMessage response = await client.PostAsync(url, values);
                    string json = await response.Content.ReadAsStringAsync();

                    BasicApiResponse result = JsonConvert.DeserializeObject<BasicApiResponse>(json);

                    if (result != null && result.success)
                    {
                        MessageBox.Show("Session and term deleted successfully.");
                        await LoadSessionTerms();
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show(result?.message ?? "Failed to delete session and term.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting session and term: " + ex.Message);
            }
        }
    }

    public class BasicApiResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
    }

    public class TermApiResponse
    {
        public bool success { get; set; }
        public List<TermItem> data { get; set; }
    }

    public class TermItem
    {
        public int Id { get; set; }
        public string termName { get; set; }
    }

    public class SessionTermApiResponse
    {
        public bool success { get; set; }
        public List<SessionTermItem> data { get; set; }
    }

    public class SessionTermItem
    {
        public int Id { get; set; }
        public string sessionName { get; set; }
        public int termId { get; set; }
        public string termName { get; set; }
        public int isActive { get; set; }
        public string statusText { get; set; }
        public string dateCreated { get; set; }
    }
}