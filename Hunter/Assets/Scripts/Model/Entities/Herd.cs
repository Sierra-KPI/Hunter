using System;


namespace Hunter.Model.Entities
{
    public class Herd : Animal
    {
        private HerdAnimal[] _animals;

        public Herd()
        {
            int numberOfAnimals = new Random().Next(3, 10);
            _animals = new Deer[numberOfAnimals]; // to fix
            for (var i = 0; i < numberOfAnimals; i++)
            {
                _animals[i] = new Deer();
            }

        }

        public override void Move()
        {
            foreach (HerdAnimal deer in _animals)
            {
                deer.MoveInHerd(_animals);
            }
        }

        public HerdAnimal[] GetAnimals() => _animals;

    }
}
