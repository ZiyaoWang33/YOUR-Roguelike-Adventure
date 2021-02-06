using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Enemy Stats", order = 2)]
public class EnemyStats : ScriptableObject
{
    [SerializeField] private float _speed = 1;
    public float speed { get { return _speed; } }

    [SerializeField] private float _attackSpeed = 1;
    public float attackSpeed { get { return _attackSpeed; } }

    [SerializeField] private int _damage = 1;
    public int damage  { get { return _damage; } }
}
