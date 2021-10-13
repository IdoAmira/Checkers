using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Checkers_IdoAmira
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormSettings GameProperties = new FormSettings();
            Application.Run(GameProperties);
            if (GameProperties.DialogResult == DialogResult.OK)
            {
                FormGame FormGame = new FormGame(GameProperties.GameProgres);
                Application.Run(FormGame);

                int NumOfPlayers = GetNumOfPlayers(GameProperties);
                if (NumOfPlayers == 2)
                {
                    
                    int PlayerTurn = 0;
                    eUserError Error;
                    //int[] a = FormGame.getMove(PlayerTurn, out Error);
                }
                else
                {
                    //playGameAgainstTheComputer(Game);
                }
            }
        }

        public static int GetNumOfPlayers(FormSettings i_GameProperties)
        {
            int NumOfPlayers;
            if (i_GameProperties.AgainstComputerChoosen)
            {
                NumOfPlayers = 1;
            }
            else
            {
                NumOfPlayers = 2;
            }
            return NumOfPlayers;
        }

        //private static void playGameWithTwoPlayers(GameProgress i_GameProgress, FormGame i_FormGame)
        //{
        //    int PlayerTurn = 0;
        //    eUserError Error;
        //    eOwner TheWinner;
        //    string move = InsertMove(i_GameProgress, PlayerTurn % 2, out Error);
        //    while ((Error != eUserError.Quit) && (i_GameProgress.CheckIfExistsMoves(i_GameProgress.Players[PlayerTurn % 2])))
        //    {
        //        eTypeOfMove i_MoveType = i_GameProgress.Domove(move);
        //        while ((i_MoveType == eTypeOfMove.IllegalMove) || (i_MoveType == eTypeOfMove.MustToEat))
        //        {
        //            printMoveTypeError(i_MoveType);
        //            move = InsertMove(i_Game, PlayerTurn % 2, out Error);
        //            i_MoveType = i_Game.Domove(move);
        //        }
        //        i_Game.TryMakeKing(i_Game.StringToMatrixLocation(move, 3, 4));
        //        PrintBoard(i_Game.GameBoard);
        //        Console.WriteLine("{0}'s move was ({1}):  {2}", i_Game.Players[PlayerTurn % 2].Name, i_Game.Players[PlayerTurn % 2].CoinSign, move);
        //        if (i_MoveType == eTypeOfMove.CanMoreSkip)
        //        {
        //            printMoveTypeError(i_MoveType);
        //            move = InsertAnotherSkip(i_Game, move, PlayerTurn % 2, out Error);
        //        }
        //        else
        //        {
        //            PlayerTurn++;
        //            move = InsertMove(i_Game, PlayerTurn % 2, out Error);
        //        }
        //    }
        //    if (Error == eUserError.Quit)
        //    {
        //        PlayerTurn++;
        //        TheWinner = (eOwner)((PlayerTurn % 2) + 1);
        //    }
        //    else
        //    {
        //        TheWinner = i_Game.Winner();
        //    }
        //    i_Game.UpdateScore(TheWinner);
        //    printWinnerAndScore(i_Game, TheWinner);
        //}

        //public static string InsertMove(GameProgress i_Game, int i_PlayerTurn, out eUserError o_Error)
        //{

        //    Console.Write("{0}'s Turn({1}):  ", i_Game.Players[i_PlayerTurn].Name, i_Game.Players[i_PlayerTurn].CoinSign);
        //    string i_PlayerMove = Console.ReadLine();
        //    // eUserError o_Error;
        //    while (!i_Game.ChecMoveIsValid(i_PlayerMove, i_PlayerTurn, out o_Error))
        //    {
        //        PrintUserError(o_Error);
        //        i_PlayerMove = Console.ReadLine();
        //    }
        //    return i_PlayerMove;
        //}
























        ////public static void StartGame()
        ////{
        ////    string FirstPlayerName = InsertPlayerName();
        ////    int BoardSize = InsertBoardSize();
        ////    int NumOfPlayers = InsertNumOfPlayers();
        ////    GameProgress Game;
        ////    if (NumOfPlayers == 2)
        ////    {
        ////        string SecondPlayerName = InsertPlayerName();
        ////        Game = new GameProgress(BoardSize, FirstPlayerName, SecondPlayerName);
        ////        playGameWithTwoPlayers(Game);
        ////    }
        ////    else
        ////    {
        ////        Game = new GameProgress(BoardSize, FirstPlayerName);
        ////        playGameAgainstTheComputer(Game);
        ////    }
        ////}

        //private static void playGameAgainstTheComputer(GameProgress i_Game)
        //{
        //    eUserError o_Error;
        //    string i_move = InsertMove(i_Game, 0, out o_Error);
        //}

        ////private static void playGameWithTwoPlayers(GameProgress i_Game)
        ////{
        ////    PrintBoard(i_Game.GameBoard);
        ////    int PlayerTurn = 0;
        ////    eUserError Error;
        ////    eOwner TheWinner;
        ////    string move = InsertMove(i_Game, PlayerTurn % 2, out Error);
        ////    while ((Error != eUserError.Quit) && (i_Game.CheckIfExistsMoves(i_Game.Players[PlayerTurn % 2])))
        ////    {
        ////        eTypeOfMove i_MoveType = i_Game.Domove(move);
        ////        while ((i_MoveType == eTypeOfMove.IllegalMove) || (i_MoveType == eTypeOfMove.MustToEat))
        ////        {
        ////            printMoveTypeError(i_MoveType);
        ////            move = InsertMove(i_Game, PlayerTurn % 2, out Error);
        ////            i_MoveType = i_Game.Domove(move);
        ////        }
        ////        i_Game.TryMakeKing(i_Game.StringToMatrixLocation(move, 3, 4));
        ////        PrintBoard(i_Game.GameBoard);
        ////        Console.WriteLine("{0}'s move was ({1}):  {2}", i_Game.Players[PlayerTurn % 2].Name, i_Game.Players[PlayerTurn % 2].CoinSign, move);
        ////        if (i_MoveType == eTypeOfMove.CanMoreSkip)
        ////        {
        ////            printMoveTypeError(i_MoveType);
        ////            move = InsertAnotherSkip(i_Game, move, PlayerTurn % 2, out Error);
        ////        }
        ////        else
        ////        {
        ////            PlayerTurn++;
        ////            move = InsertMove(i_Game, PlayerTurn % 2, out Error);
        ////        }
        ////    }
        ////    if (Error == eUserError.Quit)
        ////    {
        ////        PlayerTurn++;
        ////        TheWinner = (eOwner)((PlayerTurn % 2) + 1);
        ////    }
        ////    else
        ////    {
        ////        TheWinner = i_Game.Winner();
        ////    }
        ////    i_Game.UpdateScore(TheWinner);
        ////    printWinnerAndScore(i_Game, TheWinner);
        ////}

        //private static void printMoveTypeError(eTypeOfMove i_MoveError)
        //{
        //    int i_ErrorIndex = (int)i_MoveError;
        //    switch (i_ErrorIndex)
        //    {
        //        case 3:
        //            Console.WriteLine("Pay attention! You must eat again:  ");
        //            break;
        //        case 4:
        //            Console.WriteLine("Pay attention! You must eat your opponent, please try again:  ");
        //            break;
        //        case 5:
        //            Console.WriteLine("illegal move, please try again:  ");
        //            break;

        //    }
        //}

        //public static string InsertPlayerName()
        //{
        //    Console.WriteLine("Please enter your name:");
        //    string i_PlayerName = Console.ReadLine();
        //    while (!GameProgress.CheckName(i_PlayerName))
        //    {
        //        Console.WriteLine("The name must be shorter than 20 and without spaces, please try again:");
        //        i_PlayerName = Console.ReadLine();
        //    }
        //    return i_PlayerName;
        //}

        ////public static int GetBoardSize(FormProperties i_GameProperties)
        ////{
        ////    int BoardSize = 0;
        ////    if (i_GameProperties.Board10Choosen)
        ////    {
        ////        BoardSize = 10;
        ////    }
        ////    else if (i_GameProperties.Board8Choosen)
        ////    {
        ////        BoardSize = 8;
        ////    }
        ////    else if (i_GameProperties.Board6Choosen)
        ////    {
        ////        BoardSize = 6;
        ////    }
        ////    return BoardSize;
        ////}

        //public static int GetNumOfPlayers(FormProperties i_GameProperties)
        //{
        //    int NumOfPlayers;
        //    if (i_GameProperties.AgainstComputerChoosen)
        //    {
        //        NumOfPlayers = 1;
        //    }
        //    else
        //    {
        //        NumOfPlayers = 2;
        //    }
        //    return NumOfPlayers;
        //}

        ////public static string InsertMove(GameProgress i_Game, int i_PlayerTurn, out eUserError o_Error)
        ////{

        ////    Console.Write("{0}'s Turn({1}):  ", i_Game.Players[i_PlayerTurn].Name, i_Game.Players[i_PlayerTurn].CoinSign);
        ////    string i_PlayerMove = Console.ReadLine();
        ////    // eUserError o_Error;
        ////    while (!i_Game.ChecMoveIsValid(i_PlayerMove, i_PlayerTurn, out o_Error))
        ////    {
        ////        PrintUserError(o_Error);
        ////        i_PlayerMove = Console.ReadLine();
        ////    }
        ////    return i_PlayerMove;
        ////}

        //public static string InsertAnotherSkip(GameProgress i_Game, string i_OldMove, int i_PlayerTurn, out eUserError o_Error)
        //{
        //    bool v_InvalidSource = true;
        //    Console.Write("{0}'s Turn({1}):  ", i_Game.Players[i_PlayerTurn].Name, i_Game.Players[i_PlayerTurn].CoinSign);
        //    string i_PlayerMove = Console.ReadLine();
        //    if (i_PlayerMove.Length == 5)
        //    {
        //        v_InvalidSource = (i_PlayerMove[0] != i_OldMove[3]) || (i_PlayerMove[1] != i_OldMove[4]);
        //    }
        //    while ((!i_Game.ChecMoveIsValid(i_PlayerMove, i_PlayerTurn, out o_Error) || v_InvalidSource) && ((o_Error != eUserError.Quit)))
        //    {
        //        PrintUserError(o_Error);
        //        i_PlayerMove = Console.ReadLine();
        //        if (i_PlayerMove.Length == 5)
        //        {
        //            v_InvalidSource = (i_PlayerMove[0] != i_OldMove[3]) || (i_PlayerMove[1] != i_OldMove[4]);
        //        }
        //        else
        //        {
        //            v_InvalidSource = true;
        //        }
        //        if ((v_InvalidSource) && (o_Error != eUserError.Quit))
        //        {
        //            Console.WriteLine("Pay attention! You must eat again with the coin that made eating :  ");
        //        }
        //    }
        //    return i_PlayerMove;
        //}

        //private static void PrintUserError(eUserError i_Error)
        //{
        //    int i_ErrorIndex = (int)i_Error;
        //    switch (i_ErrorIndex)
        //    {
        //        case 2:
        //            Console.WriteLine("The input length must be five. ");
        //            break;
        //        case 3:
        //            Console.WriteLine("The format should be like this: COLrow>COLrow. ");
        //            break;
        //        case 4:
        //            Console.WriteLine("The cell is not in the range of the board.  ");
        //            break;
        //        case 5:
        //            Console.WriteLine("There is no coin in the source cell.  ");
        //            break;
        //        case 6:
        //            Console.WriteLine("The coin you want to move is not yours.  ");
        //            break;
        //        case 7:
        //            Console.WriteLine("You're trying to get to an illegal cell.  ");
        //            break;
        //    }
        //    Console.WriteLine("please try again:");
        //}

        ////public static void PrintBoard(Board i_BoardToPrint)
        ////{
        ////    Screen.Clear();
        ////    String i_NewLine = Environment.NewLine;
        ////    char i_ColLetter = 'A';
        ////    int i = 0;
        ////    char i_RowLetter = 'a';
        ////    StringBuilder i_Line = new StringBuilder();
        ////    i_Line.Insert(0, ' ');
        ////    i_Line.Insert(1, "=", (4 * i_BoardToPrint.NumOfRowsAndCols) + 1);
        ////    string i_LineSeparator = i_Line.ToString();
        ////    for (int j = 0; j < i_BoardToPrint.NumOfRowsAndCols; ++j)
        ////    {
        ////        Console.Write("   {0}", i_ColLetter++);
        ////    }

        ////    foreach (Cell c in i_BoardToPrint.Matrix)
        ////    {
        ////        if (i % i_BoardToPrint.NumOfRowsAndCols == 0)
        ////        {
        ////            Console.WriteLine();
        ////            Console.WriteLine(i_LineSeparator);
        ////            Console.Write("{0}|", i_RowLetter++);
        ////        }

        ////        if (c.Status == 0)
        ////        {
        ////            Console.Write(" {0} |", c.Sign);
        ////        }
        ////        else
        ////        {
        ////            Console.Write("   |");
        ////        }

        ////        ++i;
        ////    }

        ////    Console.WriteLine();
        ////    Console.WriteLine(i_LineSeparator);
        ////}

        //private static void printWinnerAndScore(GameProgress i_Game, eOwner i_Winner)
        //{
        //    int PlayerNumber = (int)i_Winner - 1;
        //    if (i_Winner == eOwner.None)
        //    {
        //        Console.WriteLine("Both players have no legal moves");
        //        Console.WriteLine("Equality!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("The winner is: {0}", i_Game.Players[PlayerNumber].Name);
        //    }
        //    Console.WriteLine("{0}  {1} - {2}   {3}", i_Game.Players[0].Name, i_Game.Players[0].Score, i_Game.Players[1].Score, i_Game.Players[1].Name);
        //}
    }
}
