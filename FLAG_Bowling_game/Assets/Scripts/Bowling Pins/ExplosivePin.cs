using System.Drawing;
using UnityEngine;

public class ExplosivePin : BowlingPinBase
{
    public ExplosivePin()
    {
        //explosion on contact with other pins or with the ball
        //both can happen if we want more pin variations
        points = 80;
        abilityType = new Explosion();

        TryDoAbility();
    }
}
