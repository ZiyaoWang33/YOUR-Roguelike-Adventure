using UnityEngine;

public class EnemyMedia : MediaController<Enemy>
{
    private void OnEnable()
    {
        host.OnMove += OnMove;
        host.OnAttack += OnAttack;
    }

    private void OnMove(float direction)
    {
        sprite.flipX = direction > 0;
        print(sprite.flipX);
    }

    private void OnAttack()
    {
        anim.SetTrigger("Attack");
    }

    private void OnDisable()
    {
        host.OnMove -= OnMove;
        host.OnAttack -= OnAttack;
    }
}
