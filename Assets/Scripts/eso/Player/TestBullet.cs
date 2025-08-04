using Unity.VisualScripting;
using UnityEngine;

public class TestBullet : MonoBehaviour
{
    [SerializeField] private string _shot_Side_Tag;
    public void SetShotSideTag(string shot_Side_Tag)
    {
        _shot_Side_Tag = shot_Side_Tag;
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Bullet1") || other.CompareTag("Bullet2"))
        //{
        //    Debug.Log(other.tag + "/" + tag + "/A");
        //    return;
        //}
        if((other.tag == "Player1") && "Bullet1" == tag)
        {
            Debug.Log(other.tag + "/" + tag + "/B");
            return;
        }
        if((other.tag == "Player2") && "Bullet2" == tag)
        {
            Debug.Log(other.tag + "/" + tag + "/C");
            return;
        }
        Debug.Log(other.tag + "/" + tag + "/D");
        Destroy(this.gameObject);
    }
}
