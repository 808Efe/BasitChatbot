using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WinFormsApp2.UIHelper;

namespace WinFormsApp2
{
public partial class SettingsForm : Form
{
        public SettingsForm()
        {
            InitializeComponent();
            InitializeSettingsUI();
            UIHelper.RoundAllControls(this);
        }

        private void InitializeSettingsUI()
        {
            this.Text = "Settings";
            this.Size = new Size(300, 350);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            FlowLayoutPanel panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = true,
                Padding = new Padding(20)
            };
            this.Controls.Add(panel);

            // New Conversation on Startup checkbox
            CheckBox chkOpenOnStart = new CheckBox
            {
                Text = "Open new conversation on launch",
                AutoSize = true,
                Checked = true // default
            };
            panel.Controls.Add(chkOpenOnStart);

            // Dark Mode toggle (not functional yet)
            CheckBox chkDarkMode = new CheckBox
            {
                Text = "Dark Mode (coming soon)",
                AutoSize = true,
                Enabled = false
            };
            panel.Controls.Add(chkDarkMode);

            // Clear all conversations button
            Button btnClearConvos = new Button
            {
                Text = "Clear All Conversations",
                Width = 220,
                Height = 40,
                BackColor = Color.IndianRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnClearConvos.Click += (s, e) =>
            {
                var mainForm = this.Owner as Form1;
                if (mainForm != null)
                {
                    mainForm.ClearAllConversations(); // Add this method in Form1
                }
                MessageBox.Show("All conversations cleared.");
            };
            panel.Controls.Add(btnClearConvos);

            // Separator
            panel.Controls.Add(new Label
            {
                Text = "────────────",
                AutoSize = true,
                ForeColor = Color.Gray,
                Margin = new Padding(0, 10, 0, 10)
            });

            // Logout button
            Button btnLogout = new Button
            {
                Text = "Logout",
                Width = 220,
                Height = 35,
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnLogout.Click += (s, e) =>
            {
                Application.Restart();
            };
            panel.Controls.Add(btnLogout);

            // Close button
            Button btnClose = new Button
            {
                Text = "Close Settings",
                Width = 220,
                Height = 35,
                BackColor = Color.DimGray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnClose.Click += (s, e) => this.Close();
            panel.Controls.Add(btnClose);
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            //Empty for now.
        }
}

}