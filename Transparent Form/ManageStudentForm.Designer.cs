
namespace Transparent_Form
{
    partial class ManageStudentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.comboBox_classArm = new System.Windows.Forms.ComboBox();
            this.comboBox_class = new System.Windows.Forms.ComboBox();
            this.button_add = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.DataGridView_student = new System.Windows.Forms.DataGridView();
            this.textBox_admissionNumber = new System.Windows.Forms.TextBox();
            this.textBox_otherName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button_clear = new System.Windows.Forms.Button();
            this.button_search = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button_delete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button_update = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_firstName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_lastName = new System.Windows.Forms.TextBox();
            this.textBox_search = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_student)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_classArm
            // 
            this.comboBox_classArm.FormattingEnabled = true;
            this.comboBox_classArm.Location = new System.Drawing.Point(452, 106);
            this.comboBox_classArm.Name = "comboBox_classArm";
            this.comboBox_classArm.Size = new System.Drawing.Size(149, 31);
            this.comboBox_classArm.TabIndex = 65;
            // 
            // comboBox_class
            // 
            this.comboBox_class.FormattingEnabled = true;
            this.comboBox_class.Location = new System.Drawing.Point(738, 116);
            this.comboBox_class.Name = "comboBox_class";
            this.comboBox_class.Size = new System.Drawing.Size(149, 31);
            this.comboBox_class.TabIndex = 64;
            // 
            // button_add
            // 
            this.button_add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_add.BackColor = System.Drawing.Color.CadetBlue;
            this.button_add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_add.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_add.ForeColor = System.Drawing.Color.White;
            this.button_add.Location = new System.Drawing.Point(143, 159);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(118, 39);
            this.button_add.TabIndex = 63;
            this.button_add.Text = "Add";
            this.button_add.UseVisualStyleBackColor = false;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 10.8F);
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.label10.Location = new System.Drawing.Point(12, 105);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(169, 21);
            this.label10.TabIndex = 62;
            this.label10.Text = "Admission Number :";
            // 
            // DataGridView_student
            // 
            this.DataGridView_student.AllowUserToAddRows = false;
            this.DataGridView_student.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            this.DataGridView_student.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DataGridView_student.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridView_student.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView_student.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.DataGridView_student.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DataGridView_student.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DataGridView_student.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView_student.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.DataGridView_student.ColumnHeadersHeight = 24;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridView_student.DefaultCellStyle = dataGridViewCellStyle6;
            this.DataGridView_student.EnableHeadersVisualStyles = false;
            this.DataGridView_student.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.DataGridView_student.Location = new System.Drawing.Point(12, 261);
            this.DataGridView_student.Name = "DataGridView_student";
            this.DataGridView_student.RowHeadersVisible = false;
            this.DataGridView_student.RowHeadersWidth = 51;
            this.DataGridView_student.RowTemplate.Height = 80;
            this.DataGridView_student.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridView_student.Size = new System.Drawing.Size(910, 340);
            this.DataGridView_student.TabIndex = 42;
            this.DataGridView_student.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_student_CellDoubleClick_1);
            // 
            // textBox_admissionNumber
            // 
            this.textBox_admissionNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_admissionNumber.Location = new System.Drawing.Point(187, 100);
            this.textBox_admissionNumber.Name = "textBox_admissionNumber";
            this.textBox_admissionNumber.Size = new System.Drawing.Size(149, 32);
            this.textBox_admissionNumber.TabIndex = 61;
            // 
            // textBox_otherName
            // 
            this.textBox_otherName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_otherName.Location = new System.Drawing.Point(724, 57);
            this.textBox_otherName.Name = "textBox_otherName";
            this.textBox_otherName.Size = new System.Drawing.Size(186, 32);
            this.textBox_otherName.TabIndex = 60;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 10.8F);
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.label9.Location = new System.Drawing.Point(613, 62);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 21);
            this.label9.TabIndex = 59;
            this.label9.Text = "Other Name :";
            // 
            // button_clear
            // 
            this.button_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_clear.BackColor = System.Drawing.Color.LightSeaGreen;
            this.button_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_clear.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_clear.ForeColor = System.Drawing.Color.White;
            this.button_clear.Location = new System.Drawing.Point(286, 159);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(118, 39);
            this.button_clear.TabIndex = 58;
            this.button_clear.Text = "Clear";
            this.button_clear.UseVisualStyleBackColor = false;
            this.button_clear.Click += new System.EventHandler(this.button_clear_Click_1);
            // 
            // button_search
            // 
            this.button_search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_search.BackColor = System.Drawing.Color.DarkKhaki;
            this.button_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_search.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_search.ForeColor = System.Drawing.Color.White;
            this.button_search.Location = new System.Drawing.Point(769, 3);
            this.button_search.Name = "button_search";
            this.button_search.Size = new System.Drawing.Size(118, 39);
            this.button_search.TabIndex = 57;
            this.button_search.Text = "Search";
            this.button_search.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(934, 51);
            this.panel1.TabIndex = 44;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(370, 13);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(242, 32);
            this.label7.TabIndex = 0;
            this.label7.Text = "Manage Students";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 10.8F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.label6.Location = new System.Drawing.Point(362, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 21);
            this.label6.TabIndex = 54;
            this.label6.Text = "Class Id: ";
            // 
            // button_delete
            // 
            this.button_delete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_delete.BackColor = System.Drawing.Color.Coral;
            this.button_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_delete.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_delete.ForeColor = System.Drawing.Color.White;
            this.button_delete.Location = new System.Drawing.Point(561, 159);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(118, 39);
            this.button_delete.TabIndex = 52;
            this.button_delete.Text = "Delete";
            this.button_delete.UseVisualStyleBackColor = false;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click_1);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 10.8F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.label1.Location = new System.Drawing.Point(8, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 21);
            this.label1.TabIndex = 43;
            this.label1.Text = "First Name :";
            // 
            // button_update
            // 
            this.button_update.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_update.BackColor = System.Drawing.Color.SlateGray;
            this.button_update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_update.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_update.ForeColor = System.Drawing.Color.White;
            this.button_update.Location = new System.Drawing.Point(424, 159);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(118, 39);
            this.button_update.TabIndex = 51;
            this.button_update.Text = "Update";
            this.button_update.UseVisualStyleBackColor = false;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 10.8F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.label3.Location = new System.Drawing.Point(613, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 21);
            this.label3.TabIndex = 45;
            this.label3.Text = "Class Arm Id :";
            // 
            // textBox_firstName
            // 
            this.textBox_firstName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_firstName.Location = new System.Drawing.Point(121, 51);
            this.textBox_firstName.Name = "textBox_firstName";
            this.textBox_firstName.Size = new System.Drawing.Size(187, 32);
            this.textBox_firstName.TabIndex = 46;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 10.8F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(71)))), ((int)(((byte)(160)))));
            this.label4.Location = new System.Drawing.Point(314, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 21);
            this.label4.TabIndex = 47;
            this.label4.Text = "Last Name :";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // textBox_lastName
            // 
            this.textBox_lastName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_lastName.Location = new System.Drawing.Point(427, 57);
            this.textBox_lastName.Name = "textBox_lastName";
            this.textBox_lastName.Size = new System.Drawing.Size(186, 32);
            this.textBox_lastName.TabIndex = 48;
            // 
            // textBox_search
            // 
            this.textBox_search.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBox_search.Location = new System.Drawing.Point(390, 3);
            this.textBox_search.Name = "textBox_search";
            this.textBox_search.Size = new System.Drawing.Size(361, 32);
            this.textBox_search.TabIndex = 66;
            this.textBox_search.TextChanged += new System.EventHandler(this.textBox_search_TextChanged_1);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox_search);
            this.panel2.Controls.Add(this.textBox_lastName);
            this.panel2.Controls.Add(this.comboBox_classArm);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.button_search);
            this.panel2.Controls.Add(this.textBox_firstName);
            this.panel2.Controls.Add(this.comboBox_class);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.button_add);
            this.panel2.Controls.Add(this.button_update);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.button_delete);
            this.panel2.Controls.Add(this.textBox_admissionNumber);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.textBox_otherName);
            this.panel2.Controls.Add(this.button_clear);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Location = new System.Drawing.Point(12, 57);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(910, 202);
            this.panel2.TabIndex = 67;
            // 
            // ManageStudentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 613);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.DataGridView_student);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "ManageStudentForm";
            this.Text = "ManageStudentForm";
            this.Load += new System.EventHandler(this.ManageStudentForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_student)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_classArm;
        private System.Windows.Forms.ComboBox comboBox_class;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView DataGridView_student;
        private System.Windows.Forms.TextBox textBox_admissionNumber;
        private System.Windows.Forms.TextBox textBox_otherName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.Button button_search;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_firstName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_lastName;
        private System.Windows.Forms.TextBox textBox_search;
        private System.Windows.Forms.Panel panel2;
    }
}