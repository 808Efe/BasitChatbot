namespace WinFormsApp2
{
    public partial class LoginForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblTitle;

        public LoginForm()
        {
            InitializeComponent();
            InitializeLoginUI();
        }

        private void InitializeLoginUI()
        {
            this.Text = "Login";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(400, 300);
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

            btnLogin = new Button()
            {
                Text = "Login",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Size = new Size(250, 40),
                Location = new Point((this.ClientSize.Width - 250) / 2, 200),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Cursor = Cursors.Hand;

            btnLogin.Click += BtnLogin_Click;

            this.Controls.Add(lblTitle);
            this.Controls.Add(txtUsername);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnLogin);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (username == "admin" && password == "admin")
            {
                this.DialogResult = DialogResult.OK;  // Triggers return to Program.cs
                this.Close(); // Closes the login form
            }
            else
            {
                MessageBox.Show("Invalid credentials!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Empty for now.
        }

    }
}