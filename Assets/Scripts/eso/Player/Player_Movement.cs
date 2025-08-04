using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Movement : MonoBehaviour
{
    private Func<Vector2> Move_Input;
    [SerializeField] private float _move_Speed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Vector3 _movement_Area;
    [SerializeField] private Vector3 _movement_Pos;
    void Start()
    {
        
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 vec = new Vector3(Move_Input.Invoke().x, Move_Input.Invoke().y, 0) * Time.deltaTime;

        transform.Translate(vec * _move_Speed,Space.Self);

        transform.localPosition = new Vector2(
            Mathf.Clamp(transform.localPosition.x, -_movement_Area.x, _movement_Area.x),
            Mathf.Clamp(transform.localPosition.y, -_movement_Area.y, _movement_Area.y)
            );
    }

    public void SetAction(Func<Vector2> func)
    {
        Move_Input = func;
    }

    public void SetMovementArea(Vector3 pos, Vector3 area)
    {
        _movement_Area = area;
        _movement_Pos = pos;
    }
}
