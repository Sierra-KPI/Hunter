using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Model.Entities
{
    public abstract class HerdAnimal : Animal
    {


        public override void Move() {}
        public abstract void MoveInHerd(Animal[] animals);

    }
}
