using UnityEngine;


[CreateAssetMenu(fileName = "New FireStats", menuName = "FireStats", order = 3)]
public class FireStats : ScriptableObject
{
    [SerializeField] private int _damage = 1;
    public int damage { get { return _damage; } }

    [SerializeField] private float _damageCooldown = 1;
    public float damageCooldown { get { return _damageCooldown; } }

    [SerializeField] private float _lifetime = 1;
    public float lifetime { get { return _lifetime; } }

    [SerializeField] private float _dotLifetime = 1;
    public float dotLifetime { get { return _dotLifetime; } }
}
