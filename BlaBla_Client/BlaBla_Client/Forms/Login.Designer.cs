namespace BlaBla_Client.Forms
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.textbox_login = new System.Windows.Forms.TextBox();
            this.textbox_password = new System.Windows.Forms.TextBox();
            this.button_login = new System.Windows.Forms.Button();
            this.label_text = new System.Windows.Forms.Label();
            this.link_register = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textbox_login
            // 
            this.textbox_login.Font = new System.Drawing.Font("Roboto Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textbox_login.Location = new System.Drawing.Point(12, 240);
            this.textbox_login.Name = "textbox_login";
            this.textbox_login.Size = new System.Drawing.Size(260, 22);
            this.textbox_login.TabIndex = 0;
            this.textbox_login.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textbox_password
            // 
            this.textbox_password.Font = new System.Drawing.Font("Roboto Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textbox_password.Location = new System.Drawing.Point(12, 283);
            this.textbox_password.Name = "textbox_password";
            this.textbox_password.PasswordChar = '•';
            this.textbox_password.Size = new System.Drawing.Size(260, 22);
            this.textbox_password.TabIndex = 1;
            this.textbox_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_login
            // 
            this.button_login.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(137)))), ((int)(((byte)(29)))));
            this.button_login.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_login.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_login.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_login.ForeColor = System.Drawing.Color.White;
            this.button_login.Location = new System.Drawing.Point(12, 325);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(260, 38);
            this.button_login.TabIndex = 2;
            this.button_login.Text = "Zaloguj";
            this.button_login.UseVisualStyleBackColor = false;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // label_text
            // 
            this.label_text.AutoSize = true;
            this.label_text.Font = new System.Drawing.Font("Roboto Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_text.Location = new System.Drawing.Point(31, 377);
            this.label_text.Name = "label_text";
            this.label_text.Size = new System.Drawing.Size(129, 15);
            this.label_text.TabIndex = 3;
            this.label_text.Text = "Nie masz jeszcze konta?";
            this.label_text.Click += new System.EventHandler(this.label_text_Click);
            // 
            // link_register
            // 
            this.link_register.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.link_register.AutoSize = true;
            this.link_register.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.link_register.Font = new System.Drawing.Font("Roboto Condensed", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.link_register.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.link_register.LinkColor = System.Drawing.Color.DarkGreen;
            this.link_register.Location = new System.Drawing.Point(166, 377);
            this.link_register.Name = "link_register";
            this.link_register.Size = new System.Drawing.Size(72, 15);
            this.link_register.TabIndex = 4;
            this.link_register.TabStop = true;
            this.link_register.Text = "Zarejestruj się!";
            this.link_register.UseWaitCursor = true;
            this.link_register.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_register_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(62, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 160);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto Condensed", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 222);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto Condensed", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(12, 265);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 15);
            this.label2.TabIndex = 17;
            this.label2.Text = "Hasło";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(284, 430);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.link_register);
            this.Controls.Add(this.label_text);
            this.Controls.Add(this.button_login);
            this.Controls.Add(this.textbox_password);
            this.Controls.Add(this.textbox_login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textbox_login;
        private System.Windows.Forms.TextBox textbox_password;
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.Label label_text;
        private System.Windows.Forms.LinkLabel link_register;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}