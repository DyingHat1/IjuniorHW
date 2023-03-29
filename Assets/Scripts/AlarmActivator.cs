using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AlarmActivator : MonoBehaviour
{
    [SerializeField] private UnityEvent _activateAlarm;
    [SerializeField] private UnityEvent _deactivateAlarm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ThiefMovement>(out ThiefMovement thief))
            _activateAlarm.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ThiefMovement>(out ThiefMovement thief))
            _deactivateAlarm.Invoke();
    }
}
