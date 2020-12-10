using System;
using UnityEngine;

public class FireBossWoods : MonoBehaviour, IBossElement
{
    [SerializeField] private Enemy boss = null;
    private Player player = null;
    [SerializeField] chargeSpeed = 1;
    [SerializeField] private GameObject missile = null;

    public void UseAbility(int ability)
    {
        switch (ability)
        {
            case 0:
                break;
            case 1:
                break;
        }
    }

    public void Attack()
    {
        
    }
}
