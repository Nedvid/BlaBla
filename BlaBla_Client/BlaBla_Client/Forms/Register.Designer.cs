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
            this.button_login = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.link_login = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // textbox_login
            // 
            this.textbox_login.Font = new System.Drawing.Font("Roboto Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textbox_login.Location = new System.Drawing.Point(12, 221);
            this.textbox_login.Name = "textbox_login";
            this.textbox_login.Size = new System.Drawing.Size(260, 22);
            this.textbox_login.TabIndex = 0;
            this.textbox_login.Text = "Login";
            // 
            // textbot_password
            // 
            this.textbot_password.Font = new System.Drawing.Font("Roboto Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textbot_password.Location = new System.Drawing.Point(12, 249);
            this.textbot_password.Name = "textbot_password";
            this.textbot_password.Size = new System.Drawing.Size(260, 22);
            this.textbot_password.TabIndex = 6;
            this.textbot_password.Text = "Hasło";
            // 
            // texbox_password_again
            // 
            this.texbox_password_again.Font = new System.Drawing.Font("Roboto Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.texbox_password_again.Location = new System.Drawing.Point(12, 277);
            this.texbox_password_again.Name = "texbox_password_again";
            this.texbox_password_again.Size = new System.Drawing.Size(260, 22);
            this.texbox_password_again.TabIndex = 7;
            this.texbox_password_again.Text = "Powtórz hasło";
            this.texbox_password_again.TextChanged += new System.EventHandler(this.texbox_password_again_TextChanged);
            // 
            // label_text
            // 
            this.label_text.AutoSize = true;
            this.label_text.Font = new System.Drawing.Font("Roboto Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label_text.Location = new System.Drawing.Point(77, 342);
            this.label_text.Name = "label_text";
            this.label_text.Size = new System.Drawing.Size(86, 15);
            this.label_text.TabIndex = 8;
            this.label_text.Text = "Masz już konto?";
            this.label_text.Click += new System.EventHandler(this.label_text_Click);
            // 
            // button_login
            // 
            this.button_login.Font = new System.Drawing.Font("Roboto Condensed", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button_login.Location = new System.Drawing.Point(105, 305);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(75, 23);
            this.button_login.TabIndex = 10;
            this.button_login.Text = "Zarejestruj";
            this.button_login.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(62, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(160, 160);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
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
            this.link_login.Font = new System.Drawing.Font("Roboto Condensed", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.link_login.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.link_login.LinkColor = System.Drawing.Color.DarkGreen;
            this.link_login.Location = new System.Drawing.Point(169, 342);
            this.link_login.Name = "link_login";
            this.link_login.Size = new System.Drawing.Size(53, 15);
            this.link_login.TabIndex = 13;
            this.link_login.TabStop = true;
            this.link_login.Text = "Zaloguj się";
            this.link_login.UseWaitCursor = true;
            this.link_login.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_login_LinkClicked_1);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(284, 377);
            this.Controls.Add(this.link_login);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button_login);
            this.Controls.Add(this.label_text);
            this.Controls.Add(this.texbox_password_again);
            this.Controls.Add(this.textbot_password);
            this.Controls.Add(this.textbox_login);
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
        private System.Windows.Forms.Button button_login;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.LinkLabel link_login;
    }
}