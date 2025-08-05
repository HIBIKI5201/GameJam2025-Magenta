using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤーの各コンポーネントを統括し、弾の発射や武器切り替えなどのコアロジックを管理します。
/// </summary>
public class Player_Main_System : MonoBehaviour
{
    // --- プロパティ ---
    /// <summary>
    /// プレイヤーのステータスコンポーネントへの参照を取得します。
    /// </summary>
    public Player_Status PlayerStatus => _playerStatus;

    // --- シリアライズされたフィールド ---
    [Header("入力アクション設定")]
    [SerializeField] private string _selectInputActionName;

    [Header("プレイヤーの構成要素")]
    [SerializeField] private Player_Controller _playerController;
    [SerializeField] private Player_Movement _playerMovement;
    [SerializeField] private Player_Status _playerStatus;

    [Header("使用可能な弾ジェネレーター")]
    [SerializeReference, SubclassSelector]
    private IBulletGenerator[] _bulletGenerators;
    [SerializeField]
    private Transform _bulletRoot;
    [SerializeField]
    private SelectBulletManager _bulletUI;

    // --- privateフィールド ---
    private Player_Main_System _opponent;
    private int _selectedBulletGeneratorIndex;
    private bool _isPlaying;

    /// <summary>
    /// Unityのライフサイクルメソッド。オブジェクトの初期化時に呼び出されます。
    /// </summary>
    private void Start()
    {
        // 移動コンポーネントに、入力を提供する関数を渡します。
        _playerMovement.SetMoveInputFunction(_playerController.GetMoveInput);
    }

    /// <summary>
    /// Unityのライフサイクルメソッド。毎フレーム呼び出されます。
    /// </summary>
    private void Update()
    {
        // プレイ中でなければ処理を中断します。
        if (!_isPlaying) return;

        // 弾ジェネレーターの更新処理を呼び出します。
        UpdateBulletGenerator();
    }

    /// <summary>
    /// このプレイヤーを初期化します。
    /// </summary>
    /// <param name="opponent">対戦相手のプレイヤー。</param>
    public void Initialize(Player_Main_System opponent)
    {
        _opponent = opponent;

        // 全ての弾ジェネレーターを初期化します。
        foreach (var generator in _bulletGenerators)
        {
            generator.Initialize(transform, _opponent.transform, _bulletRoot);
        }

        // 初期選択されている弾ジェネレーターに合わせて移動速度の倍率を設定します。
        _playerMovement.ApplyMoveSpeedScale(_bulletGenerators[_selectedBulletGeneratorIndex].MoveSpeedScale);
    }

    /// <summary>
    /// プレイヤーの動作を有効化します。
    /// </summary>
    /// <param name="playerInput">使用するPlayerInputコンポーネント。</param>
    public void Activate(PlayerInput playerInput)
    {
        _playerController.Initialize(playerInput);

        // 武器切り替えアクションにイベントハンドラを登録します。
        InputAction selectAction = playerInput.actions[_selectInputActionName];
        selectAction.started += HandleSelectAction;

        _isPlaying = true;
    }

    /// <summary>
    /// プレイヤーの動作を無効化します。
    /// </summary>
    public void Deactivate()
    {
        _isPlaying = false;
    }

    /// <summary>
    /// ダメージ処理をPlayer_Statusコンポーネントに依頼します。
    /// </summary>
    /// <param name="damageAmount">受けるダメージ量。</param>
    public void TakeDamage(float damageAmount)
    {
        _playerStatus.TakeDamage(damageAmount);
    }

    /// <summary>
    /// 現在選択されている弾ジェネレーターの更新処理を呼び出します。
    /// </summary>
    private void UpdateBulletGenerator()
    {
        // インデックスの妥当性を確認します。
        if (_selectedBulletGeneratorIndex < 0 || _bulletGenerators.Length <= _selectedBulletGeneratorIndex)
        {
            Debug.LogError("弾ジェネレーターのインデックスが不正です: " + _selectedBulletGeneratorIndex, this);
            return;
        }

        // 選択されているジェネレーターのUpdateを呼び出します。
        IBulletGenerator selectedGenerator = _bulletGenerators[_selectedBulletGeneratorIndex];
        selectedGenerator.Update(Time.deltaTime);
    }

    /// <summary>
    /// 武器切り替えの入力アクションを処理します。
    /// </summary>
    private void HandleSelectAction(InputAction.CallbackContext context)
    {
        // 入力値（-1または1）を取得します。
        int inputValue = (int)Mathf.Sign(context.ReadValue<float>());

        // インデックスを更新します。剰余演算子を使って範囲内に収めます。
        int newIndex = _selectedBulletGeneratorIndex + inputValue;
        if (newIndex < 0)
        {
            newIndex = _bulletGenerators.Length - 1;
        }
        else if (newIndex >= _bulletGenerators.Length)
        {
            newIndex = 0;
        }
        _selectedBulletGeneratorIndex = newIndex;

        Debug.Log("選択された弾ジェネレーター: " + _selectedBulletGeneratorIndex);

        // 新しいジェネレーターの移動速度倍率をプレイヤーの移動に反映させます。
        _playerMovement.ApplyMoveSpeedScale(_bulletGenerators[_selectedBulletGeneratorIndex].MoveSpeedScale);
    }
}