using UnityEngine;
using Hunter.Model.Entitiies;

namespace Hunter.Model.Behaviours
{

    public abstract class DesiredVelocityGiver : MonoBehaviour
    {
        [SerializeField, Range(0, 3)] //needs to be asked about
        private float weight = 1f;

        public float Weight => weight;

        protected Entity entity;

        private void Awake()
        {
            entity = GetComponent<GameObject>();
        }

        public abstract Vector3 GetDesiredVelocity();
    }
}
