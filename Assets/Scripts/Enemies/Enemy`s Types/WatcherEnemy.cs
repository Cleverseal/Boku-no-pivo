using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BokuNoPivo
{
    public abstract class WatcherEnemy : Enemy
    {
        protected void lookOutArea()
        {
            checkAtach();
            if (atachedOnPlayer)
            {
                analyzePlayerTransform();
                atackPlayer();
            }
            fliping();
        }

    }
}
