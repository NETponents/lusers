using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lusers_game
{
    public abstract class Task
    {
        public bool isComplete;

        public Task(string taskTitle, string taskDetails)
        {
            isComplete = false;
        }

        public abstract void checkForCompletion(RoomScreen rs);
        protected abstract void onCompletion(RoomScreen rs);
    }
}
