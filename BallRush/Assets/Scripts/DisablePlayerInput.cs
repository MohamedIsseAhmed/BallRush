using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePlayerInput : MonoBehaviour
{
    public static event System.Action OnDisablePlayerInput;


    private void OnTriggerEnter(Collider other)
    {
        OnDisablePlayerInput?.Invoke();
    }
}
