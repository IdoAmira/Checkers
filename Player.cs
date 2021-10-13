using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers_IdoAmira
{
    public class Player
    {
        private string m_PlayerName;
        private int m_Score;
        private eTypeOfPlayer m_TypeOfPlayer;  // 0=player2, 1=computer
                                               // private int m_NumOfCoins;
                                               // private int m_NumOfSkipPlayers;
        private char m_CoinSign;
        private char m_KingSign;
        //private Board m_AIMemoryBoard;

        public Player(string i_Name, eTypeOfPlayer i_TypeOfPlayer, char i_CoinSign)
        {
            this.m_TypeOfPlayer = i_TypeOfPlayer;
            this.m_PlayerName = i_Name;
            this.m_Score = 0;
            // this.m_NumOfCoins = i_NumOfCoins;
            // this.m_NumOfSkipPlayers = 0;
            this.m_CoinSign = i_CoinSign;
            if (m_CoinSign == 'O')
            {
                this.m_KingSign = 'U';
            }
            else if (m_CoinSign == 'X')
            {
                this.m_KingSign = 'K';
            }
            //this.m_AIMemoryBoard = null;
        }

        public Player(Player i_Player)
        {
            this.m_PlayerName = i_Player.Name;
            this.m_Score = i_Player.Score;
            this.m_TypeOfPlayer = i_Player.TypeOfPlayer;
            this.m_CoinSign = i_Player.CoinSign;
            this.m_KingSign = i_Player.KingSign;
        }

    

        //public Board MemoryBoard
        //{
        //    get
        //    {
        //        return this.m_AIMemoryBoard;
        //    }

        //    set
        //    {
        //        this.m_AIMemoryBoard = value;
        //    }
        //}

        public eTypeOfPlayer TypeOfPlayer
        {
            get
            {
                return this.m_TypeOfPlayer;
            }

            set
            {
                m_TypeOfPlayer = value;
            }
        }

        public int Score
        {
            get
            {
                return this.m_Score;
            }

            set
            {
                this.m_Score = value;
            }
        }

        public char CoinSign
        {
            get
            {
                return this.m_CoinSign;
            }
        }

        public char KingSign
        {
            get
            {
                return this.m_KingSign;
            }
        }


        public string Name
        {
            get
            {
                return this.m_PlayerName;
            }

            set
            {
                this.m_PlayerName = value;
            }
        }
    }
}
