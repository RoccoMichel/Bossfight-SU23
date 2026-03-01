using UnityEngine;

public class Boss : Entity
{
    public override void Die()
    {
        print("You win!!!");
    }
    protected override void OnReset()
    {
        identity = "Boss";
        maxHealth = 1000;
        base.OnReset();
    }
}
