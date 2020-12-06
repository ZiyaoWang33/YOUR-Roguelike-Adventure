using UnityEngine;

public interface IBossElement
{
    /* If a boss has multiple abilities while in a certain area, 
    put their cooldowns into an array so that the ability parameter can correspond to their index.
    If no array is needed, always pass 0 to this function.*/
    void UseAbility(int ability); 
    void Attack();
}
