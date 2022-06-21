
namespace Serial_Port_Stream
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
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxContent = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.disconnect = new System.Windows.Forms.Button();
            this.buttonAutoOpen = new System.Windows.Forms.Button();
            this.buttonClearScreen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(983, 20);
            this.textBoxPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(141, 31);
            this.textBoxPort.TabIndex = 0;
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(920, 25);
            this.labelPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(44, 25);
            this.labelPort.TabIndex = 1;
            this.labelPort.Text = "Port";
            // 
            // textBoxContent
            // 
            this.textBoxContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxContent.BackColor = System.Drawing.Color.White;
            this.textBoxContent.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxContent.Location = new System.Drawing.Point(17, 80);
            this.textBoxContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxContent.Multiline = true;
            this.textBoxContent.Name = "textBoxContent";
            this.textBoxContent.ReadOnly = true;
            this.textBoxContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxContent.Size = new System.Drawing.Size(1107, 647);
            this.textBoxContent.TabIndex = 2;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(779, 18);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(107, 38);
            this.buttonConnect.TabIndex = 3;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // disconnect
            // 
            this.disconnect.Location = new System.Drawing.Point(641, 18);
            this.disconnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.disconnect.Name = "disconnect";
            this.disconnect.Size = new System.Drawing.Size(129, 38);
            this.disconnect.TabIndex = 4;
            this.disconnect.Text = "Disconnect";
            this.disconnect.UseVisualStyleBackColor = true;
            this.disconnect.Click += new System.EventHandler(this.disconnect_Click);
            // 
            // buttonAutoOpen
            // 
            this.buttonAutoOpen.Location = new System.Drawing.Point(17, 20);
            this.buttonAutoOpen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonAutoOpen.Name = "buttonAutoOpen";
            this.buttonAutoOpen.Size = new System.Drawing.Size(202, 38);
            this.buttonAutoOpen.TabIndex = 5;
            this.buttonAutoOpen.Text = "Open All Ports";
            this.buttonAutoOpen.UseVisualStyleBackColor = true;
            this.buttonAutoOpen.Click += new System.EventHandler(this.buttonAutoOpen_Click);
            // 
            // buttonClearScreen
            // 
            this.buttonClearScreen.Location = new System.Drawing.Point(227, 20);
            this.buttonClearScreen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonClearScreen.Name = "buttonClearScreen";
            this.buttonClearScreen.Size = new System.Drawing.Size(169, 38);
            this.buttonClearScreen.TabIndex = 6;
            this.buttonClearScreen.Text = "Clear Screen";
            this.buttonClearScreen.UseVisualStyleBackColor = true;
            this.buttonClearScreen.Click += new System.EventHandler(this.buttonClearScreen_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.buttonConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1143, 750);
            this.Controls.Add(this.buttonClearScreen);
            this.Controls.Add(this.buttonAutoOpen);
            this.Controls.Add(this.disconnect);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textBoxContent);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.textBoxPort);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textBoxContent;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button disconnect;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonAutoOpen;
        private System.Windows.Forms.Button buttonClearScreen;
    }
}

