using UnityEngine;

public class PlayerAvatarManager : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rightPos;

    [SerializeField]
    private Vector3 _leftPos;

    public void FlipX(bool active)
    {
        transform.rotation = Quaternion.Euler(0, active ? 180 : 0, 0);
        transform.localPosition = active ? _leftPos : _rightPos;
    }
}
