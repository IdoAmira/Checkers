namespace Checkers_IdoAmira
{
    partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.groupBoxSelectOpponent = new System.Windows.Forms.GroupBox();
            this.textBoxPlayerTwoName = new System.Windows.Forms.TextBox();
            this.labelPlayerTwoName = new System.Windows.Forms.Label();
            this.radioButtonAgainstFriend = new System.Windows.Forms.RadioButton();
            this.radioButtonAgainstComputer = new System.Windows.Forms.RadioButton();
            this.groupBoxSelectBoardSize = new System.Windows.Forms.GroupBox();
            this.radioButton6X6 = new System.Windows.Forms.RadioButton();
            this.radioButton8X8 = new System.Windows.Forms.RadioButton();
            this.radioButton10X10 = new System.Windows.Forms.RadioButton();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelPlayerOneName = new System.Windows.Forms.Label();
            this.textBoxPlayerOneName = new System.Windows.Forms.TextBox();
            this.groupBoxSelectOpponent.SuspendLayout();
            this.groupBoxSelectBoardSize.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxSelectOpponent
            // 
            this.groupBoxSelectOpponent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSelectOpponent.Controls.Add(this.textBoxPlayerTwoName);
            this.groupBoxSelectOpponent.Controls.Add(this.labelPlayerTwoName);
            this.groupBoxSelectOpponent.Controls.Add(this.radioButtonAgainstFriend);
            this.groupBoxSelectOpponent.Controls.Add(this.radioButtonAgainstComputer);
            this.groupBoxSelectOpponent.Location = new System.Drawing.Point(12, 64);
            this.groupBoxSelectOpponent.Name = "groupBoxSelectOpponent";
            this.groupBoxSelectOpponent.Size = new System.Drawing.Size(297, 96);
            this.groupBoxSelectOpponent.TabIndex = 1;
            this.groupBoxSelectOpponent.TabStop = false;
            this.groupBoxSelectOpponent.Text = "Choose against whom you want to play:";
            // 
            // textBoxPlayerTwoName
            // 
            this.textBoxPlayerTwoName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPlayerTwoName.Location = new System.Drawing.Point(162, 59);
            this.textBoxPlayerTwoName.Name = "textBoxPlayerTwoName";
            this.textBoxPlayerTwoName.Size = new System.Drawing.Size(126, 20);
            this.textBoxPlayerTwoName.TabIndex = 4;
            this.textBoxPlayerTwoName.Visible = false;
            // 
            // labelPlayerTwoName
            // 
            this.labelPlayerTwoName.AutoSize = true;
            this.labelPlayerTwoName.Location = new System.Drawing.Point(33, 62);
            this.labelPlayerTwoName.Name = "labelPlayerTwoName";
            this.labelPlayerTwoName.Size = new System.Drawing.Size(127, 13);
            this.labelPlayerTwoName.TabIndex = 2;
            this.labelPlayerTwoName.Text = "Enter name of player two:";
            this.labelPlayerTwoName.Visible = false;
            // 
            // radioButtonAgainstFriend
            // 
            this.radioButtonAgainstFriend.AutoSize = true;
            this.radioButtonAgainstFriend.Location = new System.Drawing.Point(36, 42);
            this.radioButtonAgainstFriend.Name = "radioButtonAgainstFriend";
            this.radioButtonAgainstFriend.Size = new System.Drawing.Size(120, 17);
            this.radioButtonAgainstFriend.TabIndex = 3;
            this.radioButtonAgainstFriend.TabStop = true;
            this.radioButtonAgainstFriend.Text = "Play against a friend";
            this.radioButtonAgainstFriend.UseVisualStyleBackColor = true;
            this.radioButtonAgainstFriend.CheckedChanged += new System.EventHandler(this.radioButtonAgainstFriend_CheckedChanged);
            // 
            // radioButtonAgainstComputer
            // 
            this.radioButtonAgainstComputer.AutoSize = true;
            this.radioButtonAgainstComputer.Checked = true;
            this.radioButtonAgainstComputer.Location = new System.Drawing.Point(36, 19);
            this.radioButtonAgainstComputer.Name = "radioButtonAgainstComputer";
            this.radioButtonAgainstComputer.Size = new System.Drawing.Size(147, 17);
            this.radioButtonAgainstComputer.TabIndex = 2;
            this.radioButtonAgainstComputer.TabStop = true;
            this.radioButtonAgainstComputer.Text = "Play against the computer";
            this.radioButtonAgainstComputer.UseVisualStyleBackColor = true;
            this.radioButtonAgainstComputer.CheckedChanged += new System.EventHandler(this.radioButtonAgainstComputer_CheckedChanged);
            // 
            // groupBoxSelectBoardSize
            // 
            this.groupBoxSelectBoardSize.Controls.Add(this.radioButton6X6);
            this.groupBoxSelectBoardSize.Controls.Add(this.radioButton8X8);
            this.groupBoxSelectBoardSize.Controls.Add(this.radioButton10X10);
            this.groupBoxSelectBoardSize.Location = new System.Drawing.Point(12, 179);
            this.groupBoxSelectBoardSize.Name = "groupBoxSelectBoardSize";
            this.groupBoxSelectBoardSize.Size = new System.Drawing.Size(200, 100);
            this.groupBoxSelectBoardSize.TabIndex = 5;
            this.groupBoxSelectBoardSize.TabStop = false;
            this.groupBoxSelectBoardSize.Text = "Choose a board size";
            // 
            // radioButton6X6
            // 
            this.radioButton6X6.AutoSize = true;
            this.radioButton6X6.Location = new System.Drawing.Point(36, 65);
            this.radioButton6X6.Name = "radioButton6X6";
            this.radioButton6X6.Size = new System.Drawing.Size(50, 17);
            this.radioButton6X6.TabIndex = 8;
            this.radioButton6X6.TabStop = true;
            this.radioButton6X6.Text = "6 X 6";
            this.radioButton6X6.UseVisualStyleBackColor = true;
            // 
            // radioButton8X8
            // 
            this.radioButton8X8.AutoSize = true;
            this.radioButton8X8.Location = new System.Drawing.Point(36, 42);
            this.radioButton8X8.Name = "radioButton8X8";
            this.radioButton8X8.Size = new System.Drawing.Size(50, 17);
            this.radioButton8X8.TabIndex = 7;
            this.radioButton8X8.TabStop = true;
            this.radioButton8X8.Text = "8 X 8";
            this.radioButton8X8.UseVisualStyleBackColor = true;
            // 
            // radioButton10X10
            // 
            this.radioButton10X10.AutoSize = true;
            this.radioButton10X10.Checked = true;
            this.radioButton10X10.Location = new System.Drawing.Point(36, 19);
            this.radioButton10X10.Name = "radioButton10X10";
            this.radioButton10X10.Size = new System.Drawing.Size(62, 17);
            this.radioButton10X10.TabIndex = 6;
            this.radioButton10X10.TabStop = true;
            this.radioButton10X10.Text = "10 X 10";
            this.radioButton10X10.UseVisualStyleBackColor = true;
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Location = new System.Drawing.Point(105, 308);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(99, 23);
            this.buttonStart.TabIndex = 9;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttons_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(210, 308);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(99, 23);
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttons_Click);
            // 
            // labelPlayerOneName
            // 
            this.labelPlayerOneName.AutoSize = true;
            this.labelPlayerOneName.Location = new System.Drawing.Point(45, 38);
            this.labelPlayerOneName.Name = "labelPlayerOneName";
            this.labelPlayerOneName.Size = new System.Drawing.Size(87, 13);
            this.labelPlayerOneName.TabIndex = 4;
            this.labelPlayerOneName.Text = "Enter your name:";
            // 
            // textBoxPlayerOneName
            // 
            this.textBoxPlayerOneName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPlayerOneName.Location = new System.Drawing.Point(135, 35);
            this.textBoxPlayerOneName.Name = "textBoxPlayerOneName";
            this.textBoxPlayerOneName.Size = new System.Drawing.Size(165, 20);
            this.textBoxPlayerOneName.TabIndex = 0;
            // 
            // FormProperties
            // 
            this.AcceptButton = this.buttonStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(324, 343);
            this.Controls.Add(this.textBoxPlayerOneName);
            this.Controls.Add(this.labelPlayerOneName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.groupBoxSelectBoardSize);
            this.Controls.Add(this.groupBoxSelectOpponent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(461, 447);
            this.MinimumSize = new System.Drawing.Size(340, 382);
            this.Name = "FormProperties";
            this.Text = "Settings";
            this.groupBoxSelectOpponent.ResumeLayout(false);
            this.groupBoxSelectOpponent.PerformLayout();
            this.groupBoxSelectBoardSize.ResumeLayout(false);
            this.groupBoxSelectBoardSize.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxSelectOpponent;
        private System.Windows.Forms.TextBox textBoxPlayerTwoName;
        private System.Windows.Forms.Label labelPlayerTwoName;
        private System.Windows.Forms.RadioButton radioButtonAgainstFriend;
        private System.Windows.Forms.RadioButton radioButtonAgainstComputer;
        private System.Windows.Forms.GroupBox groupBoxSelectBoardSize;
        private System.Windows.Forms.RadioButton radioButton6X6;
        private System.Windows.Forms.RadioButton radioButton8X8;
        private System.Windows.Forms.RadioButton radioButton10X10;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelPlayerOneName;
        private System.Windows.Forms.TextBox textBoxPlayerOneName;
    }
}