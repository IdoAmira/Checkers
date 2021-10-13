using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers_IdoAmira
{
    public class GameProgress
    {
        private Board m_GameBoard;
        Player[] m_Players = new Player[2];

        public GameProgress(int i_BoardSize, string i_FirstPlayerName, string m_SecondPlayerName)
        {
            this.m_GameBoard = new Board(i_BoardSize);
            //int i_NumOfCoins = (i_BoardSize / 2) * ((i_BoardSize / 2) - 1);
            this.m_Players[0] = new Player(i_FirstPlayerName, eTypeOfPlayer.Human, 'O');
            this.m_Players[1] = new Player(m_SecondPlayerName, eTypeOfPlayer.Human, 'X');
        }

        public GameProgress(int i_BoardSize, string i_FirstPlayerName)
        {
            this.m_GameBoard = new Board(i_BoardSize);
            //int i_NumOfCoins = (i_BoardSize / 2) * ((i_BoardSize / 2) - 1);
            this.m_Players[0] = new Player(i_FirstPlayerName, eTypeOfPlayer.Human, 'O');
            this.m_Players[1] = new Player("Computer", eTypeOfPlayer.Computer, 'X');
        }

        public GameProgress(GameProgress i_Game)
        {
            this.m_GameBoard = new Board(i_Game.GameBoard.NumOfRowsAndCols);
            this.m_Players[0] = new Player(i_Game.Players[0]);
            this.m_Players[1] = new Player(i_Game.Players[1]);
        }

        public Board GameBoard
        {
            get
            {
                return this.m_GameBoard;
            }
        }

        public Player[] Players
        {
            get
            {
                return this.m_Players;
            }
        }

        //good
        public static bool CheckName(string i_NameToCheck)
        {
            bool v_NameIsValid = true;
            if ((i_NameToCheck.Length <= 10) && (i_NameToCheck.Length != 0)) 
            {
                for (int i = 0; i < i_NameToCheck.Length; ++i)
                {
                    if (i_NameToCheck[i] == ' ')
                    {
                        v_NameIsValid = false;
                    }
                }
            }
            else
            {
                v_NameIsValid = false;
            }

            return v_NameIsValid;
        }

        //good
        public bool CheckSourceIsValid(int[] Source, int i_PlayerTurn, out eUserError o_ErrorType)
        {
            bool v_MoveIsValid = false;
            o_ErrorType = SourceUserErrors(Source, i_PlayerTurn);
            if ((o_ErrorType == eUserError.Valid))
            {
                v_MoveIsValid = true;
            }
            return v_MoveIsValid;
        }

        //good
        public bool CheckTargetIsValid(int[] Target, int i_PlayerTurn, out eUserError o_ErrorType)
        {
            bool v_MoveIsValid = false;
            o_ErrorType = TargetUserErrors(Target, i_PlayerTurn);
            if ((o_ErrorType == eUserError.Valid))
            {
                v_MoveIsValid = true;
            }
            return v_MoveIsValid;
        }

        //good
        public eUserError SourceUserErrors(int[] Source, int i_PlayerTurn)
        {
            eUserError ErrorType;
            char SignOfCurCoin = GameBoard.Matrix[Source[0], Source[1]].Sign;
            if (GameBoard.Matrix[Source[0], Source[1]].Status != eStatus.ExistCoin)
            {
                ErrorType = eUserError.NotExistCoin;
            }
            else if ((SignOfCurCoin != this.Players[i_PlayerTurn].CoinSign) && (SignOfCurCoin != this.Players[i_PlayerTurn].KingSign))
            {
                ErrorType = eUserError.NotYourCoin;
            }
            else if (!CheckIfCanMove(Source[0], Source[1]))
            {
                ErrorType = eUserError.CoinCantMove;
            }
            else
            {
                ErrorType = eUserError.Valid;
            }
            return ErrorType;
        }

        //good
        public eUserError TargetUserErrors(int[] Target, int i_PlayerTurn)
        {
            eUserError ErrorType;
            if (GameBoard.Matrix[Target[0], Target[1]].Status != eStatus.NotExistCoin)
            {
                ErrorType = eUserError.IllegalTarget;
            }
            else
            {
                ErrorType = eUserError.Valid;
            }
            return ErrorType;
        }

        public eTypeOfMove Domove(int[] Source, int[] Target)
        {
            eTypeOfMove MoveType = eTypeOfMove.IllegalMove;
            char CoinSign = GameBoard.Matrix[Source[0], Source[1]].Sign;
            if (CoinSign == 'X')
            {
                MoveType = MoveDownToUp(Source, Target);
            }
            else if (CoinSign == 'O')
            {
                MoveType = MoveUpToDown(Source, Target);
            }
            else if ((CoinSign == 'U') || (CoinSign == 'K'))
            {
                if (Source[0] > Target[0])
                {
                    MoveType = MoveDownToUp(Source, Target);
                }
                else
                {
                    MoveType = MoveUpToDown(Source, Target);
                }
            }
            return MoveType;
        }

        public eTypeOfMove MoveUpToDown(int[] i_Source, int[] i_Terget)
        {
            eTypeOfMove MoveType = eTypeOfMove.IllegalMove;
            eOwner CurPlayer = GameBoard.Matrix[i_Source[0], i_Source[1]].Owner;
            char CurSign = GameBoard.Matrix[i_Source[0], i_Source[1]].Sign;
            if ((i_Terget[0] - 1 == i_Source[0]) && ((i_Source[1] == i_Terget[1] + 1) || (i_Source[1] == i_Terget[1] - 1)))
            {
                if (checkIfMustToEat(CurPlayer))
                {
                    MoveType = eTypeOfMove.MustToEat;
                }
                else
                {
                    MoveType = eTypeOfMove.RegularMove;
                    updateSourceAndTargetCells(i_Source, i_Terget);
                }
            }
            else if ((i_Source[0] == i_Terget[0] - 2) && (i_Source[1] == i_Terget[1] - 2))
            {
                eOwner EatenOwner = GameBoard.Matrix[(i_Source[0] + 1), (i_Source[1] + 1)].Owner;
                if ((EatenOwner != CurPlayer) && (EatenOwner != eOwner.None))
                {
                    resetCell(i_Source[0] + 1, i_Source[1] + 1);
                    updateSourceAndTargetCells(i_Source, i_Terget);
                    if ((CurSign == 'K') || (CurSign == 'U'))
                    {
                        MoveType = checkIfMustToEatUpToDown(i_Terget[0], i_Terget[1]) || checkIfMustToEatDownToUp(i_Terget[0], i_Terget[1]) ? eTypeOfMove.CanMoreSkip : eTypeOfMove.SkipMove;
                    }
                    else
                    {
                        MoveType = checkIfMustToEatUpToDown(i_Terget[0], i_Terget[1]) ? eTypeOfMove.CanMoreSkip : eTypeOfMove.SkipMove;
                    }
                }

            }
            else if ((i_Source[0] == i_Terget[0] - 2) && (i_Source[1] == i_Terget[1] + 2))
            {
                eOwner EatenOwner = GameBoard.Matrix[(i_Source[0] + 1), (i_Source[1] - 1)].Owner;
                if ((EatenOwner != CurPlayer) && (EatenOwner != eOwner.None))
                {
                    resetCell(i_Source[0] + 1, i_Source[1] - 1);
                    updateSourceAndTargetCells(i_Source, i_Terget);
                    if ((CurSign == 'K') || (CurSign == 'U'))
                    {
                        MoveType = checkIfMustToEatUpToDown(i_Terget[0], i_Terget[1]) || checkIfMustToEatDownToUp(i_Terget[0], i_Terget[1]) ? eTypeOfMove.CanMoreSkip : eTypeOfMove.SkipMove;
                    }
                    else
                    {
                        MoveType = checkIfMustToEatUpToDown(i_Terget[0], i_Terget[1]) ? eTypeOfMove.CanMoreSkip : eTypeOfMove.SkipMove;
                    }
                }
            }
            return MoveType;
        }

        public eTypeOfMove MoveDownToUp(int[] i_Source, int[] i_Terget)
        {
            eTypeOfMove MoveType = eTypeOfMove.IllegalMove;
            eOwner CurPlayer = GameBoard.Matrix[i_Source[0], i_Source[1]].Owner;
            char CurSign = GameBoard.Matrix[i_Source[0], i_Source[1]].Sign;
            if ((i_Terget[0] + 1 == i_Source[0]) && ((i_Source[1] == i_Terget[1] + 1) || (i_Source[1] == i_Terget[1] - 1)))
            {
                if (checkIfMustToEat(CurPlayer))
                {
                    MoveType = eTypeOfMove.MustToEat;
                }
                else
                {
                    MoveType = eTypeOfMove.RegularMove;
                    updateSourceAndTargetCells(i_Source, i_Terget);
                }
            }
            else if ((i_Terget[0] + 2 == i_Source[0]) && (i_Source[1] == i_Terget[1] - 2))
            {
                eOwner i_EatenOwner = GameBoard.Matrix[(i_Source[0] - 1), (i_Source[1] + 1)].Owner;
                if ((i_EatenOwner != CurPlayer) && (i_EatenOwner != eOwner.None))
                {
                    resetCell(i_Source[0] - 1, i_Source[1] + 1);
                    updateSourceAndTargetCells(i_Source, i_Terget);
                    if ((CurSign == 'K') || (CurSign == 'U'))
                    {
                        MoveType = checkIfMustToEatUpToDown(i_Terget[0], i_Terget[1]) || checkIfMustToEatDownToUp(i_Terget[0], i_Terget[1]) ? eTypeOfMove.CanMoreSkip : eTypeOfMove.SkipMove;
                    }
                    else
                    {
                        MoveType = checkIfMustToEatDownToUp(i_Terget[0], i_Terget[1]) ? eTypeOfMove.CanMoreSkip : eTypeOfMove.SkipMove;
                    }
                }

            }
            else if ((i_Terget[0] + 2 == i_Source[0]) && (i_Source[1] == i_Terget[1] + 2))
            {
                eOwner i_EatenOwner = GameBoard.Matrix[(i_Source[0] - 1), (i_Source[1] - 1)].Owner;
                if ((i_EatenOwner != CurPlayer) && (i_EatenOwner != eOwner.None))
                {
                    resetCell(i_Source[0] - 1, i_Source[1] - 1);
                    updateSourceAndTargetCells(i_Source, i_Terget);
                    if ((CurSign == 'K') || (CurSign == 'U'))
                    {
                        MoveType = checkIfMustToEatUpToDown(i_Terget[0], i_Terget[1]) || checkIfMustToEatDownToUp(i_Terget[0], i_Terget[1]) ? eTypeOfMove.CanMoreSkip : eTypeOfMove.SkipMove;
                    }
                    else
                    {
                        MoveType = checkIfMustToEatDownToUp(i_Terget[0], i_Terget[1]) ? eTypeOfMove.CanMoreSkip : eTypeOfMove.SkipMove;
                    }
                }
            }
            return MoveType;
        }

        private bool checkIfMustToEat(eOwner i_CurPlayer)
        {
            bool v_MustToEat = false;

            for (int i = 0; i < GameBoard.NumOfRowsAndCols; i++)
            {
                for (int j = 0; j < GameBoard.NumOfRowsAndCols; j++)
                {
                    char i_CoinSign = GameBoard.Matrix[i, j].Sign;
                    if (GameBoard.Matrix[i, j].Owner == i_CurPlayer)
                    {
                        if (i_CoinSign == 'X')
                        {
                            v_MustToEat = v_MustToEat || checkIfMustToEatDownToUp(i, j);
                        }
                        else if (i_CoinSign == 'O')
                        {
                            v_MustToEat = v_MustToEat || checkIfMustToEatUpToDown(i, j);
                        }
                        else if ((i_CoinSign == 'U') || (i_CoinSign == 'K'))
                        {
                            v_MustToEat = v_MustToEat || checkIfMustToEatDownToUp(i, j) || checkIfMustToEatUpToDown(i, j);
                        }
                    }
                }
            }
            return v_MustToEat;
        }

        private bool checkIfMustToEatUpToDown(int i_Row, int i_Col)
        {
            bool v_checkIfMustToEatRight = false;
            bool v_checkIfMustToEatLeft = false;
            eOwner CurOwner = GameBoard.Matrix[i_Row, i_Col].Owner;
            if ((i_Col < GameBoard.NumOfRowsAndCols - 2) && (i_Row < GameBoard.NumOfRowsAndCols - 2))
            {
                if ((CurOwner != GameBoard.Matrix[i_Row + 1, i_Col + 1].Owner) && (GameBoard.Matrix[i_Row + 1, i_Col + 1].Owner != eOwner.None))
                {
                    v_checkIfMustToEatRight = GameBoard.Matrix[i_Row + 2, i_Col + 2].Status == eStatus.NotExistCoin;
                }
            }
            if ((i_Col >= 2) && (i_Row < GameBoard.NumOfRowsAndCols - 2))
            {
                if ((CurOwner != GameBoard.Matrix[i_Row + 1, i_Col - 1].Owner) && (GameBoard.Matrix[i_Row + 1, i_Col - 1].Owner != eOwner.None))
                {
                    v_checkIfMustToEatLeft = GameBoard.Matrix[i_Row + 2, i_Col - 2].Status == eStatus.NotExistCoin;
                }
            }
            return v_checkIfMustToEatRight || v_checkIfMustToEatLeft;
        }

        private bool checkIfMustToEatDownToUp(int i_Row, int i_Col)
        {
            bool v_checkIfMustToEatRight = false;
            bool v_checkIfMustToEatLeft = false;
            eOwner CurOwner = GameBoard.Matrix[i_Row, i_Col].Owner;
            if ((i_Col < GameBoard.NumOfRowsAndCols - 2) && (i_Row >= 2))
            {
                if ((CurOwner != GameBoard.Matrix[i_Row - 1, i_Col + 1].Owner) && (GameBoard.Matrix[i_Row - 1, i_Col + 1].Owner != eOwner.None))
                {
                    v_checkIfMustToEatRight = GameBoard.Matrix[i_Row - 2, i_Col + 2].Status == eStatus.NotExistCoin;
                }
            }
            if ((i_Col >= 2) && (i_Row >= 2))
            {
                if ((CurOwner != GameBoard.Matrix[i_Row - 1, i_Col - 1].Owner) && (GameBoard.Matrix[i_Row - 1, i_Col - 1].Owner != eOwner.None))
                {
                    v_checkIfMustToEatLeft = GameBoard.Matrix[i_Row - 2, i_Col - 2].Status == eStatus.NotExistCoin;
                }
            }
            return v_checkIfMustToEatRight || v_checkIfMustToEatLeft;
        }

        public bool checkIfCanEat(int i_Row, int i_Col)
        {
            bool v_CanEat = false;
            char i_CoinSign = GameBoard.Matrix[i_Row, i_Col].Sign;

            if (i_CoinSign == 'X')
            {
                v_CanEat = checkIfMustToEatDownToUp(i_Row, i_Col);
            }
            else if (i_CoinSign == 'O')
            {
                v_CanEat = checkIfMustToEatUpToDown(i_Row, i_Col);
            }
            else if ((i_CoinSign == 'U') || (i_CoinSign == 'K'))
            {
                v_CanEat = checkIfMustToEatDownToUp(i_Row, i_Col) || checkIfMustToEatUpToDown(i_Row, i_Col);
            }
            return v_CanEat;
        }

        public int[] FindTargetOfEating(int[] i_Source)
        {
            int[] Target = new int[2];
            if (checkIfCanEat(i_Source[0], i_Source[1]))
            {
                char CoinSign = GameBoard.Matrix[i_Source[0], i_Source[1]].Sign;
                eOwner CurOwner = GameBoard.Matrix[i_Source[0], i_Source[1]].Owner;
                if (CoinSign != 'O')
                {
                    if ((i_Source[1] < GameBoard.NumOfRowsAndCols - 2) && (i_Source[0] >= 2))
                    {
                        if ((CurOwner != GameBoard.Matrix[i_Source[0] - 1, i_Source[1] + 1].Owner) && (GameBoard.Matrix[i_Source[0] - 1, i_Source[1] + 1].Owner != eOwner.None))
                        {
                            if (GameBoard.Matrix[i_Source[0] - 2, i_Source[1] + 2].Status == eStatus.NotExistCoin) 
                            {
                                Target[0] = i_Source[0] - 2;
                                Target[1] = i_Source[1] + 2;
                                goto finish;
                            }
                        }
                    }
                    if ((i_Source[1] >= 2) && (i_Source[0] >= 2))
                    {
                        if ((CurOwner != GameBoard.Matrix[i_Source[0] - 1, i_Source[1] - 1].Owner) && (GameBoard.Matrix[i_Source[0] - 1, i_Source[1] - 1].Owner != eOwner.None))
                        {
                            if (GameBoard.Matrix[i_Source[0] - 2, i_Source[1] - 2].Status == eStatus.NotExistCoin)
                            {
                                Target[0] = i_Source[0] - 2;
                                Target[1] = i_Source[1] - 2;
                                goto finish;
                            }
                        }
                    }
                }
                if (CoinSign != 'X')
                {
                    if ((i_Source[1] < GameBoard.NumOfRowsAndCols - 2) && (i_Source[0] < GameBoard.NumOfRowsAndCols - 2))
                    {
                        if ((CurOwner != GameBoard.Matrix[i_Source[0] + 1, i_Source[1] + 1].Owner) && (GameBoard.Matrix[i_Source[0] + 1, i_Source[1] + 1].Owner != eOwner.None))
                        {
                            if (GameBoard.Matrix[i_Source[0] + 2, i_Source[1] + 2].Status == eStatus.NotExistCoin)
                            {
                                Target[0] = i_Source[0] + 2;
                                Target[1] = i_Source[1] + 2;
                                goto finish;
                            }
                        }
                    }
                    if ((i_Source[1] >= 2) && (i_Source[0] < GameBoard.NumOfRowsAndCols - 2))
                    {
                        if ((CurOwner != GameBoard.Matrix[i_Source[0] + 1, i_Source[1] - 1].Owner) && (GameBoard.Matrix[i_Source[0] + 1, i_Source[1] - 1].Owner != eOwner.None))
                        {
                            if (GameBoard.Matrix[i_Source[0] + 2, i_Source[1] - 2].Status == eStatus.NotExistCoin)
                            {
                                Target[0] = i_Source[0] + 2;
                                Target[1] = i_Source[1] - 2;
                                goto finish;
                            }
                        }
                    }
                }
            }
            finish:
            return Target;
        }

        private void resetCell(int i_Row, int i_Col)
        {
            GameBoard.Matrix[i_Row, i_Col].Sign = ' ';
            GameBoard.Matrix[i_Row, i_Col].Owner = eOwner.None;
            GameBoard.Matrix[i_Row, i_Col].Status = eStatus.NotExistCoin;
        }

        private void updateSourceAndTargetCells(int[] i_Source, int[] i_Terget)
        {
            GameBoard.Matrix[i_Terget[0], i_Terget[1]].Status = eStatus.ExistCoin;
            GameBoard.Matrix[i_Source[0], i_Source[1]].Status = eStatus.NotExistCoin;
            GameBoard.Matrix[i_Terget[0], i_Terget[1]].Sign = GameBoard.Matrix[i_Source[0], i_Source[1]].Sign;
            GameBoard.Matrix[i_Source[0], i_Source[1]].Sign = ' ';
            GameBoard.Matrix[i_Terget[0], i_Terget[1]].Owner = GameBoard.Matrix[i_Source[0], i_Source[1]].Owner;
            GameBoard.Matrix[i_Source[0], i_Source[1]].Owner = eOwner.None;
        }

        public void TryMakeKing(int[] i_Target)
        {
            if ((i_Target[0] == 0) && (GameBoard.Matrix[i_Target[0], i_Target[1]].Sign == 'X'))
            {
                GameBoard.Matrix[i_Target[0], i_Target[1]].Sign = 'K';
            }
            else if ((i_Target[0] == GameBoard.NumOfRowsAndCols - 1) && (GameBoard.Matrix[i_Target[0], i_Target[1]].Sign == 'O'))
            {
                GameBoard.Matrix[i_Target[0], i_Target[1]].Sign = 'U';
            }
        }

        public bool CheckIfExistsMoves(Player i_Player)
        {
            bool v_ThereAreMoves = false;
            for (int i = 0; i < GameBoard.NumOfRowsAndCols; i++)
            {
                for (int j = 0; j < GameBoard.NumOfRowsAndCols; j++)
                {
                    char i_CoinSign = GameBoard.Matrix[i, j].Sign;
                    if ((i_CoinSign == i_Player.CoinSign) || i_CoinSign == i_Player.KingSign)
                    {
                        v_ThereAreMoves = v_ThereAreMoves || CheckIfCanMove(i, j);
                    }
                }
            }
            return v_ThereAreMoves;
        }

        public bool CheckIfExistRegularMove(int i_Row, int i_Col)
        {
            bool v_CanMove = false;
            Char CurSign = GameBoard.Matrix[i_Row, i_Col].Sign;
            eStatus i_NotExistCoin = eStatus.NotExistCoin;
            eStatus i_UpRightCell = (i_Row > 0 && i_Col < GameBoard.NumOfRowsAndCols - 1) ? GameBoard.Matrix[i_Row - 1, i_Col + 1].Status : eStatus.Illegal;
            eStatus i_UpLeftCell = (i_Row > 0 && i_Col > 0) ? GameBoard.Matrix[i_Row - 1, i_Col - 1].Status : eStatus.Illegal;
            eStatus i_DownRightCell = (i_Row < GameBoard.NumOfRowsAndCols - 1 && i_Col < GameBoard.NumOfRowsAndCols - 1) ? GameBoard.Matrix[i_Row + 1, i_Col + 1].Status : eStatus.Illegal;
            eStatus i_DownLeftCell = (i_Row < GameBoard.NumOfRowsAndCols - 1 && i_Col > 0) ? GameBoard.Matrix[i_Row + 1, i_Col - 1].Status : eStatus.Illegal;
            if (CurSign == 'O')
            {
                v_CanMove = i_DownLeftCell == i_NotExistCoin || i_DownRightCell == i_NotExistCoin;
            }
            if (CurSign == 'X')
            {
                v_CanMove = i_UpLeftCell == i_NotExistCoin || i_UpRightCell == i_NotExistCoin;
            }
            if ((CurSign == 'U') || (CurSign == 'K'))
            {
                v_CanMove = i_UpLeftCell == i_NotExistCoin || i_UpRightCell == i_NotExistCoin || i_DownLeftCell == i_NotExistCoin || i_DownRightCell == i_NotExistCoin;
            }
            return v_CanMove;
        }

        public bool CheckIfCanMove(int i_Row, int i_Col)
        {
            bool v_CanMove = false;
            Char CurSign = GameBoard.Matrix[i_Row, i_Col].Sign;
            if (CurSign == 'O')
            {
                v_CanMove = checkIfMustToEatUpToDown(i_Row, i_Col) || CheckIfExistRegularMove(i_Row, i_Col);
            }
            if (CurSign == 'X')
            {
                v_CanMove = checkIfMustToEatDownToUp(i_Row, i_Col) || CheckIfExistRegularMove(i_Row, i_Col);
            }
            if ((CurSign == 'U') || (CurSign == 'K'))
            {
                v_CanMove = checkIfMustToEatUpToDown(i_Row, i_Col) || checkIfMustToEatDownToUp(i_Row, i_Col) || CheckIfExistRegularMove(i_Row, i_Col);
            }
            return v_CanMove;
        }

        public eOwner Winner()
        {
            eOwner TheWinner = eOwner.None;
            if (CheckIfExistsMoves(Players[0]) && CheckIfExistsMoves(Players[1]))
            {
                TheWinner = eOwner.None;
            }
            else if (CheckIfExistsMoves(Players[0]))
            {
                TheWinner = eOwner.Player2;
            }
            else if (CheckIfExistsMoves(Players[1]))
            {
                TheWinner = eOwner.Player1;
            }
            return TheWinner;
        }

        public void UpdateScore(eOwner i_TheWinner)
        {
            if (i_TheWinner == eOwner.Player1)
            {
                this.Players[0].Score++;
            }
            else if (i_TheWinner == eOwner.Player2)
            {
                this.Players[1].Score++;
            }
        }



        //_____________________________________________________________________________________//
        public void AImove(out int[] o_Source, out int[] o_TargetStep1, out int[] o_TargetStep2, out bool o_CanDoubleSkip)
        {
            //int[,] Steps = new int[3, 2];
            int[] Source = new int[2];
            int[] TargetStep1 = new int[2];
            int[] TargetStep2 = new int[2];
            o_CanDoubleSkip = false;
            o_Source = Source;
            o_TargetStep1 = TargetStep1;
            o_TargetStep2 = TargetStep2;
            if (this.checkIfMustToEat(eOwner.Player2))
            {
                if (checkDoubleSkip(out Source, out TargetStep1, out TargetStep2))
                {
                    o_CanDoubleSkip = true;
                    o_Source = Source;
                    o_TargetStep1 = TargetStep1;
                    o_TargetStep2 = TargetStep2;
                    //Steps[0, 0] = Source[0];
                    //Steps[0, 1] = Source[1];
                    //Steps[1, 0] = TargetStep1[0];
                    //Steps[1, 1] = TargetStep1[1];
                    //Steps[2, 0] = TargetStep2[0];
                    //Steps[2, 1] = TargetStep2[1];
                }
                else if (checkIfCanEatAndNoEaten(out Source, out TargetStep1))
                {
                    o_Source = Source;
                    o_TargetStep1 = TargetStep1;
                    //Steps[0, 0] = Source[0];
                    //Steps[0, 1] = Source[1];
                    //Steps[1, 0] = TargetStep1[0];
                    //Steps[1, 1] = TargetStep1[1];
                    //Steps[2, 0] = -1;
                    //Steps[2, 1] = -1;
                }
                else if (checkIfCanEatAndEaten(out Source, out TargetStep1))
                {
                    o_Source = Source;
                    o_TargetStep1 = TargetStep1;
                    //Source = getSkipMove(out TargetStep1);
                    //Steps[0, 1] = Source[1];
                    //Steps[1, 0] = TargetStep1[0];
                    //Steps[1, 1] = TargetStep1[1];
                    //Steps[2, 0] = -1;
                    //Steps[2, 1] = -1;
                }
            }
            else
            {
                //להזיז שחקן שעומד לאיאכל
                if(trysaveSoldier(out Source, out TargetStep1))
                {
                    o_Source = Source;
                    o_TargetStep1 = TargetStep1;
                    //Steps[0, 0] = Source[0];
                    //Steps[0, 1] = Source[1];
                    //Steps[1, 0] = TargetStep1[0];
                    //Steps[1, 1] = TargetStep1[1];
                    //Steps[2, 0] = -1;
                    //Steps[2, 1] = -1;
                }
                //להזיז שחקן אקראי שלא יאכל
                //להזיז שחקן אקראי בעדיפות לשורה נמוכה
                else if (moveSoldierToSaveZone(out Source, out TargetStep1))
                {
                    o_Source = Source;
                    o_TargetStep1 = TargetStep1;
                    //Steps[0, 0] = Source[0];
                    //Steps[0, 1] = Source[1];
                    //Steps[1, 0] = TargetStep1[0];
                    //Steps[1, 1] = TargetStep1[1];
                    //Steps[2, 0] = -1;
                    //Steps[2, 1] = -1;
                }
                else if (moveRandomSoldier(out Source, out TargetStep1))
                {
                    o_Source = Source;
                    o_TargetStep1 = TargetStep1;
                    //Steps[0, 0] = Source[0];
                    //Steps[0, 1] = Source[1];
                    //Steps[1, 0] = TargetStep1[0];
                    //Steps[1, 1] = TargetStep1[1];
                    //Steps[2, 0] = -1;
                    //Steps[2, 1] = -1;
                }
            }
        }

        private bool moveSoldierToSaveZone(out int[] o_Source, out int[] o_Target)
        {
            bool v_MoveSucces = false;
            int[] Source = new int[2];
            o_Target = new int[2];
            o_Source = new int[2];
            for (int i = 0; i < this.GameBoard.NumOfRowsAndCols; ++i)
            {
                for (int j = this.GameBoard.NumOfRowsAndCols - 1; j >= 0; --j)
                {
                    Source[0] = i;
                    Source[1] = j;
                    if ((GameBoard.Matrix[i, j].Sign == 'X') || (GameBoard.Matrix[i, j].Sign == 'K'))
                    {

                        if (GameBoard.Matrix[i, j].Sign == 'K')
                        {
                            if ((i < GameBoard.NumOfRowsAndCols - 1) && (j < GameBoard.NumOfRowsAndCols - 1) && (GameBoard.Matrix[i + 1, j + 1].Owner == eOwner.None)) 
                            {
                                o_Target[0] = i + 1;
                                o_Target[1] = j + 1;
                                if (!checkIfCanBeEaten(o_Target))
                                {
                                    if (((i + 1 == GameBoard.NumOfRowsAndCols - 1) || (j + 1 == GameBoard.NumOfRowsAndCols - 1) || GameBoard.Matrix[i + 2, j + 2].Sign != 'U'))
                                    {
                                        o_Source = Source;
                                        v_MoveSucces = true;
                                        return v_MoveSucces;
                                    }
                                }
                            }
                            if ((i < GameBoard.NumOfRowsAndCols - 1) && (j > 0) && (GameBoard.Matrix[i + 1, j - 1].Owner == eOwner.None)) 
                            {
                                o_Target[0] = i + 1;
                                o_Target[1] = j - 1;
                                if (!checkIfCanBeEaten(o_Target))
                                {
                                    if (((i + 1 == GameBoard.NumOfRowsAndCols - 1) || (j - 1 == 0) || GameBoard.Matrix[i + 2, j - 2].Sign != 'U'))
                                    {
                                        o_Source = Source;
                                        v_MoveSucces = true;
                                        return v_MoveSucces;
                                    }
                                }
                            }
                        }
                        if ((i > 0) && (j < GameBoard.NumOfRowsAndCols - 1) && (GameBoard.Matrix[i - 1, j + 1].Owner == eOwner.None)) 
                        {
                            o_Target[0] = i - 1;
                            o_Target[1] = j + 1;
                            if (!checkIfCanBeEaten(o_Target))
                            {
                                if ((i - 1 == 0) || (j + 1 == GameBoard.NumOfRowsAndCols - 1) || (GameBoard.Matrix[i - 2, j + 2].Owner != eOwner.Player1))
                                {
                                    o_Source = Source;
                                    v_MoveSucces = true;
                                    return v_MoveSucces;
                                }
                            }
                        }
                        if ((i > 0) && (j > 0) && (GameBoard.Matrix[i - 1, j - 1].Owner == eOwner.None)) 
                        {
                            o_Target[0] = i - 1;
                            o_Target[1] = j - 1;
                            if (!checkIfCanBeEaten(o_Target))
                            {
                                if (((i - 1 == 0) || (j - 1 == 0) || GameBoard.Matrix[i - 2, j - 2].Owner != eOwner.Player1))
                                {
                                    o_Source = Source;
                                    v_MoveSucces = true;
                                    return v_MoveSucces;
                                }
                            }
                        }
                    }
                }
            }
            return v_MoveSucces;
        }

        private bool moveRandomSoldier(out int[] o_Source, out int[] o_Target)
        {
            bool v_MoveSucces = false;
            int[] Source = new int[2];
            o_Target = new int[2];
            o_Source = new int[2];
            for (int i = 0; i < this.GameBoard.NumOfRowsAndCols; ++i)
            {
                for (int j = 0; j < this.GameBoard.NumOfRowsAndCols; ++j)
                {
                    Source[0] = i;
                    Source[1] = j;
                    if ((GameBoard.Matrix[i, j].Sign == 'X') || (GameBoard.Matrix[i, j].Sign == 'K'))
                    {
                        if ((i != 0) && (j != GameBoard.NumOfRowsAndCols - 1) && (GameBoard.Matrix[i - 1, j + 1].Owner == eOwner.None))
                        {
                            o_Target[0] = i - 1;
                            o_Target[1] = j + 1;
                            o_Source = Source;
                            v_MoveSucces = true;
                            return v_MoveSucces;
                        }
                        if ((i != 0) && (j != 0) && (GameBoard.Matrix[i - 1, j - 1].Owner == eOwner.None))
                        {
                            o_Target[0] = i - 1;
                            o_Target[1] = j - 1;
                            o_Source = Source;
                            v_MoveSucces = true;
                            return v_MoveSucces;
                        }
                        if (GameBoard.Matrix[i, j].Sign == 'K')
                        {
                            if ((i != GameBoard.NumOfRowsAndCols - 1) && (j != GameBoard.NumOfRowsAndCols - 1) && (GameBoard.Matrix[i + 1, j + 1].Owner == eOwner.None))
                            {
                                o_Target[0] = i + 1;
                                o_Target[1] = j + 1;
                                o_Source = Source;
                                v_MoveSucces = true;
                                return v_MoveSucces;
                            }
                            if ((i != GameBoard.NumOfRowsAndCols - 1) && (j != 0) && (GameBoard.Matrix[i + 1, j - 1].Owner == eOwner.None))
                            {
                                o_Target[0] = i + 1;
                                o_Target[1] = j - 1;
                                o_Source = Source;
                                v_MoveSucces = true;
                                return v_MoveSucces;
                            }
                        }
                    }
                }
            }
            return v_MoveSucces;
        }

        private bool trysaveSoldier(out int[] o_Source, out int[] o_Target)
        {
            bool v_SaveSucces = false;
            int[] Source = new int[2];
            o_Target = new int[2];
            o_Source = new int[2];
            for (int i = 0; i < this.GameBoard.NumOfRowsAndCols; ++i)
            {
                for (int j = 0; j < this.GameBoard.NumOfRowsAndCols; ++j)
                {
                    Source[0] = i;
                    Source[1] = j;
                    if ((GameBoard.Matrix[i, j].Sign == 'X') || (GameBoard.Matrix[i, j].Sign == 'K'))
                    {
                        if (checkIfCanBeEaten(Source))
                        {
                            if (GameBoard.Matrix[i, j].Sign == 'K')
                            {
                                if ((i < GameBoard.NumOfRowsAndCols - 1) && (j < GameBoard.NumOfRowsAndCols - 1) && (GameBoard.Matrix[i + 1, j + 1].Owner == eOwner.None)) 
                                {
                                    o_Target[0] = i + 1;
                                    o_Target[1] = j + 1;
                                    if (!checkIfCanBeEaten(o_Target))
                                    {
                                        if (((i + 1 == GameBoard.NumOfRowsAndCols - 1) || (j + 1 == GameBoard.NumOfRowsAndCols - 1) || GameBoard.Matrix[i + 2, j + 2].Sign != 'U'))
                                        {
                                            o_Source = Source;
                                            v_SaveSucces = true;
                                            return v_SaveSucces;
                                        }
                                    }
                                }
                                if ((i < GameBoard.NumOfRowsAndCols - 1) && (j > 0) && (GameBoard.Matrix[i + 1, j - 1].Owner == eOwner.None)) 
                                {
                                    o_Target[0] = i + 1;
                                    o_Target[1] = j - 1;
                                    if (!checkIfCanBeEaten(o_Target))
                                    {
                                        if (((i + 1 == GameBoard.NumOfRowsAndCols - 1) || (j - 1 == 0) || GameBoard.Matrix[i + 2, j - 2].Sign != 'U'))
                                        {
                                            o_Source = Source;
                                            v_SaveSucces = true;
                                            return v_SaveSucces;
                                        }
                                    }
                                }
                            }
                            if ((i > 0) && (j < GameBoard.NumOfRowsAndCols - 1) && (GameBoard.Matrix[i - 1, j + 1].Owner == eOwner.None)) 
                            {
                                o_Target[0] = i - 1;
                                o_Target[1] = j + 1;
                                if (!checkIfCanBeEaten(o_Target))
                                {
                                    if ((i - 1 == 0) || (j + 1 == GameBoard.NumOfRowsAndCols - 1) || (GameBoard.Matrix[i - 2, j + 2].Owner != eOwner.Player1)) 
                                    {
                                        o_Source = Source;
                                        v_SaveSucces = true;
                                        return v_SaveSucces;
                                    }
                                }                              
                            }
                            if ((i > 0) && (j > 0) && (GameBoard.Matrix[i - 1, j - 1].Owner == eOwner.None)) 
                            {
                                o_Target[0] = i - 1;
                                o_Target[1] = j - 1;
                                if (!checkIfCanBeEaten(o_Target))
                                {
                                    if (((i - 1 == 0) || (j - 1 == 0) || GameBoard.Matrix[i - 2, j - 2].Owner != eOwner.Player1)) 
                                    {
                                        o_Source = Source;
                                        v_SaveSucces = true;
                                        return v_SaveSucces;
                                    }
                                }                             
                            }
                            
                        }
                    }
                }
            }
            return v_SaveSucces;
        }

        private int[] getSkipMove(out int[] o_Target)
        {
            int[] Source = new int[2];
            int[] o_EatToRight = new int[2];
            int[] o_EatToLeft = new int[2];
            bool o_CanEatToRight;
            bool o_CanEatToLeft;
            o_Target = new int[2];
            for (int i = 0; i < this.GameBoard.NumOfRowsAndCols; ++i)
            {
                for (int j = 0; j < this.GameBoard.NumOfRowsAndCols; ++j)
                {
                    Source[0] = i;
                    Source[1] = j;
                    if ((GameBoard.Matrix[i, j].Sign == 'X') || (GameBoard.Matrix[i, j].Sign == 'K')) 
                    {
                        checkIfCanEatDownToUp(Source, out o_EatToRight, out o_EatToLeft, out o_CanEatToRight, out o_CanEatToLeft);
                        if (o_CanEatToRight)
                        {
                            o_Target = o_EatToRight;
                            return Source;
                        }
                        if (o_CanEatToLeft)
                        {
                            o_Target = o_EatToLeft;
                            return Source;
                        }
                    }
                    if (GameBoard.Matrix[i, j].Sign == 'K') 
                    {
                        checkIfCanEatUpToDown(Source, out o_EatToRight, out o_EatToLeft, out o_CanEatToRight, out o_CanEatToLeft);
                        if (o_CanEatToRight)
                        {
                            o_Target = o_EatToRight;
                            return Source;
                        }
                        if (o_CanEatToLeft)
                        {
                            o_Target = o_EatToLeft;
                            return Source;
                        }
                    }
                }
            }
            return Source;
        }

        private bool checkDoubleSkip(out int[] o_Source, out int[] o_TargetStep1, out int[] o_TargetStep2)
        {
            int[] step1 = new int[2];
            int[] step2 = new int[2];
            int[] step3 = new int[2];
            bool v_ThereIsDoubleSkip = false;
            bool v_RegularNoEaten;
            bool v_KingNoEaten;
            bool RegularDoubleSkip = checkRegularDoubleSkip(out o_Source, out o_TargetStep1, out o_TargetStep2, out v_RegularNoEaten);
            bool KingDoubleSkip = checkKingDoubleSkip(out step1, out step2, out step3, out v_KingNoEaten);
            if (RegularDoubleSkip && KingDoubleSkip)
            {
                if (v_RegularNoEaten && v_KingNoEaten)
                {
                    v_ThereIsDoubleSkip = true;
                }
                else if (v_RegularNoEaten)
                {
                    v_ThereIsDoubleSkip = true;
                }
                else if(v_KingNoEaten)
                {
                    v_ThereIsDoubleSkip = true;
                }
            }
            else if (RegularDoubleSkip)
            {
                v_ThereIsDoubleSkip = true;
            }
            else if (KingDoubleSkip)
            {
                o_Source = step1;
                o_TargetStep1 = step2;
                o_TargetStep2 = step3;
                v_ThereIsDoubleSkip = true;
            }
            return v_ThereIsDoubleSkip;
        }

        private bool checkRegularDoubleSkip(out int[] o_Source, out int[] o_TargetStep1, out int[] o_TargetStep2, out bool o_NoEaten)
        {
            bool v_ThereIsDoubleSkip = false;
            bool CanEatToRight;
            bool CanEatToLeft;
            bool CanEatToRightStep2;
            bool CanEatToLeftStep2;
            o_NoEaten = false;
            o_Source = new int[2];
            o_TargetStep1 = new int[2];
            o_TargetStep2 = new int[2];
            int[] DownLeftToUpRight = new int[2];
            int[] DownRightToUpLeft = new int[2];
            int[] upLeftToDownRight = new int[2];
            int[] UpRightToDownLeft = new int[2];
            int[] RightToLeftStep2 = new int[2];
            int[] LeftToRightStep2 = new int[2];
            for (int i = 0; i < this.GameBoard.NumOfRowsAndCols; ++i)
            {
                for (int j = 0; j < this.GameBoard.NumOfRowsAndCols; ++j)
                {
                    char i_CoinSign = GameBoard.Matrix[i, j].Sign;

                    if (i_CoinSign == 'X')
                    {
                        o_Source[0] = i;
                        o_Source[1] = j;
                        if (checkIfCanEatDownToUp(o_Source, out DownLeftToUpRight, out DownRightToUpLeft, out CanEatToRight, out CanEatToLeft))  
                        {
                            if (CanEatToRight) 
                            {
                                checkIfCanEatDownToUp(DownLeftToUpRight, out LeftToRightStep2, out RightToLeftStep2, out CanEatToRightStep2, out CanEatToLeftStep2);
                                if (CanEatToRightStep2)
                                {
                                    o_TargetStep1 = DownLeftToUpRight;
                                    o_TargetStep2 = LeftToRightStep2;
                                    v_ThereIsDoubleSkip = true;
                                    if (!checkIfCanBeEaten(LeftToRightStep2))
                                    {
                                        o_NoEaten = true;
                                        goto finish;
                                    }
                                }
                                if (CanEatToLeftStep2)
                                {
                                    o_TargetStep1 = DownLeftToUpRight;
                                    o_TargetStep2 = RightToLeftStep2;
                                    v_ThereIsDoubleSkip = true;
                                    if (!checkIfCanBeEaten(RightToLeftStep2))
                                    {
                                        o_NoEaten = true;
                                        goto finish;
                                    }
                                }
                            }
                            if (CanEatToLeft)
                            {
                                checkIfCanEatDownToUp(DownRightToUpLeft, out LeftToRightStep2, out RightToLeftStep2, out CanEatToRightStep2, out CanEatToLeftStep2);
                                if (CanEatToRightStep2)
                                {
                                    o_TargetStep1 = DownRightToUpLeft;
                                    o_TargetStep2 = LeftToRightStep2;
                                    v_ThereIsDoubleSkip = true;
                                    if (!checkIfCanBeEaten(LeftToRightStep2))
                                    {
                                        o_NoEaten = true;
                                        goto finish;
                                    }
                                }
                                if (CanEatToLeftStep2)
                                {
                                    o_TargetStep1 = DownLeftToUpRight;
                                    o_TargetStep2 = RightToLeftStep2;
                                    v_ThereIsDoubleSkip = true;
                                    if (!checkIfCanBeEaten(RightToLeftStep2))
                                    {
                                        o_NoEaten = true;
                                        goto finish;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            finish:
            return v_ThereIsDoubleSkip;
        }

        private bool checkKingDoubleSkip(out int[] o_Source, out int[] o_TargetStep1, out int[] o_TargetStep2, out bool o_NoEaten)
        {
            bool v_ThereIsDoubleSkip = false;
            bool CanEatToRight;
            bool CanEatToLeft;
            bool CanEatToRightStep2;
            //bool CanEatToRightDownStep2;
            bool CanEatToLeftStep2;
            //bool CanEatToLeftDownStep2;
            o_NoEaten = false;
            o_Source = new int[2];
            o_TargetStep1 = new int[2];
            o_TargetStep2 = new int[2];
            int[] DownLeftToUpRight = new int[2];
            int[] DownRightToUpLeft = new int[2];
            int[] upLeftToDownRight = new int[2];
            int[] UpRightToDownLeft = new int[2];
            int[] RightToLeftStep2 = new int[2];
            int[] LeftToRightStep2 = new int[2];
            for (int i = 0; i < this.GameBoard.NumOfRowsAndCols; ++i)
            {
                for (int j = 0; j < this.GameBoard.NumOfRowsAndCols; ++j)
                {
                    char i_CoinSign = GameBoard.Matrix[i, j].Sign;

                    if (i_CoinSign == 'K')
                    {
                        o_Source[0] = i;
                        o_Source[1] = j;
                        if (checkIfCanEatDownToUp(o_Source, out DownLeftToUpRight, out DownRightToUpLeft, out CanEatToRight, out CanEatToLeft))
                        {
                            if (CanEatToRight)
                            {
                                checkIfCanEatDownToUp(DownLeftToUpRight, out LeftToRightStep2, out RightToLeftStep2, out CanEatToRightStep2, out CanEatToLeftStep2);
                                if (CanEatToRightStep2)
                                {
                                    o_TargetStep1 = DownLeftToUpRight;
                                    o_TargetStep2 = LeftToRightStep2;
                                    v_ThereIsDoubleSkip = true;
                                    if (!checkIfCanBeEaten(LeftToRightStep2))
                                    {
                                        o_NoEaten = true;
                                        goto finish;
                                    }
                                }
                                if (CanEatToLeftStep2)
                                {
                                    o_TargetStep1 = DownLeftToUpRight;
                                    o_TargetStep2 = RightToLeftStep2;
                                    v_ThereIsDoubleSkip = true;
                                    if (!checkIfCanBeEaten(RightToLeftStep2))
                                    {
                                        o_NoEaten = true;
                                        goto finish;
                                    }
                                }
                                checkIfCanEatUpToDown(DownLeftToUpRight, out LeftToRightStep2, out RightToLeftStep2, out CanEatToRightStep2, out CanEatToLeftStep2);
                                if (CanEatToRightStep2)
                                {
                                    o_TargetStep1 = DownLeftToUpRight;
                                    o_TargetStep2 = LeftToRightStep2;
                                    v_ThereIsDoubleSkip = true;
                                    if (!checkIfCanBeEaten(LeftToRightStep2))
                                    {
                                        o_NoEaten = true;
                                        goto finish;
                                    }
                                }
                            }
                            //
                            if (CanEatToLeft)
                            {
                                checkIfCanEatDownToUp(DownRightToUpLeft, out LeftToRightStep2, out RightToLeftStep2, out CanEatToRightStep2, out CanEatToLeftStep2);
                                if (CanEatToRightStep2)
                                {
                                    o_TargetStep1 = DownRightToUpLeft;
                                    o_TargetStep2 = LeftToRightStep2;
                                    v_ThereIsDoubleSkip = true;
                                    if (!checkIfCanBeEaten(LeftToRightStep2))
                                    {
                                        o_NoEaten = true;
                                        goto finish;
                                    }
                                }
                                if (CanEatToLeftStep2)
                                {
                                    o_TargetStep1 = DownLeftToUpRight;
                                    o_TargetStep2 = RightToLeftStep2;
                                    v_ThereIsDoubleSkip = true;
                                    if (!checkIfCanBeEaten(RightToLeftStep2))
                                    {
                                        o_NoEaten = true;
                                        goto finish;
                                    }
                                }
                                checkIfCanEatUpToDown(DownLeftToUpRight, out LeftToRightStep2, out RightToLeftStep2, out CanEatToRightStep2, out CanEatToLeftStep2);
                                if (CanEatToLeftStep2)
                                {
                                    o_TargetStep1 = DownLeftToUpRight;
                                    o_TargetStep2 = LeftToRightStep2;
                                    v_ThereIsDoubleSkip = true;
                                    if (!checkIfCanBeEaten(LeftToRightStep2))
                                    {
                                        o_NoEaten = true;
                                        goto finish;
                                    }
                                }
                            }
                        }


                        if (checkIfCanEatUpToDown(o_Source, out upLeftToDownRight, out UpRightToDownLeft, out CanEatToRight, out CanEatToLeft))
                        {
                            if (CanEatToRight)
                            {
                                checkIfCanEatUpToDown(upLeftToDownRight, out LeftToRightStep2, out RightToLeftStep2, out CanEatToRightStep2, out CanEatToLeftStep2);
                                if (CanEatToRightStep2)
                                {
                                    o_TargetStep1 = upLeftToDownRight;
                                    o_TargetStep2 = LeftToRightStep2;
                                    v_ThereIsDoubleSkip = true;
                                    if (!checkIfCanBeEaten(LeftToRightStep2))
                                    {
                                        o_NoEaten = true;
                                        goto finish;
                                    }
                                }
                                if (CanEatToLeftStep2)
                                {
                                    o_TargetStep1 = upLeftToDownRight;
                                    o_TargetStep2 = RightToLeftStep2;
                                    v_ThereIsDoubleSkip = true;
                                    if (!checkIfCanBeEaten(RightToLeftStep2))
                                    {
                                        o_NoEaten = true;
                                        goto finish;
                                    }
                                }
                                checkIfCanEatDownToUp(upLeftToDownRight, out LeftToRightStep2, out RightToLeftStep2, out CanEatToRightStep2, out CanEatToLeftStep2);
                                if (CanEatToRightStep2)
                                {
                                    o_TargetStep1 = upLeftToDownRight;
                                    o_TargetStep2 = LeftToRightStep2;
                                    v_ThereIsDoubleSkip = true;
                                    if (!checkIfCanBeEaten(LeftToRightStep2))
                                    {
                                        o_NoEaten = true;
                                        goto finish;
                                    }
                                }
                            }
                            //
                            if (CanEatToLeft)
                            {
                                checkIfCanEatUpToDown(UpRightToDownLeft, out LeftToRightStep2, out RightToLeftStep2, out CanEatToRightStep2, out CanEatToLeftStep2);
                                if (CanEatToRightStep2)
                                {
                                    o_TargetStep1 = UpRightToDownLeft;
                                    o_TargetStep2 = LeftToRightStep2;
                                    v_ThereIsDoubleSkip = true;
                                    if (!checkIfCanBeEaten(LeftToRightStep2))
                                    {
                                        o_NoEaten = true;
                                        goto finish;
                                    }
                                }
                                if (CanEatToLeftStep2)
                                {
                                    o_TargetStep1 = UpRightToDownLeft;
                                    o_TargetStep2 = RightToLeftStep2;
                                    v_ThereIsDoubleSkip = true;
                                    if (!checkIfCanBeEaten(RightToLeftStep2))
                                    {
                                        o_NoEaten = true;
                                        goto finish;
                                    }
                                }
                                checkIfCanEatDownToUp(UpRightToDownLeft, out LeftToRightStep2, out RightToLeftStep2, out CanEatToRightStep2, out CanEatToLeftStep2);
                                if (CanEatToLeftStep2)
                                {
                                    o_TargetStep1 = UpRightToDownLeft;
                                    o_TargetStep2 = RightToLeftStep2;
                                    v_ThereIsDoubleSkip = true;
                                    if (!checkIfCanBeEaten(RightToLeftStep2))
                                    {
                                        o_NoEaten = true;
                                        goto finish;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            finish:
            return v_ThereIsDoubleSkip;
        }

        //private bool checkTripleSkip(out int[] o_Source, out int[] o_TargetStep1, out int[] o_TargetStep2)
        //{
        //    o_Source = new int[2];
        //    o_TargetStep1 = new int[2];
        //    o_TargetStep2 = new int[2];
        //    return true;
        //}

        private bool checkIfCanEatDownToUp(int[] i_Source, out int[] o_EatToRight, out int[] o_EatToLeft, out bool o_CanEatToRight, out bool o_CanEatToLeft)
        {
            o_EatToRight = new int[2];
            o_EatToLeft = new int[2];
            o_CanEatToRight = false;
            o_CanEatToLeft = false;
            //eOwner CurOwner = GameBoard.Matrix[i_Source[0], i_Source[1]].Owner;
            if ((i_Source[1] < GameBoard.NumOfRowsAndCols - 2) && (i_Source[0] >= 2))
            {
                if (GameBoard.Matrix[i_Source[0] - 1, i_Source[1] + 1].Owner == eOwner.Player1)
                {
                    if (GameBoard.Matrix[i_Source[0] - 2, i_Source[1] + 2].Status == eStatus.NotExistCoin)
                    {
                        o_EatToRight[0] = i_Source[0] - 2;
                        o_EatToRight[1] = i_Source[1] + 2;
                        o_CanEatToRight = true;
                    }
                }
            }
            if ((i_Source[1] >= 2) && (i_Source[0] >= 2))
            {
                if (GameBoard.Matrix[i_Source[0] - 1, i_Source[1] - 1].Owner == eOwner.Player1)
                {
                    if (GameBoard.Matrix[i_Source[0] - 2, i_Source[1] - 2].Status == eStatus.NotExistCoin)
                    {
                        o_EatToLeft[0] = i_Source[0] - 2;
                        o_EatToLeft[1] = i_Source[1] - 2;
                        o_CanEatToLeft = true;
                    }
                }
            }
            return o_CanEatToRight || o_CanEatToLeft;
        }

        private bool checkIfCanEatUpToDown(int[] i_Source, out int[] o_EatToRight, out int[] o_EatToLeft, out bool o_CanEatToRight, out bool o_CanEatToLeft)
        {
            o_EatToRight = new int[2];
            o_EatToLeft = new int[2];
            o_CanEatToRight = false;
            o_CanEatToLeft = false;
            //eOwner CurOwner = GameBoard.Matrix[i_Source[0], i_Source[1]].Owner;
            if ((i_Source[1] < GameBoard.NumOfRowsAndCols - 2) && (i_Source[0] < GameBoard.NumOfRowsAndCols - 2))
            {
                if (GameBoard.Matrix[i_Source[0] + 1, i_Source[1] + 1].Owner == eOwner.Player1)
                {
                    if (GameBoard.Matrix[i_Source[0] + 2, i_Source[1] + 2].Status == eStatus.NotExistCoin)
                    {
                        o_EatToRight[0] = i_Source[0] + 2;
                        o_EatToRight[1] = i_Source[1] + 2;
                        o_CanEatToRight = true;
                    }
                }
            }
            if ((i_Source[1] >= 2) && (i_Source[0] < GameBoard.NumOfRowsAndCols - 2))
            {
                if (GameBoard.Matrix[i_Source[0] + 1, i_Source[1] - 1].Owner == eOwner.Player1)
                {
                    if(GameBoard.Matrix[i_Source[0] + 2, i_Source[1] - 2].Status == eStatus.NotExistCoin)
                    {
                        o_EatToLeft[0] = i_Source[0] + 2;
                        o_EatToLeft[1] = i_Source[1] - 2;
                        o_CanEatToLeft = true;
                    }
                }
            }
            return o_CanEatToLeft || o_CanEatToRight;
        }

        private bool checkIfCanBeEaten(int[] i_Location)
        {
            bool v_CanBeEaten = false;
            if ((i_Location[0] > 0) && (i_Location[0] < this.GameBoard.NumOfRowsAndCols - 1))
            {
                if ((i_Location[1] > 0) && (i_Location[1] < this.GameBoard.NumOfRowsAndCols - 1))
                {
                    if ((this.GameBoard.Matrix[i_Location[0] - 1, i_Location[1] - 1].Owner == eOwner.Player1) && (this.GameBoard.Matrix[i_Location[0] + 1, i_Location[1] + 1].Owner == eOwner.None))
                    {
                        v_CanBeEaten = true;
                    }
                    else if ((this.GameBoard.Matrix[i_Location[0] - 1, i_Location[1] + 1].Owner == eOwner.Player1) && (this.GameBoard.Matrix[i_Location[0] + 1, i_Location[1] - 1].Owner == eOwner.None))
                    {
                        v_CanBeEaten = true;
                    }
                    else if ((this.GameBoard.Matrix[i_Location[0] + 1, i_Location[1] - 1].Sign == 'U') && (this.GameBoard.Matrix[i_Location[0] - 1, i_Location[1] + 1].Owner == eOwner.None))
                    {
                        v_CanBeEaten = true;
                    }
                    else if ((this.GameBoard.Matrix[i_Location[0] + 1, i_Location[1] + 1].Sign == 'U') && (this.GameBoard.Matrix[i_Location[0] - 1, i_Location[1] - 1].Owner == eOwner.None))
                    {
                        v_CanBeEaten = true;
                    }
                }
            }
            return v_CanBeEaten;
        }

        private bool checkIfCanEatAndNoEaten(out int[] o_Source, out int[] o_Target)
        {
            bool v_CanEatAndNoEaten = false;
            int[] Source = new int[2];
            int[] Target = new int[2];
            for (int i = 0; i < GameBoard.NumOfRowsAndCols; i++)
            {
                for (int j = 0; j < GameBoard.NumOfRowsAndCols; j++)
                {
                    Source[0] = i;
                    Source[1] = j;
                    if ((GameBoard.Matrix[i, j].Sign == 'X') || (GameBoard.Matrix[i, j].Sign == 'K')) 
                    {
                        v_CanEatAndNoEaten = checkIfCanEatAndNoEatenDownToUp(Source, out Target);
                        if (v_CanEatAndNoEaten)
                        {
                            goto finish;
                        }
                    }
                    else if (GameBoard.Matrix[i, j].Sign == 'K')
                    {
                        v_CanEatAndNoEaten = checkIfCanEatAndNoEatenUpToDown(Source, out Target);
                        if (v_CanEatAndNoEaten)
                        {
                            goto finish;
                        }
                    }   
                }
            }
            finish:
            o_Source = Source;
            o_Target = Target;
            return v_CanEatAndNoEaten;
        }

        private bool checkIfCanEatAndEaten(out int[] o_Source, out int[] o_Target)
        {
            bool v_CanEatAndEaten = false;
            int[] Source = new int[2];
            int[] Target = new int[2];
            for (int i = 0; i < GameBoard.NumOfRowsAndCols; i++)
            {
                for (int j = 0; j < GameBoard.NumOfRowsAndCols; j++)
                {
                    Source[0] = i;
                    Source[1] = j;
                    if ((GameBoard.Matrix[i, j].Sign == 'X') || (GameBoard.Matrix[i, j].Sign == 'K'))
                    {
                        if (checkIfCanEat(i, j))
                        {
                            Target = FindTargetOfEating(Source);
                            v_CanEatAndEaten = true;
                            goto finish;
                        }
                    }
                }
            }
        finish:
            o_Source = Source;
            o_Target = Target;
            return v_CanEatAndEaten;
        }

        private bool checkIfCanEatAndNoEatenUpToDown(int[] i_Source, out int[] o_Target)
        {
            bool v_CanEatAndNoEaten = false;
            int[] Target = new int[2];
            if ((i_Source[1] < GameBoard.NumOfRowsAndCols - 2) && (i_Source[0] < GameBoard.NumOfRowsAndCols - 2))
            {
                if (GameBoard.Matrix[i_Source[0] + 1, i_Source[1] + 1].Owner == eOwner.Player1)
                {
                    if (GameBoard.Matrix[i_Source[0] + 2, i_Source[1] + 2].Status == eStatus.NotExistCoin)
                    {
                        Target[0] = i_Source[0] + 2;
                        Target[1] = i_Source[1] + 2;
                        if (!checkIfCanBeEaten(Target))
                        {
                            v_CanEatAndNoEaten = true;
                        }
                    }
                }
            }
            if ((i_Source[1] >= 2) && (i_Source[0] < GameBoard.NumOfRowsAndCols - 2))
            {
                if (GameBoard.Matrix[i_Source[0] + 1, i_Source[1] - 1].Owner == eOwner.Player1)
                {
                    if (GameBoard.Matrix[i_Source[0] + 2, i_Source[1] - 2].Status == eStatus.NotExistCoin)
                    {
                        Target[0] = i_Source[0] + 2;
                        Target[1] = i_Source[1] - 2;
                        if (!checkIfCanBeEaten(Target))
                        {
                            v_CanEatAndNoEaten = true;
                        }
                    }
                }
            }
            o_Target = Target;
            return v_CanEatAndNoEaten;
        }

        private bool checkIfCanEatAndNoEatenDownToUp(int[] i_Source, out int[] o_Target)
        {
            bool v_CanEatAndNoEaten = false;
            int[] Target = new int[2];
            if ((i_Source[1] < GameBoard.NumOfRowsAndCols - 2) && (i_Source[0] >= 2))
            {
                if (GameBoard.Matrix[i_Source[0] - 1, i_Source[1] + 1].Owner == eOwner.Player1)
                {
                    if(GameBoard.Matrix[i_Source[0] - 2, i_Source[1] + 2].Status == eStatus.NotExistCoin)
                    {
                        Target[0] = i_Source[0] - 2;
                        Target[1] = i_Source[1] + 2;
                        if (!checkIfCanBeEaten(Target))
                        {
                            v_CanEatAndNoEaten = true;
                        }
                    }
                }
            }
            if ((i_Source[1] >= 2) && (i_Source[0] >= 2))
            {
                if (GameBoard.Matrix[i_Source[0] - 1, i_Source[1] - 1].Owner == eOwner.Player1)
                {
                    if (GameBoard.Matrix[i_Source[0] - 2, i_Source[1] - 2].Status == eStatus.NotExistCoin)
                    {
                        Target[0] = i_Source[0] - 2;
                        Target[1] = i_Source[1] - 2;
                        if (!checkIfCanBeEaten(Target))
                        {
                            v_CanEatAndNoEaten = true;
                        }
                    }
                }
            }
            o_Target = Target;
            return v_CanEatAndNoEaten;
        }
    }
}
