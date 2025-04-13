namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private List<string> conversations = new List<string>();
        private Dictionary<string, List<string>> conversationHistory = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> conversationSenders = new Dictionary<string, List<string>>();
        private string selectedConversation = null;

        public Form1()
        {
            InitializeComponent();
            InitializeConversationPanel();
            txtInput.KeyDown += TxtInput_KeyDown; // Subscribe to KeyDown event
        }

        private void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift) // Check if Enter is pressed (without Shift for multi-line input)
            {
                e.SuppressKeyPress = true; // Prevents the "ding" sound
                btnSend.PerformClick(); // Trigger the send button click event
            }
        }

        private void InitializeConversationPanel()
        {
            // Left panel for conversations
            Panel panelConversations = new Panel();
            panelConversations.Dock = DockStyle.Left;
            panelConversations.Width = 200;
            panelConversations.BackColor = Color.FromArgb(60,60,60);

            // Adding the panel to the form
            this.Controls.Add(panelConversations);

            // Add a scrollable list of conversation titles
            FlowLayoutPanel conversationList = new FlowLayoutPanel();
            conversationList.Dock = DockStyle.Fill;
            conversationList.FlowDirection = FlowDirection.TopDown;
            conversationList.WrapContents = false;
            conversationList.AutoScroll = true;
            panelConversations.Controls.Add(conversationList);

            Button btnNewConversation = new Button();
            btnNewConversation.Text = "New Conversation";
            btnNewConversation.Size = new Size(180, 50);
            btnNewConversation.Margin = new Padding(10);
            btnNewConversation.BackColor = Color.FromArgb(0, 122, 204);
            btnNewConversation.ForeColor = Color.White;
            btnNewConversation.FlatStyle = FlatStyle.Flat;
            btnNewConversation.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnNewConversation.ImageAlign = ContentAlignment.MiddleLeft;
            btnNewConversation.Padding = new Padding(10, 0, 0, 0);
            btnNewConversation.FlatAppearance.BorderSize = 0;
            btnNewConversation.Region = new Region(new Rectangle(0, 0, btnNewConversation.Width, btnNewConversation.Height));
            btnNewConversation.Cursor = Cursors.Hand;

            btnNewConversation.Click += (sender, e) => CreateNewConversation(conversationList);

            conversationList.Controls.Add(btnNewConversation);
        }

        private void CreateNewConversation(FlowLayoutPanel conversationList)
        {

            // Remove any empty conversations before creating a new one
            RemoveEmptyConversations(conversationList);

            string conversationName = "Conversation " + (conversations.Count + 1);

            conversations.Add(conversationName);
            conversationHistory[conversationName] = new List<string>();
            conversationSenders[conversationName] = new List<string>();

            AddConversationToPanel(conversationList, conversationName);
            selectedConversation = conversationName;
            LoadConversation(conversationName);
            UpdateConversationHighlight(conversationList);
        }

        private void RemoveEmptyConversations(FlowLayoutPanel conversationList)
        {
            var emptyConversations = new List<string>();

            foreach (var conversation in conversations)
            {
                if (conversationHistory[conversation].Count == 0) // If conversation has no messages
                {
                    emptyConversations.Add(conversation);
                }
            }

            foreach (var emptyConversation in emptyConversations)
            {
                // Remove from UI
                foreach (Control control in conversationList.Controls)
                {
                    if (control is Button btn && btn.Text == emptyConversation)
                    {
                        conversationList.Controls.Remove(btn);
                        break;
                    }
                }

                // Remove from the data structures
                conversations.Remove(emptyConversation);
                conversationHistory.Remove(emptyConversation);
                conversationSenders.Remove(emptyConversation);
            }
        }

        private void AddConversationToPanel(FlowLayoutPanel conversationList, string conversationName)
        {
            Button btnConversation = new Button
            {
                Text = conversationName,
                Size = new Size(180, 40),
                Margin = new Padding(10),
                BackColor = Color.LightGray,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.Black
            };

            if (conversationName == selectedConversation)
            {
                btnConversation.BackColor = Color.FromArgb(0, 122, 204);
                btnConversation.ForeColor = Color.White;
                btnConversation.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            }

            btnConversation.Click += (sender, e) =>
            {
                txtInput.Clear();
                selectedConversation = conversationName;
                LoadConversation(conversationName);
                UpdateConversationHighlight(conversationList);

                foreach (Control control in conversationList.Controls)
                {
                    Button btn = control as Button;
                    if (btn != null)
                    {
                        btn.BackColor = btn == btnConversation ? Color.FromArgb(50, 122, 204) : Color.LightGray;
                        btn.ForeColor = btn == btnConversation ? Color.White : Color.Black;
                        btn.Font = btn == btnConversation ? new Font("Segoe UI", 10, FontStyle.Bold) : new Font("Segoe UI", 10);
                    }
                }
            };

            conversationList.Controls.Add(btnConversation);
        }

        private void UpdateConversationHighlight(FlowLayoutPanel conversationList)
        {
            foreach (Control control in conversationList.Controls)
            {
                if (control is Button btnConversation)
                {
                    if (btnConversation.Text == selectedConversation)
                    {
                        btnConversation.BackColor = Color.FromArgb(0, 122, 204);
                        btnConversation.ForeColor = Color.White;
                        btnConversation.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    }
                    else
                    {
                        btnConversation.BackColor = Color.LightGray;
                        btnConversation.ForeColor = Color.Black;
                        btnConversation.Font = new Font("Segoe UI", 10);
                    }
                }
            }
        }

        private void LoadConversation(string conversationName)
        {
            panelChat.Controls.Clear();

            if (!string.IsNullOrEmpty(conversationName) && conversationHistory.ContainsKey(conversationName))
            {
                var messages = conversationHistory[conversationName];
                var senders = conversationSenders[conversationName];

                for (int i = 0; i < messages.Count; i++)
                {
                    AddMessage(senders[i], messages[i]);
                }
            }
        }

        private void StoreMessage(string conversationName, string sender, string message)
        {
            if (!conversationHistory.ContainsKey(conversationName))
            {
                conversationHistory[conversationName] = new List<string>();
                conversationSenders[conversationName] = new List<string>();
            }

            conversationHistory[conversationName].Add(message);
            conversationSenders[conversationName].Add(sender);
        }

        private void AddMessage(string sender, string message)
        {
            int chatWidth = 500;

            Panel centerContainer = new Panel
            {
                Width = chatWidth,
                AutoSize = true,
                BackColor = Color.Transparent,
                Margin = new Padding((panelChat.Width - chatWidth) / 2, 10, 0, 10)
            };

            Panel bubble = new Panel
            {
                AutoSize = true,
                MaximumSize = new Size(450, 0),
                Padding = new Padding(10),
                Margin = new Padding(0),
                BackColor = sender == "You" ? Color.FromArgb(0, 122, 204) : Color.FromArgb(60, 60, 60)
            };

            Label lbl = new Label
            {
                Text = message,
                AutoSize = true,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10)
            };

            bubble.Controls.Add(lbl);

            if (sender == "You")
            {
                bubble.Dock = DockStyle.Right;
            }
            else
            {
                bubble.Dock = DockStyle.Left;
            }

            Panel alignPanel = new Panel
            {
                Width = chatWidth,
                AutoSize = true
            };
            alignPanel.Controls.Add(bubble);

            centerContainer.Controls.Add(alignPanel);
            panelChat.Controls.Add(centerContainer);
            panelChat.ScrollControlIntoView(centerContainer);
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            string userInput = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(userInput) || string.IsNullOrEmpty(selectedConversation))
                return;

            AddMessage("You", userInput);
            StoreMessage(selectedConversation, "You", userInput);
            txtInput.Clear();

            AddMessage("AI", "...");

            string currentConversation = selectedConversation;

            if (selectedConversation == currentConversation)
            {
                var aiResponse = await GetAIResponseAsync(userInput);

                if (selectedConversation == currentConversation)
                {
                    if (panelChat.Controls.Count > 0)
                        panelChat.Controls.RemoveAt(panelChat.Controls.Count - 1);

                    AddMessage("AI", aiResponse);
                    StoreMessage(currentConversation, "AI", aiResponse);
                }
            }
        }

        private async Task<string> GetAIResponseAsync(string userInput)
        {
            await Task.Delay(1000);
            return "Here's a fake answer to: \n" + userInput;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panelChat.FlowDirection = FlowDirection.TopDown;
            panelChat.WrapContents = false;
            panelChat.AutoScroll = true;

            if (conversations.Count == 0)
            {
                foreach (Control control in this.Controls)
                {
                    if (control is Panel panel && panel.Dock == DockStyle.Left)
                    {
                        foreach (Control inner in panel.Controls)
                        {
                            if (inner is FlowLayoutPanel flow)
                            {
                                CreateNewConversation(flow);
                                selectedConversation = "Conversation 1";
                                LoadConversation("Conversation 1");
                                UpdateConversationHighlight(flow);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}