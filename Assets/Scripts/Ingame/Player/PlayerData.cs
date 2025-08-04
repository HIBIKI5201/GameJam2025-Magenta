using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public float MoveSpeed => _moveSpeed;

    [SerializeField]
    private float _moveSpeed = 5f;
}
