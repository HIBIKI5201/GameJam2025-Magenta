using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public float MaxHealth => _maxHealth;
    public float MoveSpeed => _moveSpeed;
    public float InvincibilityTime => _invincibilityTime;

    [SerializeField]
    private float _moveSpeed = 5f;
    [SerializeField]
    private float _maxHealth = 100f;
    [SerializeField]
    private float _invincibilityTime = 0.5f;
}
