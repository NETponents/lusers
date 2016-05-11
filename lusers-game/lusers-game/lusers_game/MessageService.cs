using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lusers_game
{
    public static class MessageService
    {
        private const int POPUPCHARLIMIT = 68;
        public static Queue<PopUp> popUpQueue = new Queue<PopUp>();

        public static void popUpEnqueue(string message)
        {
            if(message.Length < POPUPCHARLIMIT)
            {
                popUpQueue.Enqueue(new PopUp(message));
            }
            else
            {
                string trimmedMessage = message.Substring(0, POPUPCHARLIMIT - 4) + " ...";
                popUpQueue.Enqueue(new PopUp(trimmedMessage));
                popUpEnqueue("... " + message.Remove(0, POPUPCHARLIMIT - 4));
            }
        }
    }
}
