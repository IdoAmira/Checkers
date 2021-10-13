using System.Windows.Forms;

namespace Checkers_IdoAmira
{
    public class Location
    {
        public Location(int i_LocationX, int i_LocationY)
        {
            this.m_LocationX = i_LocationX;
            this.m_LocationY = i_LocationY;
        }

        //private Button m_Button;
        private int m_LocationX;
        private int m_LocationY;

        public int X
        {
            get { return this.m_LocationX; }
            set { this.m_LocationX = value; }
        }

        public int Y
        {
            get { return this.m_LocationY; }
            set { this.m_LocationY = value; }
        }

        //public Button Button
        //{
        //    get { return this.m_Button; }
        //}
    }
}
