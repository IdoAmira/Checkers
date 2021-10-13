using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Checkers_IdoAmira
{
    public partial class FormGame : Form
    {
        public FormGame(GameProgress i_Game)
        {
            InitializeComponent();
            this.m_game = new GameProgress(i_Game);
            this.buildForm(m_game.GameBoard.NumOfRowsAndCols);
        }

        Button[,] MatrixButton;
        private GameProgress m_game;
        private bool m_IsFirstClick = true;
        private bool m_ThereIsAnotherSkip = false;
        private Location m_SourceButtonLocatin;
        //private Button m_ChosenButton;

        public Location SourceButtonLocatin
        {
            get { return this.m_SourceButtonLocatin; }
            set { this.m_SourceButtonLocatin = value; }
        }

        public bool IsFirstClick
        {
            get { return this.m_IsFirstClick; }
            set { this.m_IsFirstClick = value; }
        }

        public bool ThereIsAnotherSkip
        {
            get { return this.m_ThereIsAnotherSkip; }
            set { this.m_ThereIsAnotherSkip = value; }
        }

        public GameProgress Game
        {
            get { return this.m_game; }
        }

        private void buildForm(int i_NumOfRowsAndCols)
        {
            this.label1.Text = m_game.Players[0].Name;
            this.label1.Left = this.Left + 8;
            this.label2.Text = m_game.Players[1].Name;
            this.label2.Left = this.Width - 8 - this.label2.Width;
            this.pictureBoxPlayer1.Left = this.label1.Left + (this.label1.Width / 2) - (this.pictureBoxPlayer1.Width / 2);
            this.pictureBoxPlayer2.Left = this.label2.Left + (this.label2.Width / 2) - (this.pictureBoxPlayer2.Width / 2);
            MatrixButton = new Button[i_NumOfRowsAndCols, i_NumOfRowsAndCols];
            int LocationY = this.Top + 100;
            int LocationX = this.Left;
            for (int i = 0; i < i_NumOfRowsAndCols; ++i)
            {
                for (int j = 0; j < i_NumOfRowsAndCols; ++j)
                {
                    Button button = new Button();
                    button.Top = LocationY;
                    button.Left = LocationX;
                    button.Width = 50;
                    button.Height = 50;
                    //button.UseVisualStyleBackColor = true;
                    //button.Visible = true;
                    if (i % 2 == j % 2)
                    {
                        button.BackColor = Color.White;
                        button.Enabled = false;
                    }
                    else
                    {
                        button.BackColor = Color.Black;
                    }
                    button.Click += new System.EventHandler(this.buttons_Click);
                    this.Controls.Add(button);
                    MatrixButton[i, j] = button;
                    //Buttons.Add(button);
                    LocationX += 50;
                }
                LocationY += 50;
                LocationX = this.Left;
            }
            this.Width = MatrixButton[i_NumOfRowsAndCols - 1, i_NumOfRowsAndCols - 1].Right + 16;
            this.Height = MatrixButton[i_NumOfRowsAndCols - 1, i_NumOfRowsAndCols - 1].Bottom + 39;
            updateSoldiers();
            displayTurn(0);
            this.blockIrrelevantButtons(this.getPlayerTurn());
        }

        //private void updateSoldiers1()
        //{
        //    for (int i = 0; i < this.Game.GameBoard.NumOfRowsAndCols; ++i)
        //    {
        //        for (int j = 0; j < this.Game.GameBoard.NumOfRowsAndCols; ++j)
        //        {
        //            if (this.Game.GameBoard.Matrix[i, j].Status == eStatus.ExistCoin)
        //            {
        //                if (this.Game.GameBoard.Matrix[i, j].Owner == eOwner.Player1)
        //                {
        //                    MatrixButton[i, j].BackgroundImage = this.pictureBoxPlayer1.BackgroundImage;
        //                    MatrixButton[i, j].BackgroundImageLayout = ImageLayout.Zoom;
        //                }
        //                else if (this.Game.GameBoard.Matrix[i, j].Owner == eOwner.Player2)
        //                {
        //                    MatrixButton[i, j].BackgroundImage = this.pictureBoxPlayer2.BackgroundImage;
        //                    MatrixButton[i, j].BackgroundImageLayout = ImageLayout.Zoom;
        //                    //MatrixButton[i, j].Enabled = false;
        //                }
        //            }
        //            else if(this.Game.GameBoard.Matrix[i, j].Status == eStatus.NotExistCoin)
        //            {
        //                MatrixButton[i, j].BackgroundImage = null;
        //            }
        //        }
        //    }
        //}

        private void updateSoldiers()
        {
            for (int i = 0; i < this.Game.GameBoard.NumOfRowsAndCols; ++i)
            {
                for (int j = 0; j < this.Game.GameBoard.NumOfRowsAndCols; ++j)
                {
                    if (this.Game.GameBoard.Matrix[i, j].Status == eStatus.ExistCoin)
                    {
                        if (this.Game.GameBoard.Matrix[i, j].Sign == 'O')
                        {
                            MatrixButton[i, j].BackgroundImage = this.pictureBoxPlayer1.BackgroundImage;
                            MatrixButton[i, j].BackgroundImageLayout = ImageLayout.Zoom;
                        }
                        else if (this.Game.GameBoard.Matrix[i, j].Sign == 'U')
                        {
                            MatrixButton[i, j].BackgroundImage = this.pictureBoxKingPlayer1.BackgroundImage;
                            MatrixButton[i, j].BackgroundImageLayout = ImageLayout.Zoom;
                            //MatrixButton[i, j].Enabled = false;
                        }
                        else if (this.Game.GameBoard.Matrix[i, j].Sign == 'X')
                        {
                            MatrixButton[i, j].BackgroundImage = this.pictureBoxPlayer2.BackgroundImage;
                            MatrixButton[i, j].BackgroundImageLayout = ImageLayout.Zoom;
                            //MatrixButton[i, j].Enabled = false;
                        }
                        else if (this.Game.GameBoard.Matrix[i, j].Sign == 'K')
                        {
                            MatrixButton[i, j].BackgroundImage = this.pictureBoxKingPlayer2.BackgroundImage;
                            MatrixButton[i, j].BackgroundImageLayout = ImageLayout.Zoom;
                            //MatrixButton[i, j].Enabled = false;
                        }
                    }
                    else if (this.Game.GameBoard.Matrix[i, j].Status == eStatus.NotExistCoin)
                    {
                        MatrixButton[i, j].BackgroundImage = null;
                    }
                }
            }
        }

        private void displayTurn(int i_PlayerTurn)
        {
            if (this.Game.Players[i_PlayerTurn].CoinSign == 'O')
            {
                this.pictureBoxTurn.BackgroundImage = this.pictureBoxPlayer1.BackgroundImage;
            }
            else if (this.Game.Players[i_PlayerTurn].CoinSign == 'X')
            {
                this.pictureBoxTurn.BackgroundImage = this.pictureBoxPlayer2.BackgroundImage;
            }
            this.pictureBoxTurn.Left = this.pictureBoxPlayer1.Right + (this.pictureBoxPlayer2.Left - this.pictureBoxPlayer1.Right) / 2 - 33;
            this.labelTurn.Left = this.pictureBoxTurn.Right;
        }

        private void displayUserErrorWindow(eUserError i_Error)
        {
            string str = null;
            int i_ErrorIndex = (int)i_Error;
            switch (i_ErrorIndex)
            {
                case 1:
                    str = "There is no coin in the source cell.";
                    break;
                case 2:
                    str = "The coin you want to move is not yours.";
                    break;
                case 3:
                    str = "You're trying to get to an illegal cell.";
                    break;
                case 4:
                    str = "The coin you want to move is blocked.";
                    break;
            }
            MessageBox.Show(str);
        }

        private void displayMoveErrorWindow(eTypeOfMove i_MoveError)
        {
            string str = null;
            int i_ErrorIndex = (int)i_MoveError;
            switch (i_ErrorIndex)
            {
                case 3:
                    str = "Pay attention! You must eat again:  ";
                    break;
                case 4:
                    str = "Pay attention! You must eat your opponent, please try again:  ";
                    break;
                case 5:
                    str = "illegal move, please try again:  ";
                    break;
            }
            MessageBox.Show(str);
        }
       
        //private string calculateMove()
        //{
        //    return "a";
        //}

        //public int[] getMove(int i_PlayerTurn, out eUserError o_Error)
        //{
        //    displayTurn(i_PlayerTurn);

        //    int[] Source = { m_SourceButton.X, m_SourceButton.Y };
        //    //eUserError o_Error;
        //    while (!m_game.ChecSourceIsValid(Source, i_PlayerTurn, out o_Error))
        //    {
        //        displayErrorWindow(o_Error);
        //        Source[0] = m_SourceButton.X;
        //        Source[1] = m_SourceButton.Y;
        //    }
        //    return Source;
        //}

        private int[] findButtonLocation(Button TheSender)
        {
            int[] ButtonLocation = new int[2];
            for (int i = 0; i < m_game.GameBoard.NumOfRowsAndCols; ++i)
            {
                for (int j = 0; j < m_game.GameBoard.NumOfRowsAndCols; ++j)
                {
                    {
                        if (MatrixButton[i, j] == TheSender)
                        {
                            ButtonLocation[0] = i;
                            ButtonLocation[1] = j;
                            goto find;
                        }
                    }
                }
            }
            find:
            return ButtonLocation;
        }

        private int getPlayerTurn()
        {
            int PlayerTurn = this.pictureBoxTurn.BackgroundImage == this.pictureBoxPlayer1.BackgroundImage ? 0 : 1;
            return PlayerTurn;
        }

        private void doOnSourceButton(int[] i_Source, int i_playerTurn)
        {
            eUserError o_ErrorType;
            if (this.Game.CheckSourceIsValid(i_Source, i_playerTurn, out o_ErrorType))
            {
                this.SourceButtonLocatin = new Location(i_Source[0], i_Source[1]);
                this.IsFirstClick = false;
                MatrixButton[i_Source[0],i_Source[1]].BackColor = Color.BlueViolet;
            }
            else
            {
                //MatrixButton[i_Source[0], i_Source[1]].BackColor = Color.Black;
                displayUserErrorWindow(o_ErrorType);
                MatrixButton[i_Source[0], i_Source[1]].BackColor = Color.Black;
            }
        }

        private void doOnTargetButton(int[] i_Target, int i_playerTurn)
        {
            eUserError o_ErrorType;
            if (this.Game.CheckTargetIsValid(i_Target, i_playerTurn, out o_ErrorType))
            {
                int[] Source = { this.SourceButtonLocatin.X, this.SourceButtonLocatin.Y };
                eTypeOfMove TypeOfMove = this.Game.Domove(Source, i_Target);
                if ((TypeOfMove == eTypeOfMove.IllegalMove) || (TypeOfMove == eTypeOfMove.MustToEat))
                {
                    displayMoveErrorWindow(TypeOfMove);
                }
                else
                {
                    this.Game.TryMakeKing(i_Target);
                    this.updateSoldiers();
                    if (TypeOfMove == eTypeOfMove.CanMoreSkip)
                    {
                        this.ThereIsAnotherSkip = true;
                        MatrixButton[this.SourceButtonLocatin.X, this.SourceButtonLocatin.Y].BackColor = Color.Black;
                        MatrixButton[i_Target[0], i_Target[1]].BackColor = Color.DarkBlue;
                        this.SourceButtonLocatin.X = i_Target[0];
                        this.SourceButtonLocatin.Y = i_Target[1];
                    }
                    else
                    {
                        //לפה הגענו בסוף המהלך
                        if (!this.Game.CheckIfExistsMoves(this.Game.Players[(i_playerTurn + 1) % 2]))
                        {
                            eOwner TheWinner = (eOwner)((i_playerTurn % 2) + 1);
                            this.Game.UpdateScore(TheWinner);
                            string ScoreMessage = "the winner is:", str2 = this.Game.Players[i_playerTurn % 2].Name;
                            MessageBox.Show(ScoreMessage + str2);
                        }
                        MatrixButton[this.SourceButtonLocatin.X, this.SourceButtonLocatin.Y].BackColor = Color.Black;
                        this.changePlayerTurn(i_playerTurn);
                        //this.blockIrrelevantButtons((i_playerTurn + 1) % 2);
                        this.IsFirstClick = true;
                    }
                }
                
            }
            else
            {
                displayUserErrorWindow(o_ErrorType);
            }
        }

        private void doAnotherSkip(int[] i_Target, int i_playerTurn)
        {
            eUserError o_ErrorType;
            if (this.Game.CheckTargetIsValid(i_Target, i_playerTurn, out o_ErrorType))
            {
                int[] Source = { this.SourceButtonLocatin.X, this.SourceButtonLocatin.Y };
                eTypeOfMove TypeOfMove = this.Game.Domove(Source, i_Target);
                if ((TypeOfMove == eTypeOfMove.IllegalMove) || (TypeOfMove == eTypeOfMove.RegularMove) || (TypeOfMove == eTypeOfMove.MustToEat)) 
                {
                    displayMoveErrorWindow(eTypeOfMove.CanMoreSkip);
                }
                else
                {
                    this.Game.TryMakeKing(i_Target);
                    this.updateSoldiers();
                    if (TypeOfMove == eTypeOfMove.CanMoreSkip)
                    {
                        this.ThereIsAnotherSkip = true;
                        MatrixButton[this.SourceButtonLocatin.X, this.SourceButtonLocatin.Y].BackColor = Color.Black;
                        MatrixButton[i_Target[0], i_Target[1]].BackColor = Color.DarkBlue;
                        this.SourceButtonLocatin.X = i_Target[0];
                        this.SourceButtonLocatin.Y = i_Target[1];
                    }
                    else //TypeOfMove == eTypeOfMove.SkipMove
                    {
                        //לפה הגענו בסוף המהלך
                        if (!this.Game.CheckIfExistsMoves(this.Game.Players[(i_playerTurn + 1) % 2]))
                        {
                            eOwner TheWinner = (eOwner)((i_playerTurn % 2) + 1);
                            this.Game.UpdateScore(TheWinner);
                            string ScoreMessage = "the winner is:", str2 = this.Game.Players[i_playerTurn % 2].Name;
                            MessageBox.Show(ScoreMessage + str2);
                        }
                        MatrixButton[this.SourceButtonLocatin.X, this.SourceButtonLocatin.Y].BackColor = Color.Black;
                        this.changePlayerTurn(i_playerTurn);
                        //this.blockIrrelevantButtons((i_playerTurn + 1) % 2);
                        this.IsFirstClick = true;
                        this.ThereIsAnotherSkip = false;
                    }
                }

            }
            else
            {
                displayMoveErrorWindow(eTypeOfMove.CanMoreSkip);
            }

        }

        private void blockIrrelevantButtons(int i_PlayerTurn)
        {
            for (int i = 0; i < m_game.GameBoard.NumOfRowsAndCols; ++i)
            {
                for (int j = 0; j < m_game.GameBoard.NumOfRowsAndCols; ++j)
                {
                    if (i_PlayerTurn == 0)
                    {
                        if ((this.Game.GameBoard.Matrix[i, j].Owner == eOwner.Player2) || (i % 2 == j % 2)) 
                        {
                            MatrixButton[i, j].Enabled = false;
                        }
                        else
                        {
                            MatrixButton[i, j].Enabled = true;
                        }
                    }
                    else
                    {
                        if ((this.Game.GameBoard.Matrix[i, j].Owner == eOwner.Player1) || (i % 2 == j % 2)) 
                        {
                            MatrixButton[i, j].Enabled = false;
                        }
                        else
                        {
                            MatrixButton[i, j].Enabled = true;
                        }
                    }
                }
            }
        }

        private void changePlayerTurn(int i_playerTurn)
        {
            this.displayTurn((i_playerTurn + 1) % 2);
            this.blockIrrelevantButtons((i_playerTurn + 1) % 2);
            if (this.Game.Players[1].TypeOfPlayer == eTypeOfPlayer.Computer)
            {
                this.updateSoldiers();
                doComputerMove();
                this.displayTurn(i_playerTurn % 2);
                this.blockIrrelevantButtons(i_playerTurn % 2);
            }
        }

        private void doComputerMove()
        {
            bool v_DoubleSkip;
            int[] Source = new int[2];
            int[] TargetStep1 = new int[2];
            int[] TargetStep2 = new int[2];
            int[] TargetStep3 = new int[2];
            Game.AImove(out Source, out TargetStep1, out TargetStep2, out v_DoubleSkip);
            doOneComputerMove(Source, TargetStep1);
            if (v_DoubleSkip)
            {
                doOneComputerMove(TargetStep1, TargetStep2);
            }
            else
            {
                TargetStep2 = TargetStep1;
            }
            while (Game.checkIfCanEat(TargetStep2[0], TargetStep2[1]))
            {
                TargetStep3 = Game.FindTargetOfEating(TargetStep2);
                doOneComputerMove(TargetStep2, TargetStep3);
                TargetStep2 = TargetStep3;
            }

        }

        private void doOneComputerMove(int[] i_Source, int[] i_Target)
        {
            MatrixButton[i_Source[0], i_Source[1]].BackColor = Color.BlueViolet;
            //timer
            Thread.Sleep(1000);
            MatrixButton[i_Source[0], i_Source[1]].BackColor = Color.Black;
            Game.Domove(i_Source, i_Target);
            this.Game.TryMakeKing(i_Target);
            this.updateSoldiers();
            MatrixButton[i_Target[0], i_Target[1]].BackColor = Color.BlueViolet;
            //timer
            Thread.Sleep(1000);
            MatrixButton[i_Target[0], i_Target[1]].BackColor = Color.Black;
        }

        private void buttons_Click(object sender, EventArgs e)
        {
            Button TheSender = sender as Button;
            int[] ButtonLocation = this.findButtonLocation(TheSender);
            int PlayerTurn = getPlayerTurn();
            
            if (this.IsFirstClick)
            {
                this.doOnSourceButton(ButtonLocation, PlayerTurn);
            }
            else
            {
                if (!this.ThereIsAnotherSkip)
                {
                    if (this.Game.GameBoard.Matrix[ButtonLocation[0], ButtonLocation[1]].Owner == this.Game.GameBoard.Matrix[SourceButtonLocatin.X, SourceButtonLocatin.Y].Owner)
                    {
                        if ((ButtonLocation[0] == this.SourceButtonLocatin.X) && (ButtonLocation[1] == this.SourceButtonLocatin.Y))
                        {
                            MatrixButton[this.SourceButtonLocatin.X, this.SourceButtonLocatin.Y].BackColor = Color.Black;
                            this.IsFirstClick = true;
                        }
                        else
                        {
                            MatrixButton[ButtonLocation[0], ButtonLocation[1]].BackColor = Color.BlueViolet;
                            MatrixButton[SourceButtonLocatin.X, SourceButtonLocatin.Y].BackColor = Color.Black;
                            this.doOnSourceButton(ButtonLocation, PlayerTurn);
                        }
                    }
                    else
                    {                       
                            this.doOnTargetButton(ButtonLocation, PlayerTurn);
                    }
                }
                else
                {
                    this.doAnotherSkip(ButtonLocation, PlayerTurn);
                }
            }
        }
    }
}
