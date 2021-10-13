using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers_IdoAmira
{
    public class Board
    {
        private Cell[,] m_BoardMatrix;
        private int m_NumOfRowsAndCols;


        public Board(int i_NumOfRowsAndCols)
        {
            this.m_BoardMatrix = new Cell[i_NumOfRowsAndCols, i_NumOfRowsAndCols];
            this.m_NumOfRowsAndCols = i_NumOfRowsAndCols;
            this.fillStartingMatrix();
            // this.m_NumOfGroupPlayers = (i_NumOfRowsAndCols / 2) * ((i_NumOfRowsAndCols / 2) - 1);
        }

        //public Board(Board i_BoardToCopy)
        //{
        //    this.m_NumOfRowsAndCols = i_BoardToCopy.NumOfRowsAndCols;
        //  //  this.m_NumOfGroupPlayers = i_BoardToCopy.NumOfGroupPlayers;
        //    this.m_BoardMatrix = new Cell[i_BoardToCopy.NumOfRowsAndCols, i_BoardToCopy.NumOfRowsAndCols];
        //    for (int i = 0; i < i_BoardToCopy.NumOfRowsAndCols; ++i)
        //    {
        //        for (int j = 0; j < i_BoardToCopy.NumOfRowsAndCols; ++j)
        //        {
        //            this.m_BoardMatrix[i, j] = i_BoardToCopy.Matrix[i, j];
        //        }
        //    }
        //}

        public Cell[,] Matrix
        {
            get
            {
                return this.m_BoardMatrix;
            }

            set
            {
                this.m_BoardMatrix = value;
            }
        }


        public int NumOfRowsAndCols
        {
            get
            {
                return this.m_NumOfRowsAndCols;
            }
        }

        private void fillStartingMatrix()
        {
            for (int i = 0; i < this.NumOfRowsAndCols; ++i)
            {
                for (int j = 0; j < this.NumOfRowsAndCols; ++j)
                {
                    if ((i % 2) != (j % 2))
                    {
                        if ((i == this.NumOfRowsAndCols / 2) || (i == (this.NumOfRowsAndCols / 2) - 1))
                        {
                            m_BoardMatrix[i, j].Status = eStatus.NotExistCoin;
                            m_BoardMatrix[i, j].Owner = eOwner.None;
                            m_BoardMatrix[i, j].Sign = ' ';

                        }
                        else
                        {
                            m_BoardMatrix[i, j].Status = eStatus.ExistCoin;
                            fillStartingCellSignAndOwner(i, j);
                        }
                    }
                    else
                    {
                        m_BoardMatrix[i, j].Status = eStatus.Illegal;
                        m_BoardMatrix[i, j].Sign = ' ';
                        m_BoardMatrix[i, j].Owner = eOwner.None;
                    }
                }
            }
        }

        private void fillStartingCellSignAndOwner(int i_row, int i_col)
        {
            if (i_row < (this.NumOfRowsAndCols / 2) - 1)
            {
                m_BoardMatrix[i_row, i_col].Sign = 'O';
                m_BoardMatrix[i_row, i_col].Owner = eOwner.Player1;
            }
            else if (i_row > (this.NumOfRowsAndCols / 2))
            {
                m_BoardMatrix[i_row, i_col].Sign = 'X';
                m_BoardMatrix[i_row, i_col].Owner = eOwner.Player2;
            }
        }
    }
}
