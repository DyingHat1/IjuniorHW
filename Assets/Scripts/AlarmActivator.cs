using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmActivator : MonoBehaviour
{
    private bool _isThiefInHouse;

    public bool IsThiefInHouse()
    {
        return _isThiefInHouse;
    }

    private void Start()
    {
        _isThiefInHouse = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ThiefMovement>(out ThiefMovement thief))
            _isThiefInHouse = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ThiefMovement>(out ThiefMovement thief))
            _isThiefInHouse = false;
    }
}
