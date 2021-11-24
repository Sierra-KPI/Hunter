using System;


namespace Hunter.Model.Entities
{
    public class Herd : Animal
    {
        private Deer[] Deers;

        public Herd()
        {
            int numberOfDeers = new Random().Next(3, 10);
            Deers = new Deer[numberOfDeers];
            for (var i = 0; i < numberOfDeers; i++)
            {
                Deers[i] = new Deer();
            }

        }

        public override void Move()
        {
            foreach (var deer in Deers)
            {
                deer.Move();
            }
        }

    }
}
