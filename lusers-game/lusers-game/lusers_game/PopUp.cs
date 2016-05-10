using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lusers_game
{
    public class PopUp
    {
        private string _message;

        public PopUp(string message)
        {
            _message = message;
        }

        public string getMessage()
        {
            return _message;
        }
    }
}
