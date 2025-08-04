using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _speed = 10f;
    Vector2 _direction = Vector2.up;

    public void SetDirection(Vector2 dir)
    {
        _direction = dir.normalized;
    }

    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}

