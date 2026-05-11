using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Transparent_Form
{
    public static class UITheme
    {
        private static readonly Color Background = Color.FromArgb(241, 245, 249);
        private static readonly Color Surface = Color.White;
        private static readonly Color Primary = Color.FromArgb(37, 99, 235);
        private static readonly Color PrimaryDark = Color.FromArgb(29, 78, 216);
        private static readonly Color PrimarySoft = Color.FromArgb(219, 234, 254);
        private static readonly Color Sidebar = Color.FromArgb(15, 23, 42);
        private static readonly Color SidebarHover = Color.FromArgb(30, 41, 59);
        private static readonly Color SidebarSub = Color.FromArgb(30, 58, 138);
        private static readonly Color Danger = Color.FromArgb(239, 68, 68);
        private static readonly Color DangerDark = Color.FromArgb(220, 38, 38);
        private static readonly Color Neutral = Color.FromArgb(226, 232, 240);
        private static readonly Color NeutralHover = Color.FromArgb(203, 213, 225);
        private static readonly Color TextDark = Color.FromArgb(15, 23, 42);
        private static readonly Color TextMuted = Color.FromArgb(71, 85, 105);
        private static readonly Color Border = Color.FromArgb(226, 232, 240);
        private static readonly Color Success = Color.FromArgb(22, 163, 74);
        private static readonly Color Warning = Color.FromArgb(234, 88, 12);

        public static void ApplyFormTheme(Form form)
        {
            if (form == null) return;

            EnableDoubleBuffering(form);
            form.BackColor = Background;
            form.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            form.StartPosition = FormStartPosition.CenterScreen;

            string formName = (form.Name ?? form.GetType().Name).ToLower();
            bool isLogin = formName.Contains("login");
            bool isMain = formName.Contains("mainform");

            if (isLogin || isMain)
            {
                form.MinimumSize = new Size(1280, 720);
                form.WindowState = FormWindowState.Maximized;
            }

            if (isMain && (form.FormBorderStyle == FormBorderStyle.FixedSingle || form.FormBorderStyle == FormBorderStyle.FixedDialog))
            {
                form.FormBorderStyle = FormBorderStyle.Sizable;
            }

            foreach (Control control in form.Controls)
                ApplyControlTheme(control);

            if (isLogin)
            {
                ModernizeLoginForm(form);
                form.Resize -= LoginResizeHandler;
                form.Resize += LoginResizeHandler;
            }
            else if (isMain)
            {
                ImproveMainDashboard(form);
                form.Resize -= MainResizeHandler;
                form.Resize += MainResizeHandler;
            }
            else
            {
                ModernizeChildForm(form);
                form.Resize -= ChildResizeHandler;
                form.Resize += ChildResizeHandler;
            }
        }

        public static void ApplyEmbeddedFormTheme(Form form)
        {
            if (form == null) return;
            form.MinimumSize = Size.Empty;
            form.WindowState = FormWindowState.Normal;
            form.BackColor = Background;
            foreach (Control control in form.Controls)
                ApplyControlTheme(control);
            ModernizeChildForm(form);
            form.Resize -= ChildResizeHandler;
            form.Resize += ChildResizeHandler;
        }

        private static void LoginResizeHandler(object sender, EventArgs e)
        {
            ModernizeLoginForm(sender as Form);
        }

        private static void MainResizeHandler(object sender, EventArgs e)
        {
            ImproveMainDashboard(sender as Form);
        }

        private static void ChildResizeHandler(object sender, EventArgs e)
        {
            ModernizeChildForm(sender as Form);
        }

        private static void ApplyControlTheme(Control control)
        {
            if (control == null) return;
            EnableDoubleBuffering(control);

            if (control is Panel) StylePanel((Panel)control);
            else if (control is GroupBox) StyleGroupBox((GroupBox)control);
            else if (control is Label) StyleLabel((Label)control);
            else if (control is Button) StyleButton((Button)control);
            else if (control is TextBox) StyleTextBox((TextBox)control);
            else if (control is ComboBox) StyleComboBox((ComboBox)control);
            else if (control is DateTimePicker) StyleDateTimePicker((DateTimePicker)control);
            else if (control is DataGridView) StyleDataGridView((DataGridView)control);
            else if (control is RadioButton) StyleRadioButton((RadioButton)control);
            else if (control is CheckBox) StyleCheckBox((CheckBox)control);
            else if (control is TabControl) ((TabControl)control).Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            else if (control is PictureBox) ((PictureBox)control).SizeMode = PictureBoxSizeMode.Zoom;

            foreach (Control child in control.Controls)
                ApplyControlTheme(child);
        }

        private static void ModernizeLoginForm(Form form)
        {
            if (form == null || form.ClientSize.Width <= 0) return;

            form.BackColor = Background;

            Panel top = FindControlRecursive(form, "panel1") as Panel;
            Label close = FindControlRecursive(form, "label6") as Label;
            Label school = FindControlRecursive(form, "label1") as Label;
            PictureBox logo = FindControlRecursive(form, "pictureBox1") as PictureBox;
            PictureBox icon = FindControlRecursive(form, "pictureBox2") as PictureBox;
            Label title = FindControlRecursive(form, "label2") as Label;
            Label userLabel = FindControlRecursive(form, "label3") as Label;
            Label passLabel = FindControlRecursive(form, "label4") as Label;
            Label footer = FindControlRecursive(form, "label5") as Label;
            TextBox username = FindControlRecursive(form, "textBox_usrname") as TextBox;
            TextBox password = FindControlRecursive(form, "textBox_password") as TextBox;
            Button login = FindControlRecursive(form, "button_login") as Button;
            CheckBox show = FindControlRecursive(form, "checkBox_showpass") as CheckBox;

            if (top != null)
            {
                top.Dock = DockStyle.Top;
                top.Height = 82;
                top.BackColor = Surface;
                top.Padding = new Padding(28, 0, 28, 0);
            }
            if (logo != null) { logo.Location = new Point(32, 16); logo.Size = new Size(52, 52); }
            if (school != null)
            {
                school.AutoSize = false;
                school.Location = new Point(102, 21);
                school.Size = new Size(600, 40);
                school.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
                school.ForeColor = TextDark;
                school.TextAlign = ContentAlignment.MiddleLeft;
            }
            if (close != null)
            {
                close.AutoSize = false;
                close.Location = new Point(top != null ? top.Width - 70 : form.Width - 70, 18);
                close.Size = new Size(45, 45);
                close.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
                close.ForeColor = Danger;
                close.TextAlign = ContentAlignment.MiddleCenter;
                close.Cursor = Cursors.Hand;
                close.BackColor = Color.Transparent;
            }

            Panel card = FindControlRecursive(form, "uiLoginCard") as Panel;
            if (card == null)
            {
                card = new Panel();
                card.Name = "uiLoginCard";
                card.BackColor = Surface;
                form.Controls.Add(card);
                card.BringToFront();
            }

            Control[] loginControls = new Control[] { icon, title, userLabel, username, passLabel, password, show, login };
            foreach (Control c in loginControls)
            {
                if (c != null && c.Parent != card)
                {
                    Point old = c.PointToScreen(Point.Empty);
                    card.Controls.Add(c);
                    c.Location = card.PointToClient(old);
                }
            }

            int cardW = Math.Min(520, Math.Max(420, form.ClientSize.Width / 3));
            int cardH = 545;
            int cardX = (form.ClientSize.Width - cardW) / 2;
            int cardY = Math.Max(120, (form.ClientSize.Height - cardH) / 2 + 25);
            card.Location = new Point(cardX, cardY);
            card.Size = new Size(cardW, cardH);
            card.Padding = new Padding(36);
            MakeRoundedPanel(card, 24, Border);

            if (icon != null) { icon.Location = new Point((cardW - 116) / 2, 34); icon.Size = new Size(116, 104); }
            if (title != null)
            {
                title.AutoSize = false;
                title.Location = new Point(36, 150);
                title.Size = new Size(cardW - 72, 38);
                title.TextAlign = ContentAlignment.MiddleCenter;
                title.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
                title.ForeColor = TextDark;
            }
            if (userLabel != null) { userLabel.AutoSize = false; userLabel.Location = new Point(46, 215); userLabel.Size = new Size(cardW - 92, 24); userLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold); userLabel.ForeColor = TextMuted; }
            if (username != null) { username.Location = new Point(46, 244); username.Size = new Size(cardW - 92, 42); username.Font = new Font("Segoe UI", 12F); }
            if (passLabel != null) { passLabel.AutoSize = false; passLabel.Location = new Point(46, 312); passLabel.Size = new Size(cardW - 92, 24); passLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold); passLabel.ForeColor = TextMuted; }
            if (password != null) { password.Location = new Point(46, 341); password.Size = new Size(cardW - 92, 42); password.Font = new Font("Segoe UI", 12F); }
            if (show != null) { show.Location = new Point(46, 399); show.Size = new Size(cardW - 92, 30); show.Font = new Font("Segoe UI", 10F, FontStyle.Bold); show.ForeColor = TextMuted; show.BackColor = Surface; }
            if (login != null) { login.Location = new Point(46, 452); login.Size = new Size(cardW - 92, 52); login.Font = new Font("Segoe UI", 11F, FontStyle.Bold); }

            if (footer != null)
            {
                footer.AutoSize = false;
                footer.Location = new Point(0, form.ClientSize.Height - 42);
                footer.Size = new Size(form.ClientSize.Width, 26);
                footer.TextAlign = ContentAlignment.MiddleCenter;
                footer.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
                footer.ForeColor = TextMuted;
                footer.BackColor = Color.Transparent;
            }
        }

        private static void ImproveMainDashboard(Form form)
        {
            if (form == null) return;

            Panel sidebar = FindControlRecursive(form, "panel_slide") as Panel;
            Panel main = FindControlRecursive(form, "panel_main") as Panel;
            Panel cover = FindControlRecursive(form, "panel_cover") as Panel;
            Panel logo = FindControlRecursive(form, "panel_logo") as Panel;

            if (sidebar != null)
            {
                sidebar.Width = 270;
                sidebar.BackColor = Sidebar;
                sidebar.Padding = new Padding(0, 0, 0, 12);
            }

            if (logo != null)
            {
                logo.Height = 175;
                logo.BackColor = Sidebar;

                Label logoLetter = FindControlRecursive(logo, "label2") as Label;
                Label welcome = FindControlRecursive(logo, "label3") as Label;
                Label school = FindControlRecursive(logo, "label4") as Label;

                if (logoLetter != null)
                {
                    logoLetter.AutoSize = false;
                    logoLetter.Location = new Point(0, 18);
                    int logoPanelWidth = sidebar != null ? sidebar.Width : logo.Width;
                    logoLetter.Size = new Size(logoPanelWidth, 58);
                    logoLetter.TextAlign = ContentAlignment.MiddleCenter;
                    logoLetter.Font = new Font("Segoe UI", 32F, FontStyle.Bold);
                    logoLetter.ForeColor = Color.White;
                    logoLetter.BackColor = Color.Transparent;
                }

                if (welcome != null)
                {
                    welcome.AutoSize = false;
                    welcome.Location = new Point(14, 86);
                    welcome.Size = new Size(Math.Max(120, (sidebar != null ? sidebar.Width : logo.Width) - 28), 24);
                    welcome.TextAlign = ContentAlignment.MiddleCenter;
                    welcome.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                    welcome.ForeColor = Color.White;
                    welcome.BackColor = Color.Transparent;
                }

                if (school != null)
                {
                    school.AutoSize = false;
                    school.Location = new Point(14, 112);
                    school.Size = new Size(Math.Max(120, (sidebar != null ? sidebar.Width : logo.Width) - 28), 28);
                    school.TextAlign = ContentAlignment.MiddleCenter;
                    school.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                    school.ForeColor = Color.White;
                    school.BackColor = Color.Transparent;
                }
            }

            if (main != null)
            {
                main.BackColor = Background;
                main.Padding = new Padding(18);
            }

            if (cover != null)
            {
                cover.Dock = DockStyle.Fill;
                cover.BackColor = Background;
                cover.Padding = new Padding(24);
                ArrangeDashboardCards(cover);
            }
        }

        private static void ArrangeDashboardCards(Panel cover)
        {
            if (cover == null || cover.ClientSize.Width <= 0) return;

            Panel header = FindControlRecursive(cover, "panel1") as Panel;
            if (header != null)
            {
                header.Location = new Point(24, 22);
                header.Size = new Size(Math.Max(300, cover.ClientSize.Width - 48), 86);
                header.BackColor = Surface;
                MakeRoundedPanel(header, 18, Border);

                Label title = FindControlRecursive(header, "label12") as Label;
                if (title != null)
                {
                    title.AutoSize = false;
                    title.Location = new Point(24, 18);
                    title.Size = new Size(Math.Max(250, header.Width - 48), 45);
                    title.TextAlign = ContentAlignment.MiddleLeft;
                    title.Font = new Font("Segoe UI", 19F, FontStyle.Bold);
                    title.ForeColor = TextDark;
                }
            }

            Panel[] cards = new Panel[]
            {
                FindControlRecursive(cover, "panel2") as Panel,
                FindControlRecursive(cover, "panel3") as Panel,
                FindControlRecursive(cover, "panel4") as Panel,
                FindControlRecursive(cover, "panel6") as Panel,
                FindControlRecursive(cover, "panel8") as Panel,
                FindControlRecursive(cover, "panel5") as Panel,
                FindControlRecursive(cover, "panel7") as Panel
            };

            int left = 24;
            int top = 132;
            int gap = 18;
            int availableWidth = Math.Max(300, cover.ClientSize.Width - 48);
            int columns = availableWidth >= 1050 ? 3 : (availableWidth >= 720 ? 2 : 1);
            int cardWidth = (availableWidth - ((columns - 1) * gap)) / columns;
            int cardHeight = 132;

            for (int i = 0; i < cards.Length; i++)
            {
                Panel card = cards[i];
                if (card == null) continue;
                int row = i / columns;
                int col = i % columns;
                card.Location = new Point(left + (col * (cardWidth + gap)), top + (row * (cardHeight + gap)));
                card.Size = new Size(cardWidth, cardHeight);
                card.BackColor = Surface;
                card.Padding = new Padding(20);
                MakeRoundedPanel(card, 20, Border);
                StyleDashboardCardChildren(card, i);
            }
        }

        private static void StyleDashboardCardChildren(Panel card, int index)
        {
            if (card == null) return;
            Color accent = index == 0 ? Primary : index == 1 ? Success : index == 2 ? Color.FromArgb(124, 58, 237) : index == 3 ? Warning : index == 4 ? Color.FromArgb(14, 165, 233) : index == 5 ? Color.FromArgb(236, 72, 153) : Color.FromArgb(20, 184, 166);

            foreach (Control c in card.Controls)
            {
                if (c is PictureBox)
                {
                    PictureBox pic = (PictureBox)c;
                    pic.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                    pic.Location = new Point(Math.Max(10, card.Width - 92), 26);
                    pic.Size = new Size(60, 60);
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                    pic.BackColor = Color.Transparent;
                }
                else if (c is Label)
                {
                    Label label = (Label)c;
                    label.AutoSize = false;
                    label.BackColor = Color.Transparent;
                    if (IsNumber(label.Text) || (label.Name ?? "").ToLower().Contains("count"))
                    {
                        label.Location = new Point(24, 57);
                        label.Size = new Size(Math.Max(80, card.Width - 124), 45);
                        label.Font = new Font("Segoe UI", 25F, FontStyle.Bold);
                        label.ForeColor = accent;
                    }
                    else
                    {
                        label.Location = new Point(24, 24);
                        label.Size = new Size(Math.Max(120, card.Width - 124), 28);
                        label.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                        label.ForeColor = TextMuted;
                    }
                }
            }
        }

        private static void ModernizeChildForm(Form form)
        {
            if (form == null || form.ClientSize.Width < 250) return;

            form.AutoScroll = false;
            form.BackColor = Background;
            form.Padding = new Padding(0);

            Panel header = FindControlRecursive(form, "panel1") as Panel;
            Panel inputPanel = GetInputPanel(form);
            DataGridView dgv = GetFirstDataGridView(form);
            Panel gridPanel = GetGridPanel(form, dgv);

            int margin = 24;
            int width = Math.Max(300, form.ClientSize.Width - (margin * 2));

            if (header != null)
            {
                header.Dock = DockStyle.None;
                header.Location = new Point(margin, 18);
                header.Size = new Size(width, 64);
                header.BackColor = Surface;
                header.Padding = new Padding(22, 0, 22, 0);
                MakeRoundedPanel(header, 18, Border);

                foreach (Control c in header.Controls)
                {
                    if (c is Label)
                    {
                        Label title = (Label)c;
                        title.AutoSize = false;
                        title.Location = new Point(22, 0);
                        title.Size = new Size(header.Width - 44, header.Height);
                        title.TextAlign = ContentAlignment.MiddleLeft;
                        title.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
                        title.ForeColor = TextDark;
                        title.BackColor = Color.Transparent;
                    }
                }
            }

            int inputTop = header != null ? header.Bottom + 18 : 18;
            int inputHeight = CalculateInputHeight(inputPanel);

            if (inputPanel != null)
            {
                inputPanel.Dock = DockStyle.None;
                inputPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                inputPanel.Location = new Point(margin, inputTop);
                inputPanel.Size = new Size(width, inputHeight);
                inputPanel.BackColor = Surface;
                inputPanel.Padding = new Padding(24);
                MakeRoundedPanel(inputPanel, 20, Border);
                ArrangeInputPanel(inputPanel);
            }

            if (dgv != null)
            {
                if (gridPanel != null && dgv.Parent != gridPanel)
                {
                    gridPanel.Controls.Add(dgv);
                }

                int gridTop = (inputPanel != null ? inputPanel.Bottom : inputTop) + 18;
                int gridHeight = Math.Max(160, form.ClientSize.Height - gridTop - margin);

                if (gridPanel != null)
                {
                    gridPanel.Dock = DockStyle.None;
                    gridPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                    gridPanel.Location = new Point(margin, gridTop);
                    gridPanel.Size = new Size(width, gridHeight);
                    gridPanel.BackColor = Surface;
                    gridPanel.Padding = new Padding(14);
                    MakeRoundedPanel(gridPanel, 20, Border);
                    gridPanel.BringToFront();
                }

                dgv.Dock = DockStyle.Fill;
                dgv.Margin = new Padding(0);
                dgv.BringToFront();
            }

            if (string.Equals(form.Name, "ManageScoreForm", StringComparison.OrdinalIgnoreCase))
            {
                ModernizeManageAdminForm(form);
                return;
            }

            // Hide decorative divider panels that caused awkward horizontal strips.
            foreach (Panel p in GetAllControls<Panel>(form))
            {
                if (p.Name == "panel3" && !ContainsControlRecursive(p, dgv) && p.Height <= 15)
                    p.Visible = false;
            }
        }

        private static void ModernizeManageAdminForm(Form form)
        {
            if (form == null || form.ClientSize.Width < 250) return;

            Panel header = FindControlRecursive(form, "panel1") as Panel;
            Panel inputPanel = FindControlRecursive(form, "panel4") as Panel;
            Panel gridPanel = FindControlRecursive(form, "panel2") as Panel;
            Panel divider = FindControlRecursive(form, "panel3") as Panel;
            DataGridView dgv = FindControlRecursive(form, "dataGridView_admin") as DataGridView;

            int margin = 24;
            int width = Math.Max(300, form.ClientSize.Width - (margin * 2));

            if (header != null)
            {
                header.Dock = DockStyle.None;
                header.Location = new Point(margin, 18);
                header.Size = new Size(width, 64);
                header.BackColor = Surface;
                header.Padding = new Padding(22, 0, 22, 0);
                MakeRoundedPanel(header, 18, Border);

                Label title = FindControlRecursive(header, "label7") as Label;
                if (title != null)
                {
                    title.AutoSize = false;
                    title.Location = new Point(22, 0);
                    title.Size = new Size(header.Width - 44, header.Height);
                    title.TextAlign = ContentAlignment.MiddleLeft;
                    title.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
                    title.ForeColor = TextDark;
                    title.BackColor = Color.Transparent;
                }
            }

            int inputTop = header != null ? header.Bottom + 18 : 18;
            int inputHeight = 248;
            if (inputPanel != null)
            {
                inputPanel.Dock = DockStyle.None;
                inputPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                inputPanel.Location = new Point(margin, inputTop);
                inputPanel.Size = new Size(width, inputHeight);
                inputPanel.BackColor = Surface;
                inputPanel.Padding = new Padding(26);
                MakeRoundedPanel(inputPanel, 20, Border);

                TextBox firstName = FindControlRecursive(inputPanel, "textBox_adminFname") as TextBox;
                TextBox lastName = FindControlRecursive(inputPanel, "textBox_adminLname") as TextBox;
                TextBox email = FindControlRecursive(inputPanel, "textBox_adminEmail") as TextBox;
                TextBox password = FindControlRecursive(inputPanel, "textBox_adminPassword") as TextBox;
                TextBox confirm = FindControlRecursive(inputPanel, "textBox_adminConfirmPassword") as TextBox;

                Label firstLabel = FindControlRecursive(inputPanel, "label11") as Label;
                Label lastLabel = FindControlRecursive(inputPanel, "label10") as Label;
                Label emailLabel = FindControlRecursive(inputPanel, "label3") as Label;
                Label passwordLabel = FindControlRecursive(inputPanel, "label9") as Label;
                Label confirmLabel = FindControlRecursive(inputPanel, "label1") as Label;

                Button add = FindControlRecursive(inputPanel, "button_add") as Button;
                Button update = FindControlRecursive(inputPanel, "button1") as Button;
                Button delete = FindControlRecursive(inputPanel, "button2") as Button;
                Button clear = FindControlRecursive(inputPanel, "button3") as Button;

                Control[] labels = new Control[] { firstLabel, lastLabel, emailLabel, passwordLabel, confirmLabel };
                foreach (Control c in labels)
                {
                    if (c is Label)
                    {
                        Label l = (Label)c;
                        l.AutoSize = false;
                        l.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                        l.ForeColor = TextMuted;
                        l.BackColor = Surface;
                        l.TextAlign = ContentAlignment.MiddleLeft;
                        l.BringToFront();
                    }
                }

                if (passwordLabel != null) passwordLabel.Text = "Password :";
                if (confirmLabel != null) confirmLabel.Text = "Confirm Password :";

                int pad = 26;
                int gap = 20;
                int fieldW = Math.Max(180, (inputPanel.Width - (pad * 2) - (gap * 2)) / 3);
                int labelH = 22;
                int fieldH = 36;
                int row1Y = 30;
                int row2Y = 105;

                PlaceLabeledField(firstLabel, firstName, pad, row1Y, fieldW, labelH, fieldH);
                PlaceLabeledField(lastLabel, lastName, pad + fieldW + gap, row1Y, fieldW, labelH, fieldH);
                PlaceLabeledField(emailLabel, email, pad + (fieldW + gap) * 2, row1Y, fieldW, labelH, fieldH);
                PlaceLabeledField(passwordLabel, password, pad, row2Y, fieldW, labelH, fieldH);
                PlaceLabeledField(confirmLabel, confirm, pad + fieldW + gap, row2Y, fieldW, labelH, fieldH);

                if (password != null) password.UseSystemPasswordChar = true;
                if (confirm != null) confirm.UseSystemPasswordChar = true;

                Button[] buttons = new Button[] { add, update, delete, clear }.Where(b => b != null).ToArray();
                int buttonW = 128;
                int buttonH = 44;
                int buttonGap = 14;
                int totalButtonW = buttons.Length * buttonW + Math.Max(0, buttons.Length - 1) * buttonGap;
                int buttonX = Math.Max(pad, inputPanel.Width - pad - totalButtonW);
                int buttonY = inputPanel.Height - pad - buttonH;

                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].Location = new Point(buttonX + i * (buttonW + buttonGap), buttonY);
                    buttons[i].Size = new Size(buttonW, buttonH);
                    buttons[i].Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                    buttons[i].BringToFront();
                }
            }

            if (divider != null) divider.Visible = false;

            if (gridPanel != null)
            {
                int gridTop = (inputPanel != null ? inputPanel.Bottom : inputTop) + 18;
                int gridHeight = Math.Max(180, form.ClientSize.Height - gridTop - margin);
                gridPanel.Dock = DockStyle.None;
                gridPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                gridPanel.Location = new Point(margin, gridTop);
                gridPanel.Size = new Size(width, gridHeight);
                gridPanel.BackColor = Surface;
                gridPanel.Padding = new Padding(14);
                MakeRoundedPanel(gridPanel, 20, Border);
                gridPanel.BringToFront();
            }

            if (dgv != null)
            {
                dgv.Visible = true;
                dgv.Dock = DockStyle.Fill;
                dgv.BringToFront();
            }
        }

        private static void PlaceLabeledField(Label label, Control field, int x, int y, int width, int labelHeight, int fieldHeight)
        {
            if (label != null)
            {
                label.Location = new Point(x, y);
                label.Size = new Size(width, labelHeight);
                label.Visible = true;
            }
            if (field != null)
            {
                field.Location = new Point(x, y + labelHeight + 6);
                field.Size = new Size(width, fieldHeight);
                field.Visible = true;
            }
        }

        private static int CalculateInputHeight(Panel panel)
        {
            if (panel == null) return 0;
            int fieldCount = GetAllControls<Control>(panel).Count(c => c is TextBox || c is ComboBox || c is DateTimePicker);
            int rows = fieldCount <= 2 ? 1 : (fieldCount <= 6 ? 2 : 3);
            return Math.Max(140, 36 + rows * 74 + 58);
        }

        private static void ArrangeInputPanel(Panel panel)
        {
            if (panel == null || panel.ClientSize.Width <= 0) return;

            List<Control> fields = panel.Controls.Cast<Control>()
                .Where(c => c is TextBox || c is ComboBox || c is DateTimePicker)
                .OrderBy(c => GetFieldOrder(c)).ThenBy(c => c.Top).ThenBy(c => c.Left).ToList();

            List<Button> buttons = panel.Controls.Cast<Control>()
                .Where(c => c is Button)
                .Cast<Button>()
                .OrderBy(b => b.Top).ThenBy(b => b.Left).ToList();

            int pad = 24;
            int gap = 18;
            int columns = panel.Width >= 900 ? 3 : (panel.Width >= 640 ? 2 : 1);
            int fieldW = (panel.Width - (pad * 2) - ((columns - 1) * gap)) / columns;
            int labelH = 22;
            int fieldH = 38;
            int rowH = 74;
            int startY = 22;

            for (int i = 0; i < fields.Count; i++)
            {
                Control field = fields[i];
                int row = i / columns;
                int col = i % columns;
                int x = pad + col * (fieldW + gap);
                int y = startY + row * rowH;

                Label label = FindBestLabelForField(panel, field);
                if (label != null)
                {
                    label.AutoSize = false;
                    label.Location = new Point(x, y);
                    label.Size = new Size(fieldW, labelH);
                    label.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
                    label.ForeColor = TextMuted;
                    label.BackColor = Surface;
                    label.TextAlign = ContentAlignment.MiddleLeft;
                }

                field.Location = new Point(x, y + 26);
                field.Size = new Size(fieldW, fieldH);
                field.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
            }

            int buttonY = startY + ((fields.Count + columns - 1) / columns) * rowH + 6;
            int buttonW = Math.Min(120, Math.Max(94, (panel.Width - (pad * 2) - ((buttons.Count - 1) * 12)) / Math.Max(1, buttons.Count)));
            int totalButtonW = buttons.Count * buttonW + Math.Max(0, buttons.Count - 1) * 12;
            int buttonX = Math.Max(pad, panel.Width - pad - totalButtonW);

            for (int i = 0; i < buttons.Count; i++)
            {
                Button b = buttons[i];
                b.Size = new Size(buttonW, 42);
                b.Location = new Point(buttonX + i * (buttonW + 12), buttonY);
                b.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            }
        }

        private static int GetFieldOrder(Control field)
        {
            string key = (field.Name ?? "").ToLower();

            if (key.Contains("fname") || key.Contains("firstname")) return 10;
            if (key.Contains("lname") || key.Contains("lastname")) return 20;
            if (key.Contains("other")) return 30;
            if (key.Contains("admission")) return 40;
            if (key.Contains("email")) return 50;
            if (key.Contains("phone")) return 60;
            if (key.Contains("password") && !key.Contains("confirm")) return 70;
            if (key.Contains("confirm") && key.Contains("password")) return 80;
            if (key.Contains("classarm") || (key.Contains("arm") && !key.Contains("name"))) return 100;
            if (key.Contains("class")) return 90;
            if (key.Contains("session")) return 110;
            if (key.Contains("term")) return 120;
            if (key.Contains("search")) return 5;

            return 500;
        }

        private static Label FindBestLabelForField(Panel panel, Control field)
        {
            string key = (field.Name ?? "").ToLower();
            string[] wanted;
            if (key.Contains("fname") || key.Contains("firstname")) wanted = new[] { "first" };
            else if (key.Contains("lname") || key.Contains("lastname")) wanted = new[] { "last" };
            else if (key.Contains("other")) wanted = new[] { "other" };
            else if (key.Contains("admission")) wanted = new[] { "admission" };
            else if (key.Contains("email")) wanted = new[] { "email" };
            else if (key.Contains("phone")) wanted = new[] { "phone" };
            else if (key.Contains("password") && key.Contains("confirm")) wanted = new[] { "confirm" };
            else if (key.Contains("password")) wanted = new[] { "password" };
            else if (key.Contains("classarm")) wanted = new[] { "class arm", "arm" };
            else if (key.Contains("class")) wanted = new[] { "class name", "select class", "class id", "class" };
            else if (key.Contains("session")) wanted = new[] { "session" };
            else if (key.Contains("term")) wanted = new[] { "term" };
            else if (key.Contains("search")) wanted = new[] { "search" };
            else wanted = new string[0];

            List<Label> labels = panel.Controls.Cast<Control>().Where(c => c is Label).Cast<Label>().ToList();
            foreach (string w in wanted)
            {
                Label match = labels.FirstOrDefault(l => (l.Text ?? "").ToLower().Contains(w));
                if (match != null) return match;
            }
            return labels.OrderBy(l => Math.Abs(l.Top - field.Top) + Math.Abs(l.Left - field.Left)).FirstOrDefault();
        }

        private static Panel GetInputPanel(Form form)
        {
            Panel p4 = FindControlRecursive(form, "panel4") as Panel;
            if (p4 != null && ContainsInputControl(p4)) return p4;
            Panel p2 = FindControlRecursive(form, "panel2") as Panel;
            if (p2 != null && ContainsInputControl(p2) && !ContainsControlType<DataGridView>(p2)) return p2;
            foreach (Panel p in GetAllControls<Panel>(form))
            {
                if (p.Name != "panel1" && ContainsInputControl(p) && !ContainsControlType<DataGridView>(p)) return p;
            }
            return null;
        }

        private static Panel GetGridPanel(Form form, DataGridView dgv)
        {
            if (dgv == null) return null;
            if (dgv.Parent is Panel && dgv.Parent.Name != "panel1") return dgv.Parent as Panel;

            Panel existing = FindControlRecursive(form, "uiGridPanel") as Panel;
            if (existing != null) return existing;

            Panel p = new Panel();
            p.Name = "uiGridPanel";
            p.BackColor = Surface;
            form.Controls.Add(p);
            p.BringToFront();
            return p;
        }

        private static DataGridView GetFirstDataGridView(Control parent)
        {
            return GetAllControls<DataGridView>(parent).FirstOrDefault();
        }

        private static bool ContainsInputControl(Control parent)
        {
            return ContainsControlType<TextBox>(parent) || ContainsControlType<ComboBox>(parent) || ContainsControlType<DateTimePicker>(parent);
        }

        private static bool ContainsControlType<T>(Control parent) where T : Control
        {
            return GetAllControls<T>(parent).Any();
        }

        private static bool ContainsControlRecursive(Control parent, Control target)
        {
            if (parent == null || target == null) return false;
            if (parent == target) return true;
            foreach (Control c in parent.Controls)
                if (ContainsControlRecursive(c, target)) return true;
            return false;
        }

        private static void StylePanel(Panel panel)
        {
            string name = (panel.Name ?? "").ToLower();
            if (name.Contains("side") || name.Contains("slide") || name.Contains("menu") || name.Contains("nav")) { panel.BackColor = Sidebar; return; }
            if (name.Contains("submenu")) { panel.BackColor = SidebarSub; return; }
            panel.BackColor = Surface;
        }

        private static void StyleGroupBox(GroupBox groupBox)
        {
            groupBox.ForeColor = TextDark;
            groupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBox.BackColor = Surface;
        }

        private static void StyleLabel(Label label)
        {
            string name = (label.Name ?? "").ToLower();
            string text = (label.Text ?? "").Trim();
            if (IsInsideSidebar(label))
            {
                label.ForeColor = Color.White;
                label.Font = new Font("Segoe UI", Math.Max(label.Font.Size, 10F), FontStyle.Bold);
                return;
            }
            if (name.Contains("count") || IsNumber(text))
            {
                label.ForeColor = Primary;
                label.Font = new Font("Segoe UI", Math.Max(label.Font.Size, 16F), FontStyle.Bold);
                return;
            }
            if (name.Contains("title") || name.Contains("header"))
            {
                label.ForeColor = TextDark;
                label.Font = new Font("Segoe UI", Math.Max(label.Font.Size, 14F), FontStyle.Bold);
                return;
            }
            label.ForeColor = TextDark;
            label.Font = new Font("Segoe UI", Math.Max(9F, label.Font.Size), label.Font.Style);
        }

        private static void StyleButton(Button button)
        {
            string name = (button.Name ?? "").ToLower();
            string text = (button.Text ?? "").ToLower();
            bool inSidebar = IsInsideSidebar(button);

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            button.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button.UseVisualStyleBackColor = false;

            bool isDanger = name.Contains("delete") || text.Contains("delete") || text.Contains("remove") || text.Contains("clear") || text.Contains("close") || text.Contains("exit");
            bool isNeutral = text.Contains("cancel") || text.Contains("back") || text.Contains("reset");
            Color normalBack;
            Color hoverBack;
            Color normalFore;

            if (inSidebar)
            {
                normalBack = GetSidebarButtonColor(button);
                hoverBack = SidebarHover;
                normalFore = Color.White;
                button.TextAlign = ContentAlignment.MiddleLeft;
                if (button.Padding.Left < 18) button.Padding = new Padding(22, 0, 0, 0);
            }
            else if (isDanger)
            {
                normalBack = Danger;
                hoverBack = DangerDark;
                normalFore = Color.White;
            }
            else if (isNeutral)
            {
                normalBack = Neutral;
                hoverBack = NeutralHover;
                normalFore = TextDark;
            }
            else
            {
                normalBack = Primary;
                hoverBack = PrimaryDark;
                normalFore = Color.White;
            }

            button.BackColor = normalBack;
            button.ForeColor = normalFore;
            button.Height = Math.Max(button.Height, inSidebar ? 54 : 40);

            button.MouseEnter -= ButtonMouseEnter;
            button.MouseLeave -= ButtonMouseLeave;
            button.MouseEnter += ButtonMouseEnter;
            button.MouseLeave += ButtonMouseLeave;
            button.Tag = new ButtonThemeState(normalBack, hoverBack, normalFore, inSidebar);
        }

        private static void ButtonMouseEnter(object sender, EventArgs e)
        {
            Button button = sender as Button;
            ButtonThemeState state = button != null ? button.Tag as ButtonThemeState : null;
            if (button == null || state == null) return;
            button.BackColor = state.HoverBack;
            if (!state.InSidebar) button.ForeColor = Color.White;
        }

        private static void ButtonMouseLeave(object sender, EventArgs e)
        {
            Button button = sender as Button;
            ButtonThemeState state = button != null ? button.Tag as ButtonThemeState : null;
            if (button == null || state == null) return;
            button.BackColor = state.NormalBack;
            button.ForeColor = state.NormalFore;
        }

        private class ButtonThemeState
        {
            public Color NormalBack;
            public Color HoverBack;
            public Color NormalFore;
            public bool InSidebar;
            public ButtonThemeState(Color normalBack, Color hoverBack, Color normalFore, bool inSidebar)
            {
                NormalBack = normalBack; HoverBack = hoverBack; NormalFore = normalFore; InSidebar = inSidebar;
            }
        }

        private static Color GetSidebarButtonColor(Button button)
        {
            Control parent = button.Parent;
            while (parent != null)
            {
                string parentName = (parent.Name ?? "").ToLower();
                if (parentName.Contains("submenu")) return SidebarSub;
                parent = parent.Parent;
            }
            return Sidebar;
        }

        private static void StyleTextBox(TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = Surface;
            textBox.ForeColor = TextDark;
            textBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            textBox.Height = Math.Max(textBox.Height, 34);
        }

        private static void StyleComboBox(ComboBox comboBox)
        {
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.BackColor = Surface;
            comboBox.ForeColor = TextDark;
            comboBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            comboBox.Height = Math.Max(comboBox.Height, 34);
        }

        private static void StyleDateTimePicker(DateTimePicker dateTimePicker)
        {
            dateTimePicker.CalendarTitleBackColor = Primary;
            dateTimePicker.CalendarTitleForeColor = Color.White;
            dateTimePicker.CalendarForeColor = TextDark;
            dateTimePicker.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dateTimePicker.Height = Math.Max(dateTimePicker.Height, 34);
        }

        private static void StyleRadioButton(RadioButton radioButton)
        {
            radioButton.ForeColor = TextDark;
            radioButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            radioButton.BackColor = Surface;
        }

        private static void StyleCheckBox(CheckBox checkBox)
        {
            checkBox.ForeColor = TextDark;
            checkBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            checkBox.BackColor = Surface;
        }

        private static void StyleDataGridView(DataGridView dgv)
        {
            dgv.BackgroundColor = Surface;
            dgv.BorderStyle = BorderStyle.None;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToResizeRows = false;
            dgv.GridColor = Border;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Primary;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgv.ColumnHeadersHeight = 48;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.DefaultCellStyle.BackColor = Surface;
            dgv.DefaultCellStyle.ForeColor = TextDark;
            dgv.DefaultCellStyle.SelectionBackColor = PrimarySoft;
            dgv.DefaultCellStyle.SelectionForeColor = TextDark;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgv.DefaultCellStyle.Padding = new Padding(6);
            dgv.RowTemplate.Height = 42;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Border;
        }

        private static void MakeRoundedPanel(Panel panel, int radius, Color borderColor)
        {
            if (panel == null) return;
            panel.Paint -= RoundedPanelPaint;
            panel.Paint += RoundedPanelPaint;
            panel.Tag = new RoundedPanelState(radius, borderColor);
            panel.Invalidate();
        }

        private static void RoundedPanelPaint(object sender, PaintEventArgs e)
        {
            Panel p = sender as Panel;
            RoundedPanelState state = p != null ? p.Tag as RoundedPanelState : null;
            if (p == null || state == null) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, p.Width - 1, p.Height - 1);
            using (GraphicsPath path = RoundedRectangle(rect, state.Radius))
            using (Pen pen = new Pen(state.BorderColor, 1))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        private class RoundedPanelState
        {
            public int Radius;
            public Color BorderColor;
            public RoundedPanelState(int radius, Color borderColor) { Radius = radius; BorderColor = borderColor; }
        }

        private static GraphicsPath RoundedRectangle(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();
            Rectangle arc = new Rectangle(bounds.Location, new Size(diameter, diameter));
            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }

        private static bool IsNumber(string value)
        {
            int result;
            return int.TryParse(value, out result);
        }

        private static bool IsInsideSidebar(Control control)
        {
            Control current = control;
            while (current != null)
            {
                string name = (current.Name ?? "").ToLower();
                if (name.Contains("slide") || name.Contains("side") || name.Contains("menu") || name.Contains("nav")) return true;
                current = current.Parent;
            }
            return false;
        }

        private static Control FindControlRecursive(Control parent, string name)
        {
            if (parent == null) return null;
            if (string.Equals(parent.Name, name, StringComparison.OrdinalIgnoreCase)) return parent;
            foreach (Control child in parent.Controls)
            {
                Control found = FindControlRecursive(child, name);
                if (found != null) return found;
            }
            return null;
        }

        private static List<T> GetAllControls<T>(Control parent) where T : Control
        {
            List<T> list = new List<T>();
            if (parent == null) return list;
            foreach (Control c in parent.Controls)
            {
                if (c is T) list.Add((T)c);
                list.AddRange(GetAllControls<T>(c));
            }
            return list;
        }

        private static void EnableDoubleBuffering(Control control)
        {
            if (control == null) return;
            try
            {
                PropertyInfo property = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                if (property != null) property.SetValue(control, true, null);
            }
            catch { }
        }
    }
}
