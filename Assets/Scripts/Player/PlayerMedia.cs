using UnityEngine;

public class PlayerMedia : MediaController<Player>
{
    private void OnEnable()
    {
        host.OnAim += OnAim;
        host.OnAttack += OnAttack;
    }

    private void OnAim(float angle)
    {
        sprite.flipX = angle > 90 || angle < -90;
    }

    private void OnAttack()
    {
        sfx.PlayOneShot(sfx.clip);
    }


    private void OnDisable()
    {
        host.OnAim -= OnAim;
        host.OnAttack -= OnAttack;
    }
}
