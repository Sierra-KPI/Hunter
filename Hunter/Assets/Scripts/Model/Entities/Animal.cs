using System.Collections.Generic;

namespace Hunter.Model.Entities
{
    public abstract class Animal : Entity
    {
        public float BoardSeekRadius { get; set; }
        public float BodySeekRadius { get; set; }
        public List<Entity> Entities = new();
    }
}
