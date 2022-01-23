using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Color myColor;

    private void Awake()
    {
        myColor = GetComponent<MeshRenderer>().material.color;
    }
    private void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("Ball"))
        {
            ChangeColor(other.transform);
            BallManager.Instance.StartScalingCoroutine();
        }
    }

    private void ChangeColor(Transform other)
    {
       
        for (int i = 0; i < other.childCount; i++)
        {
            other.GetChild(i).GetComponent<MeshRenderer>().material.color = myColor;
        }
    }
}

