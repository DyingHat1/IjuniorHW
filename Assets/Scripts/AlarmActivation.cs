using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmActivation : MonoBehaviour
{
    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] protected float _maxVolume;
    [SerializeField] protected float _minVolume;
    [SerializeField] protected float _deltaVolume;

    protected bool _isThiefInHouse;

    private void Start()
    {
        _isThiefInHouse = false;
    }

    private void Update()
    {
        ChangeVolume();

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

    public float VolumeBound()
    {
        if (_isThiefInHouse)
            return _maxVolume;

        return _minVolume;
    }

    private void ChangeVolume()
    {
        _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, VolumeBound(), _deltaVolume);
    }
}


