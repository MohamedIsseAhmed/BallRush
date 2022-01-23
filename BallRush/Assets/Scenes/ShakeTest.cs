using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ShakeTest : MonoBehaviour
{
   public float shakeTime=0.2f;
    public GameObject ground;
   public float strengh;
   public float radius;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shake();
        }
        isOnGround();
    }
    private void Shake()
    {
        transform.DOShakePosition(shakeTime,strengh);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, radius);
    }
    private void isOnGround()
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        print(colliders.Length);
    }
}
