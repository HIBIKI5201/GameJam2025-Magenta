using UnityEngine;

public class Player_Main_System : MonoBehaviour
{
    [SerializeField] Player_Controller _player_Controller;
    [SerializeField] Player_Movement _player_Movement;
    [SerializeField] Player_Status player_Status;

    //---テスト---
    [SerializeField] TestBulletManager bullet;



    void Start()
    {
        _player_Movement.SetAction(_player_Controller.GetMove);
    }

    void Update()
    {
        bullet.Shot();
    }

    public void Initialize()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if ((tag == "Player1") && other.CompareTag("Bullet1"))
        {
            //Debug.Log(other.tag + "/" + tag + "/D");
            return;
        }
        if ((tag == "Player2") && other.CompareTag("Bullet2"))
        {
            //Debug.Log(other.tag + "/" + tag + "/E");
            return;
        }
        //Debug.Log(other.tag + "/" + tag + "/F");
        player_Status.TakeDamage(1);
    }
}
