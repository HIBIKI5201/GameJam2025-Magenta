using SymphonyFrameWork.System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// オーディオ設定を管理するクラス
/// </summary>
public class AudioOption : MonoBehaviour
{
    // オーディオミキサー
    public AudioMixer AudioMixer;
    // BGMスライダー
    public Slider BGMSlider;
    // SEスライダー
    public Slider SESlider;
    // マスター音量スライダー
    public Slider MasterSlider;

    /// <summary>
    /// 初期化処理 (Awake)
    /// </summary>
    private void Awake()
    {
        BGMSlider.onValueChanged.AddListener(SetBGM);
        SESlider.onValueChanged.AddListener(SetSE);
        MasterSlider.onValueChanged.AddListener(SetMaster);

        // 各音量を取得してスライダーに反映
        AudioMixer.GetFloat("BGMVolume", out float bgmVolume);
        BGMSlider.value = bgmVolume;
        AudioMixer.GetFloat("SEVolume", out float seVolume);
        SESlider.value = seVolume;
        AudioMixer.GetFloat("MasterVolume", out float mastervolume);
        MasterSlider.value = mastervolume;
    }

    /// <summary>
    /// BGM音量を設定する
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetBGM(float volume)
    {
        AudioManager.VolumeSliderChanged("BGM", volume);
    }

    /// <summary>
    /// SE音量を設定する
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetSE(float volume)
    {
        AudioManager.VolumeSliderChanged("SE",volume);
    }

    /// <summary>
    /// マスター音量を設定する
    /// </summary>
    /// <param name="volume">音量</param>
    public void SetMaster(float volume)
    {
        AudioManager.VolumeSliderChanged("Master", volume);
    }
}