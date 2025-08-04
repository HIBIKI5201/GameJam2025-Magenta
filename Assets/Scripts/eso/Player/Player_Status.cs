using UnityEngine;

public class Player_Status : MonoBehaviour
{
    [SerializeField] int _now_hp;
    [SerializeField] int _max_hp;
    private bool _is_death;
    void Start()
    {
        StatusInitialize();
    }

    private void StatusInitialize()
    {
        _now_hp = _max_hp;
        _is_death = false;
    }

    public void TakeDamage(int damage)
    {
        if(_is_death) return;
        Debug.Log(damage);
        _now_hp -= damage;

        if (_now_hp <= 0)
        {
            _is_death = true;
            _now_hp = 0;
        }
    }
}
