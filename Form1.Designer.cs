namespace WinFormsApp2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtInput = new TextBox();
            btnSend = new Button();
            panel1 = new Panel();
            panelChat = new FlowLayoutPanel();
            panelConversations = new Panel();
            panel1.SuspendLayout();
            panelChat.SuspendLayout();
            SuspendLayout();
            // 
            // txtInput
            // 
            txtInput.BackColor = Color.FromArgb(60, 60, 60);
            txtInput.BorderStyle = BorderStyle.None;
            txtInput.ForeColor = Color.White;
            txtInput.Location = new Point(227, 30);
            txtInput.Multiline = true;
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(301, 48);
            txtInput.TabIndex = 1;
            // 
            // btnSend
            // 
            btnSend.BackColor = Color.FromArgb(0, 122, 204);
            btnSend.Cursor = Cursors.Hand;
            btnSend.FlatStyle = FlatStyle.Flat;
            btnSend.ForeColor = Color.White;
            btnSend.Location = new Point(548, 48);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(49, 30);
            btnSend.TabIndex = 2;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(txtInput);
            panel1.Controls.Add(btnSend);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 397);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 113);
            panel1.TabIndex = 3;
            // 
            // panelChat
            // 
            panelChat.AutoScroll = true;
            panelChat.BackColor = Color.FromArgb(40, 40, 40);
            panelChat.Controls.Add(panelConversations);
            panelChat.Dock = DockStyle.Fill;
            panelChat.Location = new Point(0, 0);
            panelChat.Name = "panelChat";
            panelChat.Size = new Size(800, 397);
            panelChat.TabIndex = 4;
            // 
            // panelConversations
            // 
            panelConversations.Location = new Point(3, 3);
            panelConversations.Name = "panelConversations";
            panelConversations.Size = new Size(153, 504);
            panelConversations.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            ClientSize = new Size(800, 510);
            Controls.Add(panelChat);
            Controls.Add(panel1);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            ShowIcon = false;
            Text = "Simple Chatbot UI";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelChat.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TextBox txtInput;
        private Button btnSend;
        private Panel panel1;
        private FlowLayoutPanel panelChat;
        private Panel panelConversations;
    }
}
