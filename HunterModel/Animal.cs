namespace HunterModel
{
    public class Animal : Entity
    {
        public float BoardSeekRadius { get; set; }
        public float BodySeekRadius { get; set;}
        public List<Entity> entities;
    }
}
