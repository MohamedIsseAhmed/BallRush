using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    public static event System.Action scoreEvent;

    private void OnTriggerEnter(Collider other)
    {
     
        if (other.CompareTag("Ball"))
        {
           other.gameObject.SetActive(false);
            scoreEvent?.Invoke();
        }
    }
}
