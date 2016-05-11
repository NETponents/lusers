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

            public override void checkForCompletion()
            {
                bool isCompleted = false;
                foreach(IGameObject i in WorldObjectHolder.objects)
                {
                    if (i.GetType() == typeof(Desk))
                    {
                        isCompleted = true;
                    }
                }
                if(isCompleted)
                {
                    onCompletion();
                    isComplete = true;
                }
            }

            protected override void onCompletion()
            {
                MessageService.popUpEnqueue("CEO: Fine ... That will have to do.");
                MessageService.popUpEnqueue("CEO: Now I need a computer so I can browse Reddit.");
                MessageService.popUpEnqueue("CEO: I mean ... uh ... get on it!");
            }
        }
    }
}
