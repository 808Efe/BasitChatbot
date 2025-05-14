using static WinFormsApp2.UIHelper;

namespace WinFormsApp2
{
    public partial class FirstPage : Form
    {
        private Button btnLogin;
        private Button btnSignup;
        private Label lblAppName;

        public FirstPage()
        {
            InitializeComponent();
            InitializeFirstPageUI();
            UIHelper.RoundAllControls(this);
        }

        private void InitializeFirstPageUI()
        {
            this.Text = "Simple Chatbot App";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(420, 300);
            this.BackColor = Color.FromArgb(60, 60, 60);
            this.MaximizeBox = false;

            lblAppName = new Label()
            {
                Text = "Welcome!",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point((this.ClientSize.Width - 250) / 2, 30),
            };

            btnLogin = new Button()
            {
                Text = "Login",
                Font = new Font("Segoe UI", 12),
                Size = new Size(250, 40),
                Location = new Point((this.ClientSize.Width - 250) / 2, 120),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnLogin.Click += BtnLogin_Click;

            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Cursor = Cursors.Hand;

            btnSignup = new Button()
            {
                Text = "Sign Up",
                Font = new Font("Segoe UI", 12),
                Size = new Size(250, 40),
                Location = new Point((this.ClientSize.Width - 250) / 2, 180),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSignup.Click += BtnSignup_Click;

            btnSignup.FlatAppearance.BorderSize = 0;
            btnSignup.Cursor = Cursors.Hand;

            this.Controls.Add(lblAppName);
            this.Controls.Add(btnLogin);
            this.Controls.Add(btnSignup);

            UIHelper.RoundAllControls(this);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            using (var loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Hide the FirstPage and show Form1
                    this.Hide();
                    Form1 form1 = new Form1();
                    form1.ShowDialog();
                    this.Close(); // Close FirstPage after Form1 is closed
                }
            }
        }

        private void BtnSignup_Click(object sender, EventArgs e)
        {
            using (var signupForm = new SignupForm())
            {
                if (signupForm.ShowDialog() == DialogResult.OK)
                {
                    // After signup, return to the first page
                    this.Show();
                }
            }
        }
    }
}
