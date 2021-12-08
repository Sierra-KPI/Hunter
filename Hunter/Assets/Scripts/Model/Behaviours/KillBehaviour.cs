using System;
using Hunter.Model.HunterGame;

namespace Hunter.Model.Behaviours
{
    if(areColliding == true)
    {
        if(FirstEntity.Type == Wolf)
        {
            SecondEntity.Die();
            Wolf.lifeTimer.Enabled = true;
        }
    }
}
