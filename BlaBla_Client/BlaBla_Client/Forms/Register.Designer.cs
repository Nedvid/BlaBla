namespace BlaBla_Client.Forms
{
    partial class Register
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Register));
            this.textbox_login = new System.Windows.Forms.TextBox();
            this.textbot_password = new System.Windows.Forms.TextBox();
            this.texbox_password_again = new System.Windows.Forms.TextBox();
            this.label_text = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.link_login = new System.Windows.Forms.LinkLabel();
            this.button_register = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
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
            // textbot_password
            // 
            this.textbot_password.Font = new System.Drawing.Font("Roboto Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textbot_password.Location = new System.Drawing.Point(12, 283);
            this.textbot_password.Name = "textbot_password";
            this.textbot_password.PasswordChar = '•';
            this.textbot_password.Size = new System.Drawing.Size(260, 22);
            this.textbot_password.TabIndex = 6;
            this.textbot_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // texbox_password_again
            // 
            this.texbox_password_again.Font = new System.Drawing.Font("Roboto Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.texbox_password_again.Location = new System.Drawing.Point(12, 326);
            this.texbox_password_again.Name = "texbox_password_again";
            this.texbox_password_again.PasswordChar = '•';
            this.texbox_password_again.Size = new System.Drawing.Size(260, 22);
            this.texbox_password_again.TabIndex = 7;
            this.texbox_password_again.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.texbox_password_again.TextChanged += new System.EventHandler(this.texbox_password_again_TextChanged);
            // 
            // label_text
            // 
            this.label_text.AutoSize = true;
            this.label_text.Font = new System.Drawing.Font("Roboto Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_text.Location = new System.Drawing.Point(65, 417);
            this.label_text.Name = "label_text";
            this.label_text.Size = new System.Drawing.Size(86, 15);
            this.label_text.TabIndex = 8;
            this.label_text.Text = "Masz już konto?";
            this.label_text.Click += new System.EventHandler(this.label_text_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(62, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 160);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(257, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(15, 15);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // link_login
            // 
            this.link_login.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.link_login.AutoSize = true;
            this.link_login.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.link_login.Font = new System.Drawing.Font("Roboto Condensed", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.link_login.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.link_login.LinkColor = System.Drawing.Color.DarkGreen;
            this.link_login.Location = new System.Drawing.Point(157, 417);
            this.link_login.Name = "link_login";
            this.link_login.Size = new System.Drawing.Size(53, 15);
            this.link_login.TabIndex = 13;
            this.link_login.TabStop = true;
            this.link_login.Text = "Zaloguj się";
            this.link_login.UseWaitCursor = true;
            this.link_login.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_login_LinkClicked_1);
            // 
            // button_register
            // 
            this.button_register.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(137)))), ((int)(((byte)(29)))));
            this.button_register.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_register.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_register.Font = new System.Drawing.Font("Roboto", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_register.ForeColor = System.Drawing.Color.White;
            this.button_register.Location = new System.Drawing.Point(12, 365);
            this.button_register.Name = "button_register";
            this.button_register.Size = new System.Drawing.Size(260, 38);
            this.button_register.TabIndex = 14;
            this.button_register.Text = "Zarejestruj";
            this.button_register.UseVisualStyleBackColor = false;
            this.button_register.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto Condensed", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 15);
            this.label1.TabIndex = 15;
            this.label1.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto Condensed", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(12, 265);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 15);
            this.label2.TabIndex = 16;
            this.label2.Text = "Hasło";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Roboto Condensed", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(12, 308);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "Powtórz Hasło";
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(284, 476);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_register);
            this.Controls.Add(this.link_login);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label_text);
            this.Controls.Add(this.texbox_password_again);
            this.Controls.Add(this.textbot_password);
            this.Controls.Add(this.textbox_login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Register";
            this.Text = "Register";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textbox_login;
        private System.Windows.Forms.TextBox textbot_password;
        private System.Windows.Forms.TextBox texbox_password_again;
        private System.Windows.Forms.Label label_text;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.LinkLabel link_login;
        private System.Windows.Forms.Button button_register;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}