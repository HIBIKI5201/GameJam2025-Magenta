using UnityEngine;

public class ShipManager : MonoBehaviour
{
    [SerializeField]
    private Vector3 _velocity;
    [SerializeField]
    private float _bounds;

    private Vector3 _initializePos;

    private void Start()
    {
        _initializePos = transform.position;
    }

    private void Update()
    {
        transform.position += _velocity * Time.deltaTime;

        if (_bounds < Vector3.Distance(_initializePos, transform.position))
        {
            transform.position = _initializePos;
        }
    }
}
