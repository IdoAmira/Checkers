namespace Checkers_IdoAmira
{
    partial class FormGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGame));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBoxPlayer1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPlayer2 = new System.Windows.Forms.PictureBox();
            this.labelTurn = new System.Windows.Forms.Label();
            this.pictureBoxTurn = new System.Windows.Forms.PictureBox();
            this.pictureBoxKingPlayer1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxKingPlayer2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTurn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxKingPlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxKingPlayer2)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Miriam", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(27, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "player1";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Miriam", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(158, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "player2";
            // 
            // pictureBoxPlayer1
            // 
            this.pictureBoxPlayer1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBoxPlayer1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxPlayer1.BackgroundImage")));
            this.pictureBoxPlayer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxPlayer1.Location = new System.Drawing.Point(45, 45);
            this.pictureBoxPlayer1.Name = "pictureBoxPlayer1";
            this.pictureBoxPlayer1.Size = new System.Drawing.Size(44, 50);
            this.pictureBoxPlayer1.TabIndex = 2;
            this.pictureBoxPlayer1.TabStop = false;
            // 
            // pictureBoxPlayer2
            // 
            this.pictureBoxPlayer2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBoxPlayer2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxPlayer2.BackgroundImage")));
            this.pictureBoxPlayer2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxPlayer2.Location = new System.Drawing.Point(175, 45);
            this.pictureBoxPlayer2.Name = "pictureBoxPlayer2";
            this.pictureBoxPlayer2.Size = new System.Drawing.Size(44, 50);
            this.pictureBoxPlayer2.TabIndex = 3;
            this.pictureBoxPlayer2.TabStop = false;
            // 
            // labelTurn
            // 
            this.labelTurn.AutoSize = true;
            this.labelTurn.Font = new System.Drawing.Font("MV Boli", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTurn.Location = new System.Drawing.Point(113, 74);
            this.labelTurn.Name = "labelTurn";
            this.labelTurn.Size = new System.Drawing.Size(45, 21);
            this.labelTurn.TabIndex = 4;
            this.labelTurn.Text = "Turn";
            // 
            // pictureBoxTurn
            // 
            this.pictureBoxTurn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxTurn.Location = new System.Drawing.Point(95, 74);
            this.pictureBoxTurn.Name = "pictureBoxTurn";
            this.pictureBoxTurn.Size = new System.Drawing.Size(21, 21);
            this.pictureBoxTurn.TabIndex = 5;
            this.pictureBoxTurn.TabStop = false;
            // 
            // pictureBoxKingPlayer1
            // 
            this.pictureBoxKingPlayer1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxKingPlayer1.BackgroundImage")));
            this.pictureBoxKingPlayer1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxKingPlayer1.Location = new System.Drawing.Point(45, 101);
            this.pictureBoxKingPlayer1.Name = "pictureBoxKingPlayer1";
            this.pictureBoxKingPlayer1.Size = new System.Drawing.Size(44, 50);
            this.pictureBoxKingPlayer1.TabIndex = 6;
            this.pictureBoxKingPlayer1.TabStop = false;
            this.pictureBoxKingPlayer1.Visible = false;
            // 
            // pictureBoxKingPlayer2
            // 
            this.pictureBoxKingPlayer2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBoxKingPlayer2.BackgroundImage")));
            this.pictureBoxKingPlayer2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxKingPlayer2.Location = new System.Drawing.Point(175, 101);
            this.pictureBoxKingPlayer2.Name = "pictureBoxKingPlayer2";
            this.pictureBoxKingPlayer2.Size = new System.Drawing.Size(44, 50);
            this.pictureBoxKingPlayer2.TabIndex = 7;
            this.pictureBoxKingPlayer2.TabStop = false;
            this.pictureBoxKingPlayer2.Visible = false;
            // 
            // FormGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(262, 212);
            this.Controls.Add(this.pictureBoxKingPlayer2);
            this.Controls.Add(this.pictureBoxKingPlayer1);
            this.Controls.Add(this.pictureBoxTurn);
            this.Controls.Add(this.labelTurn);
            this.Controls.Add(this.pictureBoxPlayer2);
            this.Controls.Add(this.pictureBoxPlayer1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormGame";
            this.Text = "FormGame";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayer2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTurn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxKingPlayer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxKingPlayer2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxPlayer1;
        private System.Windows.Forms.PictureBox pictureBoxPlayer2;
        private System.Windows.Forms.Label labelTurn;
        private System.Windows.Forms.PictureBox pictureBoxTurn;
        private System.Windows.Forms.PictureBox pictureBoxKingPlayer1;
        private System.Windows.Forms.PictureBox pictureBoxKingPlayer2;
    }
}