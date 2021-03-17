using UnityEngine;

public abstract class Curse : MonoBehaviour
{
    protected Player player = null;

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public abstract string GetDescription();

    public abstract void ChangePlayerStats();
}
