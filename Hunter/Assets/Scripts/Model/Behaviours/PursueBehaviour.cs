using System;
using UnityEngine;
using Hunter.Model.Entities;

namespace Hunter.Model.Behaviours
{
    public class PursueBehaviour : DesiredVelocityGiver
    {
        private Transform prey;
        private float arriveRadius = 5;

        public override Vector2 GetDesiredVelocity()
        {
            var distance = (prey.position - transform.position);
            float n = 1;
            if(distance.magnitude < arriveRadius)
            {
                n = distance.magnitude / arriveRadius;
            }
            return distance.normalized * n;
        }
    }
}
