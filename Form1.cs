using System.Net.Http.Json;
using System.Windows.Forms;
using static WinFormsApp2.UIHelper;


namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        private List<string> conversations = new List<string>();
        private Dictionary<string, List<string>> conversationHistory = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> conversationSenders = new Dictionary<string, List<string>>();
        private string selectedConversation = null;
        private static readonly HttpClient httpClient = new HttpClient();
        private const string apiKey = "AIzaSyA334LMpBm4-oxYwo__b-76U5NrZX5_Ahk"; // Replace this
        private Panel topPanel;
        public Form1()
        {
            InitializeComponent();
            InitializeConversationPanel();
            AddTopPanel();
            UIHelper.RoundAllControls(this);
            panelChat.Top = 60;
            txtInput.KeyDown += TxtInput_KeyDown;
            this.SizeChanged += new EventHandler(Form_SizeChanged);
        }

        private void AddTopPanel()
        {
            topPanel = new Panel
            {
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(30, 30, 30)
            };

            Label titleLabel = new Label
            {
                Text = "Simple Chatbot",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 15)
            };

            topPanel.Controls.Add(titleLabel);
            this.Controls.Add(topPanel);
            ClearAllConversations();
        }

        private void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift) // Check if Enter is pressed (without Shift for multi-line input)
            {
                e.SuppressKeyPress = true; // Prevents the "ding" sound
                btnSend.PerformClick();
            }
        }

        private void InitializeConversationPanel()
        {
            // Check if the panel already exists
            Panel existingPanel = this.Controls.OfType<Panel>().FirstOrDefault(p => p.Dock == DockStyle.Left);

            // If it exists, clear it out, otherwise create a new one
            if (existingPanel != null)
            {
                existingPanel.Controls.Clear(); // Clears the previous content
                existingPanel.Dispose(); // Optional: dispose the old panel if you want to free up resources
            }

            // Create a new panel for conversations
            Panel panelConversations = new Panel();
            panelConversations.Dock = DockStyle.Left;
            panelConversations.Width = 200;
            panelConversations.BackColor = Color.FromArgb(60, 60, 60);

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
            btnNewConversation.Margin = new Padding(10, 10, 10, 25); // left, top, right, bottom
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
                Size = new Size(160, 40),
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
            UIHelper.RoundAllControls(this);
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

        public void ClearAllConversations()
        {
            conversations.Clear();
            conversationHistory.Clear();
            conversationSenders.Clear();
            selectedConversation = null;

            // Remove all controls from the left panel
            foreach (Control control in this.Controls)
            {
                if (control is Panel panel && panel.Dock == DockStyle.Left)
                {
                    foreach (Control inner in panel.Controls)
                    {
                        if (inner is FlowLayoutPanel flow)
                        {
                            flow.Controls.Clear();
                            break;
                        }
                    }
                }
            }

            // Clear chat messages and reset
            panelChat.Controls.Clear();

            InitializeConversationPanel();
            Form1_Load(null, EventArgs.Empty);
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
            int chatWidth = panelChat.Width - 40; // dynamic width minus padding
            int maxBubbleWidth = 450;

            // Outer container for alignment
            Panel container = new Panel
            {
                AutoSize = true,
                Width = chatWidth,
                Padding = new Padding(10, 5, 10, 5),
                BackColor = Color.Transparent
            };

            // Message label
            Label lbl = new Label
            {
                Text = message,
                AutoSize = true,
                MaximumSize = new Size(maxBubbleWidth, 0),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.Transparent
            };

            // Chat bubble
            Panel bubble = new Panel
            {
                AutoSize = true,
                Padding = new Padding(10),
                BackColor = sender == "You" ? Color.FromArgb(0, 122, 204) : Color.FromArgb(60, 60, 60),
                Margin = new Padding(0),
                MaximumSize = new Size(maxBubbleWidth + 20, 0)
            };

            bubble.Controls.Add(lbl);
            container.Controls.Add(bubble);

            // Alignment based on sender: right for "You", left for others
            this.BeginInvoke((MethodInvoker)delegate
            {
                if (sender == "You")
                {
                    bubble.Left = container.Width - bubble.Width - 10; // Align to right
                }
                else
                {
                    bubble.Left = 10; // Align to left
                }

                panelChat.Controls.Add(container);
                panelChat.ScrollControlIntoView(container);
                UIHelper.RoundAllControls(this);
            });

            UIHelper.RoundAllControls(this);
        }


        private void btnSettings_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            settingsForm.Owner = this;
            settingsForm.ShowDialog();
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
            try
            {
                var requestBody = new
                {
                    contents = new[]
                    {
                new
                {
                    role = "user",
                    parts = new[] { new { text = userInput } }
                }
            }
                };

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}"),
                    Content = JsonContent.Create(requestBody)
                };

                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseJson = await response.Content.ReadFromJsonAsync<GeminiResponse>();
                return responseJson?.candidates?[0]?.content?.parts?[0]?.text ?? "No response from AI.";
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        private void Form_SizeChanged(object sender, EventArgs e)
        {
            LoadConversation(selectedConversation);
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
            UIHelper.RoundAllControls(this);
        }

        public class GeminiResponse
        {
            public Candidate[] candidates { get; set; }
        }

        public class Candidate
        {
            public Content content { get; set; }
        }

        public class Content
        {
            public Part[] parts { get; set; }
        }

        public class Part
        {
            public string text { get; set; }
        }

    }
}