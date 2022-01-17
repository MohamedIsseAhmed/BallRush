using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemnt : MonoBehaviour
{
    [SerializeField] float speed = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Move();
        }
        transform.Translate(Vector3.forward*speed*Time.deltaTime*speed,Space.World);
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Mouse X");


        Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Transform firsBall = BallManager.Instance.balls[0];
            Vector3 clampedVector = hit.point;
            // clampedVector.x = Mathf.Clamp(clampedVector.x, -4, 4);
           clampedVector.y = firsBall.localPosition.y;
           clampedVector.z = firsBall.localPosition.z;

            firsBall.localPosition = Vector3.MoveTowards(firsBall.localPosition, clampedVector, Time.deltaTime * 5);
        }

     

        //if(firsBall.localPosition.x > 4)
        //{
        //    firsBall.localPosition = new Vector3 (4, firsBall.localPosition.y, firsBall.localPosition.z);  
        //}
        //if (firsBall.localPosition.x < -4)
        //{
        //    firsBall.localPosition = new Vector3(-4, firsBall.localPosition.y, firsBall.localPosition.z);
        //}
    }
}
