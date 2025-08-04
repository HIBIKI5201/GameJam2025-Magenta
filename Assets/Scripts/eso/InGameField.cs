using Unity.VisualScripting;
using UnityEngine;

public class InGameField : MonoBehaviour
{
    private void Update()
    {
        
    }
    private void OnTrigger(Collision collision)
    {
        collision.transform.position = Vector3.zero;
    }
    private void OnCollisionExit(Collision collision)
    {
        collision.transform.position = Vector3.zero;
    }
}
