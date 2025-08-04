using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーのメインシステムを管理するクラス
/// </summary>
public class Player_Main_System : MonoBehaviour
{
    public event Action Ondead
    {
        add => player_Status.OnDeath += value;
        remove => player_Status.OnDeath -= value;
    }

    [SerializeField]
    private string _selectInputActionName;

    // プレイヤーコントローラー
    [SerializeField] Player_Controller _player_Controller;
    // プレイヤーの移動
    [SerializeField] Player_Movement _player_Movement;
    // プレイヤーステータス
    [SerializeField] Player_Status player_Status;

    [SerializeReference, SubclassSelector]
    IBulletGenerator[] _bulletGenerators;

    private Player_Main_System _target;

    private int _selectedBulletGeneratorIndex;
    private bool _isPlaying;
    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        // 移動アクションを設定
        _player_Movement.SetAction(_player_Controller.GetMove);
    }

    /// <summary>
    /// 毎フレームの更新処理
    /// </summary>
    void Update()
    {
        if (!_isPlaying) return;
        BulletGeneratorUpdate();
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize(Player_Main_System target)
    {
        _target = target;
        foreach (var item in _bulletGenerators)
        {
            item.Init(transform, target.transform);
        }

        _player_Movement.SetMoveSpeedScale(_bulletGenerators[_selectedBulletGeneratorIndex].MoveSpeedScale);
    }

    public void StartEntity(PlayerInput input)
    {
        _player_Controller.Init(input);

        InputAction selectAction = input.actions[_selectInputActionName];
        selectAction.started += HandleSelect;

        _isPlaying = true;
    }

    public void StopEntity()
    {
        _isPlaying = false;
    }

    public void TakeDamage(float damage)
    {
        player_Status.TakeDamage((int)damage);
    }   

    private void BulletGeneratorUpdate()
    {
        if (_selectedBulletGeneratorIndex < 0 || _bulletGenerators.Length <= _selectedBulletGeneratorIndex)
        {
            Debug.LogError("Invalid bullet generator index: " + _selectedBulletGeneratorIndex);
            return;
        }

        // 選択された弾生成器を取得
        IBulletGenerator selectedGenerator = _bulletGenerators[_selectedBulletGeneratorIndex];

        selectedGenerator.Update(Time.deltaTime);
    }

    private void HandleSelect(InputAction.CallbackContext context)
    {
        // 入力された値を取得
        int inputValue = (int)Mathf.Sign(context.ReadValue<float>());
        // インデックスを更新
        _selectedBulletGeneratorIndex = (_selectedBulletGeneratorIndex + inputValue + _bulletGenerators.Length) % _bulletGenerators.Length;

        Debug.Log("Selected Bullet Generator Index: " + _selectedBulletGeneratorIndex);

         _player_Movement.SetMoveSpeedScale( _bulletGenerators[_selectedBulletGeneratorIndex].MoveSpeedScale);
    }
}