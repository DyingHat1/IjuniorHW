using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmVolumeChanger : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _minVolume;
    [SerializeField] private float _deltaVolume;
    [SerializeField] private ActivateAlarm _activateAlarm;

    private void Update()
    {
        if (_audioSource.volume < _maxVolume && _audioSource.volume > _minVolume)
        {
            ChangeVolume(_activateAlarm.IsThiefInHouse());
        }
        else
        {
            if (_activateAlarm.IsThiefInHouse())
            {
                if (_audioSource.isPlaying == false)
                {
                    _audioSource.Play();
                    ChangeVolume(_activateAlarm.IsThiefInHouse());
                }
            }
            else
            {
                if (_audioSource.volume == _maxVolume)
                    ChangeVolume(_activateAlarm.IsThiefInHouse());
                else
                    _audioSource.Stop();
            }
        }
    }

    private void ChangeVolume(bool isThiefInHouse)
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, VolumeBound(isThiefInHouse), _deltaVolume);
    }

    private float VolumeBound(bool isThiefInHouse)
    {
        if (isThiefInHouse)
            return _maxVolume;

        return _minVolume;
    }
}


