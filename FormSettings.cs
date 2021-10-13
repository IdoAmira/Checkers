using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Checkers_IdoAmira
{
    public partial class FormSettings : Form
    {
        private GameProgress m_gameProgres;

        public FormSettings()
        {
            InitializeComponent();
        }

        public bool Board10Choosen
        {
            get { return this.radioButton10X10.Checked; }
        }

        public bool Board8Choosen
        {
            get { return this.radioButton8X8.Checked; }
        }

        public bool Board6Choosen
        {
            get { return this.radioButton6X6.Checked; }
        }

        public bool AgainstComputerChoosen
        {
            get { return this.radioButtonAgainstComputer.Checked; }
        }

        public bool AgainstFriendChoosen
        {
            get { return this.radioButtonAgainstFriend.Checked; }
        }

        public string PlayerOneName
        {
            get { return textBoxPlayerOneName.Text; }
        }

        public string PlayerTwoName
        {
            get { return textBoxPlayerTwoName.Text; }
        }

        public GameProgress GameProgres
        {
            get { return this.m_gameProgres; }
        }

        private void radioButtonAgainstFriend_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxPlayerTwoName.Visible = true;
            this.labelPlayerTwoName.Visible = true;
        }

        private void radioButtonAgainstComputer_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxPlayerTwoName.Visible = false;
            this.labelPlayerTwoName.Visible = false;
            this.textBoxPlayerTwoName.Text = null;
        }

        private void buttons_Click(object sender, EventArgs e)
        {
            if (sender == buttonStart)
            {
                this.DialogResult = DialogResult.OK;
                if (GameProgress.CheckName(this.PlayerOneName))
                {
                    if (this.AgainstFriendChoosen)
                    {
                        if (GameProgress.CheckName(this.PlayerTwoName))
                        {
                            m_gameProgres = new GameProgress(this.getBoardSize(), this.PlayerOneName, this.PlayerTwoName);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Player two: You must enter a name that shorter than 10 and without spaces");
                        }
                    }
                    else
                    {
                        m_gameProgres = new GameProgress(this.getBoardSize(), this.PlayerOneName);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Player one: You must enter a name that shorter than 10 and without spaces");
                }
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            //this.DialogResult = sender == buttonStart ? DialogResult.OK : DialogResult.Cancel;         
        }

        private int getBoardSize()
        {
            int BoardSize = 0;
            if (this.Board10Choosen)
            {
                BoardSize = 10;
            }
            else if (this.Board8Choosen)
            {
                BoardSize = 8;
            }
            else if (this.Board6Choosen)
            {
                BoardSize = 6;
            }
            return BoardSize;
        }
    }
}
