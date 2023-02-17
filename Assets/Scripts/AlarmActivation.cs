using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmActivation : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _minVolume;
    [SerializeField] private float _deltaVolume;

    private bool _isThiefInHouse;

    private void Start()
    {
        _isThiefInHouse = false;
    }

    private void Update()
    {
        if (_isThiefInHouse)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _deltaVolume);
        }
        else
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _deltaVolume);
        }

        if (_audioSource.volume == _minVolume)
            _audioSource.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ThiefMovement>(out ThiefMovement thief))
        {
            _audioSource.Play();
            _isThiefInHouse = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ThiefMovement>(out ThiefMovement thief))
        {
            _isThiefInHouse = false;
        }
    }
}
