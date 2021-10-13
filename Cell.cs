using System;
using System.Collections.Generic;
using System.Text;

namespace Checkers_IdoAmira
{
    public struct Cell
    {
        private char m_Sign; // X or O
        private eStatus m_Status;
        private eOwner m_Owner;

        public Cell(char i_Sign)
        {
            this.m_Sign = i_Sign;
            this.m_Status = eStatus.Illegal;
            this.m_Owner = eOwner.None;
            if (i_Sign == 'O')
            {
                m_Owner = eOwner.Player1;
            }
            else if (i_Sign == 'X')
            {
                m_Owner = eOwner.Player2;
            }
        }

        public char Sign
        {
            get
            {
                return this.m_Sign;
            }

            set
            {
                this.m_Sign = value;
            }
        }

        public eOwner Owner
        {
            get
            {
                return this.m_Owner;
            }

            set
            {
                this.m_Owner = value;
            }
        }

        public eStatus Status
        {
            get
            {
                return this.m_Status;
            }

            set
            {
                this.m_Status = value;
            }
        }
    }
}
