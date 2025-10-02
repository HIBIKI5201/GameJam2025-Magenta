using UnityEngine;

public class PlayerAvatarManager : MonoBehaviour
{
    [SerializeField]
    private Vector3 _rightPos;

    [SerializeField]
    private Vector3 _leftPos;

    private SpriteRenderer[] _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
    }

    public void FlipX(bool active)
    {
        transform.rotation = Quaternion.Euler(0, active ? 180 : 0, 0);
        transform.localPosition = active ? _leftPos : _rightPos;
    }

    public async void HitEffect(float duration, int effectCount = 5, int opacityStrangth = 1)
    {
        const string OPACITY_NAME = "_Opacity";

        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;

            // 0～1 の進行度。
            float progress = time / duration;

            // サイクル数を掛けて effectCount 回分の波を作る。
            float opacity = (Mathf.Cos(progress * effectCount * Mathf.PI * 2f) + 1) / 2;
            opacity = opacity * opacityStrangth; // 強度を掛ける。

            foreach (var renderer in _spriteRenderer)
            {
                renderer.material.SetFloat(OPACITY_NAME, opacity);
            }

            await Awaitable.NextFrameAsync();
        }

        // 最後に必ず0に戻す。
        foreach (var renderer in _spriteRenderer)
        {
            renderer.material.SetFloat(OPACITY_NAME, 0f);
        }
    }
}
