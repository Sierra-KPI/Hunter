using UnityEngine;

namespace Hunter.Model.Behaviours
{
    public class Flee : DesiredVelocityGiver
    {
        private Transform dangerObject;

        public override Vector2 GetDesiredVelocity()
        {
            return -(dangerObject.position - transform.position).normalized;
        }
    }
}
