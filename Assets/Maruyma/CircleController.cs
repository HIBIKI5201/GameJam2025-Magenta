using UnityEngine;

public class CircleBulletController : MonoBehaviour
{
    IBullet _bullet;

    void Start()
    {
        
    }

    void Update()
    {
        _bullet.Update(Time.deltaTime);
    }
}
