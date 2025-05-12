namespace WinFormsApp2
{
    public partial class SignupForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnSignUp;
        private Label lblTitle;
        private RadioButton rbMale;
        private RadioButton rbFemale;
        private ComboBox cbCountry;

        public SignupForm()
        {
            InitializeComponent();
            InitializeLoginUI();
            UIHelper.RoundAllControls(this);
            txtUsername.KeyDown += TxtInput_KeyDown;
            txtPassword.KeyDown += TxtInput_KeyDown;
        }

        private void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift) // Check if Enter is pressed (without Shift for multi-line input)
            {
                e.SuppressKeyPress = true; // Prevents the "ding" sound
                btnSignUp.PerformClick();
            }
        }

        private void InitializeLoginUI()
        {
            this.Text = "Sign Up";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(420, 400); // Slightly increase the size to prevent tightness
            this.BackColor = Color.FromArgb(60, 60, 60);
            this.MaximizeBox = false;

            lblTitle = new Label()
            {
                Text = "Simple Chatbot UI",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point((this.ClientSize.Width - 120) / 2, 30),
            };
            lblTitle.Left = ((this.ClientSize.Width - lblTitle.Width) / 2) - 60;

            txtUsername = new TextBox()
            {
                PlaceholderText = "Username",
                Font = new Font("Segoe UI", 12),
                Size = new Size(250, 30),
                Location = new Point((this.ClientSize.Width - 250) / 2, 90),
                BackColor = Color.White,
                ForeColor = Color.Black
            };

            txtPassword = new TextBox()
            {
                PlaceholderText = "Password",
                Font = new Font("Segoe UI", 12),
                Size = new Size(250, 30),
                Location = new Point((this.ClientSize.Width - 250) / 2, 140),
                BackColor = Color.White,
                ForeColor = Color.Black,
                UseSystemPasswordChar = true
            };

            // Male RadioButton
            rbMale = new RadioButton()
            {
                Text = "Male",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.White,
                Location = new Point((this.ClientSize.Width - 250) / 2, 190),
                Checked = true // Set Male as default
            };

            // Female RadioButton
            rbFemale = new RadioButton()
            {
                Text = "Female",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.White,
                Location = new Point((this.ClientSize.Width - 250) / 2 + +120, 190),
            };

            // Country ComboBox
            cbCountry = new ComboBox()
            {
                Font = new Font("Segoe UI", 12),
                Size = new Size(250, 30),
                Location = new Point((this.ClientSize.Width - 250) / 2, 240),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.White,
                ForeColor = Color.Black
            };
            cbCountry.Items.AddRange(new string[] { "USA", "Turkey", "Canada", "Germany", "Australia" });
            cbCountry.SelectedIndex = 0; // Default to USA

            btnSignUp = new Button()
            {
                Text = "Sign Up",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(250, 40),
                Location = new Point((this.ClientSize.Width - 250) / 2, 310), // Adjust button position
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSignUp.FlatAppearance.BorderSize = 0;
            btnSignUp.Cursor = Cursors.Hand;

            btnSignUp.Click += btnSignUp_Click;

            this.Controls.Add(lblTitle);
            this.Controls.Add(txtUsername);
            this.Controls.Add(txtPassword);
            this.Controls.Add(rbMale);
            this.Controls.Add(rbFemale);
            this.Controls.Add(cbCountry);
            this.Controls.Add(btnSignUp);
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            MessageBox.Show("You can now login!", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;  // Triggers return to Program.cs
            this.Close();

        }

        private void SignupForm_Load(object sender, EventArgs e)
        {
            // Empty for now.
        }
    }
}
