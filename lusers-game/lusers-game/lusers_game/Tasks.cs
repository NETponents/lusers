using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lusers_game
{
    namespace Tasks
    {
        public class T1 : Task
        {
            public T1() : base("Build (1) desk.", "The CEO needs a desk. Get on it!")
            {

            }

            public override void checkForCompletion(RoomScreen rs)
            {
                bool isCompleted = false;
                foreach(IGameObject i in rs.gameObjects)
                {
                    if (i.GetType() == typeof(Desk))
                    {
                        isCompleted = true;
                        break;
                    }
                }
                if(isCompleted)
                {
                    onCompletion(rs);
                    isComplete = true;
                }
            }

            protected override void onCompletion(RoomScreen rs)
            {
                MessageService.popUpEnqueue("CEO: Fine ... That will have to do.");
                MessageService.popUpEnqueue("CEO: Now let's build a wall and make IT pay for it!");
                MessageService.popUpEnqueue("CEO: I mean ... uh ... get on it!");
                TaskList.tasksToAdd.Add(new T2());
            }
        }
        public class T2 : Task
        {
            public T2() : base("Build (5) walls", "The CEO needs his own office. Start building!")
            {

            }
            public override void checkForCompletion(RoomScreen rs)
            {
                int wallCount = 0;
                foreach (IGameObject i in rs.gameObjects)
                {
                    if (i.GetType() == typeof(Wall))
                    {
                        wallCount++;
                    }
                }
                if (wallCount >= 5)
                {
                    onCompletion(rs);
                    isComplete = true;
                }
            }
            protected override void onCompletion(RoomScreen rs)
            {
                MessageService.popUpEnqueue("CEO: Nice.");
                MessageService.popUpEnqueue("CEO: I can finally work in peace.");
            }
        }
    }
}
