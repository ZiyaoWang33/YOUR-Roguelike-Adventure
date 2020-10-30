using UnityEngine;

[CreateAssetMenu(fileName = "New Player Stats", menuName = "Player Stats", order = 1)]
public class PlayerStats : ScriptableObject
{
    [SerializeField] private float _speed = 1;
    public float speed { get { return _speed; } }

    [SerializeField] private float _attackSpeed = 1;
    public float attackSpeed { get { return _attackSpeed; } }
}
