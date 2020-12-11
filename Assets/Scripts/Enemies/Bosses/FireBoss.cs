using UnityEngine;

public class FireBoss : Boss
{
    [HideInInspector] public bool keepDirection = false;

    protected override void ChangeDirection()
    {
        if (element.GetElement().Equals("woods") && keepDirection)
        {
            return;
        }

        base.ChangeDirection();
    }
}
