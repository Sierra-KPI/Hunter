namespace Hunter.Model.Entities
{
    public abstract class HerdAnimal : Animal
    {


        public override void Move() { }
        public abstract void MoveInHerd(HerdAnimal[] animals);

    }
}
