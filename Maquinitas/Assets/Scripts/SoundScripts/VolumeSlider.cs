using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private enum VolumeType
    {
        MASTER,

        MUSIC,

        SFX,

		AMBIENCE
    }

    [SerializeField] private VolumeType volumeType;

    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = this.GetComponentInChildren<Slider>();
    }

	private void Start()
	{
        

    }

	private void Update()
    {
        /*switch (volumeType)
        {
            case VolumeType.MASTER:
                volumeSlider.value = AudioManager.instance.masterVolume;
                break;
            case VolumeType.MUSIC:
                volumeSlider.value = AudioManager.instance.musicVolume;
                break;
            case VolumeType.SFX:
                volumeSlider.value = AudioManager.instance.SFXVolume;
                break;
			case VolumeType.AMBIENCE:
				volumeSlider.value = AudioManager.instance.ambienceVolume;
			   break;
			default:
                Debug.LogWarning("???????como???????");
            break;
        }*/
    }

    public void OnSliderValueChange()
    {
        switch (volumeType)
        {
            case VolumeType.MASTER:
                AudioManager.instance.masterVolume = volumeSlider.value;
                break;
            case VolumeType.MUSIC:
                AudioManager.instance.musicVolume = volumeSlider.value;
                break;
            case VolumeType.SFX:
                AudioManager.instance.SFXVolume = volumeSlider.value;
                break;
			case VolumeType.AMBIENCE:
				AudioManager.instance.ambienceVolume = volumeSlider.value;
			break;
			default:
                Debug.LogWarning("???????como???????");
            break;
        }
    }
}
