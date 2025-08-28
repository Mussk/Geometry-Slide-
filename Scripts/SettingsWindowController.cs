using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsWindowController : MonoBehaviour
{
    [SerializeField]
    private Slider masterSlider;

    [SerializeField] 
    private Slider backgroundSlider;

    [SerializeField]
    private Slider sfxSlider;

    [Header("ExposedParameters")]
    [SerializeField]
    private string masterVolumeExpParam;
    [SerializeField]
    private string sfxVolumeExpParam;
    [SerializeField]
    private string backgroundVolumeExpParam;

    [SerializeField]
    private AudioMixer audioMixer;

    private void Awake()
    {
        SetVolumeSliders();
        gameObject.SetActive(false);

        masterSlider.onValueChanged.AddListener(delegate { SetSoundVolume(masterSlider, masterVolumeExpParam); });
        backgroundSlider.onValueChanged.AddListener(delegate { SetSoundVolume(backgroundSlider, backgroundVolumeExpParam); });
        sfxSlider.onValueChanged.AddListener(delegate { SetSoundVolume(sfxSlider, sfxVolumeExpParam); });
    }


    private void OnEnable()
    {
        SetVolumeSliders();
    }

    private void SetVolumeSliders()
    {
        audioMixer.GetFloat(masterVolumeExpParam, out var masterVolume);
        audioMixer.GetFloat(sfxVolumeExpParam, out var sfxVolume);
        audioMixer.GetFloat(backgroundVolumeExpParam, out var bgVolume);

        float masterVolumeSliderValue = Mathf.Pow(10, masterVolume / 20f);
        float bgVolumeSliderValue = Mathf.Pow(10, sfxVolume / 20f);
        float sfxVolumeSliderValue = Mathf.Pow(10, bgVolume / 20f);

        masterSlider.SetValueWithoutNotify(masterVolumeSliderValue);
        sfxSlider.SetValueWithoutNotify(sfxVolumeSliderValue);
        backgroundSlider.SetValueWithoutNotify(bgVolumeSliderValue);

    }

    private void SetSoundVolume(Slider slider, string channel)
    {
        float dB = Mathf.Log10(Mathf.Clamp(slider.value, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat(channel, dB);
    }
}
