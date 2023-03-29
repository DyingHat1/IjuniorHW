using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AlarmVolumeChanger : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _minVolume;
    [SerializeField] private float _deltaVolume;

    private bool _isThiefInHouse = false;
    private bool _isCoroutineOn;

    public void ActivateAlarm()
    {
        _isThiefInHouse = true;

        if (_audioSource.isPlaying == false)
            _audioSource.Play();

        StartCoroutine(VolumeChanger(_maxVolume));
    }

    public void DeactivateAlarm()
    {
        _isThiefInHouse = false;
        StartCoroutine(VolumeChanger(_minVolume));
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

    private IEnumerator VolumeChanger(float directVolume)
    {
        if (_isCoroutineOn)
            StopAllCoroutines();
        else
            _isCoroutineOn = true;

        while(_audioSource.volume != directVolume)
        {
            ChangeVolume(_isThiefInHouse);
            yield return null;
        }
        
        if(_audioSource.volume == _minVolume)
        {
            _audioSource.Stop();
        }

        _isCoroutineOn = false;
    }
}


