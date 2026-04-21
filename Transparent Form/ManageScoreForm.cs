using MySql.Data.MySqlClient;
using System;
using System.Data;
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
        }

        private void ManageScoreForm_Load(object sender, EventArgs e)
        {
            // Populate the combobox with courses
            comboBox_course.DataSource = course.getCourse(new MySqlCommand("SELECT * FROM course"));
            comboBox_course.DisplayMember = "CourseName";
            comboBox_course.ValueMember = "CourseId";

            // Show score data
            showScore();
        }

        public void showScore()
        {
            DataGridView_score.DataSource = score.getList(
                new MySqlCommand(
                    "SELECT score.StdId, student.StdFirstName, student.StdLastName, " +
                    "score.CourseId, course.CourseName, score.Score, score.Description " +
                    "FROM score " +
                    "INNER JOIN student ON score.StdId = student.StdId " +
                    "INNER JOIN course ON score.CourseId = course.CourseId"
                )
            );
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            if (textBox_stdId.Text == "" || textBox_score.Text == "")
            {
                MessageBox.Show("Need score data", "Field Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int stdId = Convert.ToInt32(textBox_stdId.Text);
                int courseId = Convert.ToInt32(comboBox_course.SelectedValue);
                double scor = Convert.ToDouble(textBox_score.Text);
                string desc = textBox_description.Text;

                if (score.updateScore(stdId, courseId, scor, desc))
                {
                    showScore();
                    button_clear.PerformClick();
                    MessageBox.Show("Score Edited Complete", "Update Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Score not edited", "Update Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_stdId.Text == "")
            {
                MessageBox.Show("Field Error - we need student id", "Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int stdId = Convert.ToInt32(textBox_stdId.Text);
                int courseId = Convert.ToInt32(comboBox_course.SelectedValue);

                if (MessageBox.Show("Are you sure you want to remove this score", "Delete Score", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (score.deleteScore(stdId, courseId))
                    {
                        showScore();
                        MessageBox.Show("Score Removed", "Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button_clear.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Score not removed", "Delete Score", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_stdId.Clear();
            textBox_score.Clear();
            textBox_description.Clear();
            textBox_search.Clear();

            if (comboBox_course.Items.Count > 0)
            {
                comboBox_course.SelectedIndex = 0;
            }
        }

        private void DataGridView_course_Click(object sender, EventArgs e)
        {
            if (DataGridView_score.CurrentRow != null)
            {
                textBox_stdId.Text = DataGridView_score.CurrentRow.Cells[0].Value.ToString();

                // Cell[3] = CourseId, Cell[4] = CourseName
                comboBox_course.SelectedValue = Convert.ToInt32(DataGridView_score.CurrentRow.Cells[3].Value);

                textBox_score.Text = DataGridView_score.CurrentRow.Cells[5].Value.ToString();
                textBox_description.Text = DataGridView_score.CurrentRow.Cells[6].Value.ToString();
            }
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            MySqlCommand command = new MySqlCommand(
                "SELECT score.StdId, student.StdFirstName, student.StdLastName, " +
                "score.CourseId, course.CourseName, score.Score, score.Description " +
                "FROM score " +
                "INNER JOIN student ON score.StdId = student.StdId " +
                "INNER JOIN course ON score.CourseId = course.CourseId " +
                "WHERE CONCAT(student.StdFirstName, student.StdLastName, course.CourseName) LIKE @search"
            );

            command.Parameters.Add("@search", MySqlDbType.VarChar).Value = "%" + textBox_search.Text + "%";

            DataGridView_score.DataSource = score.getList(command);
        }
    }
}