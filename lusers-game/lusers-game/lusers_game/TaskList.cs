using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lusers_game
{
    public static class TaskList
    {
        public static List<Task> tasks = new List<Task>();
        public static List<Task> tasksToAdd = new List<Task>();

        public static void Update()
        {
            foreach(Task t in tasksToAdd)
            {
                tasks.Add(t);
            }
            tasksToAdd.RemoveRange(0, tasksToAdd.Count);
        }
    }
}
