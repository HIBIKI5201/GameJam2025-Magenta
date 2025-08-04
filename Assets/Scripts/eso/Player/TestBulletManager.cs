using UnityEngine;

public class TestBulletManager : MonoBehaviour
{
    [SerializeField] GameObject Obj;
    public float count;
    public float shotmax;

    public void Shot()
    {
        if (count >= shotmax)
        {
            count = 0;
            var obj = Instantiate(Obj, transform.localPosition, transform.localRotation);
            obj.GetComponent<Rigidbody>().linearVelocity = Vector3.right * 20;
            obj.GetComponent<TestBullet>().SetShotSideTag(tag);
            if (tag == "Player1")
            {
                obj.tag = "Bullet1";
            }
            if (tag == "Player2")
            {
                obj.tag = "Bullet2";
            }

        }
        else
        {
            count += Time.deltaTime;
        }
    }
}
