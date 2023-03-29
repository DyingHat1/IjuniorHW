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
    [SerializeField] private AlarmActivator _activateAlarm;

    private bool _isCoroutineOn;

    private void Start()
    {
        _isCoroutineOn = false;
        _audioSource.volume = _minVolume;
    }    

    private void Update()
    {
       if (_isCoroutineOn == false)
        {
            if (_audioSource.isPlaying == false)
            {
                if (_activateAlarm.IsThiefInHouse())
                {
                    _audioSource.Play();
                    StartCoroutine(VolumeChanger());
                }
            }
            else
            {
                if (_activateAlarm.IsThiefInHouse() == false)
                {
                    StartCoroutine(VolumeChanger());
                }
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

    private IEnumerator VolumeChanger()
    {
        _isCoroutineOn = true;
        ChangeVolume(_activateAlarm.IsThiefInHouse());

        while(_audioSource.volume != _minVolume && _audioSource.volume != _maxVolume)
        {
            ChangeVolume(_activateAlarm.IsThiefInHouse());
            yield return null;
        }
        
        if(_audioSource.volume == _minVolume)
        {
            _audioSource.Stop();
        }

        _isCoroutineOn = false;
    }
}


