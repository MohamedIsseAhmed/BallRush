using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemnt : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] private Transform model;
    [SerializeField] private Vector3 modelVectorOffset;

    private bool hasFinished = false;
    private bool hasDisabledInput;
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Finish.OnFinished += Finish_OnFinished;
        DisablePlayerInput.OnDisablePlayerInput += DisablePlayerInput_OnDisablePlayerInput;
    }

    private void DisablePlayerInput_OnDisablePlayerInput()
    {
        hasDisabledInput = true;
        speed += 1.75f;
    }

    private void Finish_OnFinished()
    {
      hasFinished = true;
    }

    void Update()
    {
        if (!hasFinished)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime * speed, Space.World);
            if (hasDisabledInput)
            {
                return;
            }
            if (Input.GetMouseButton(0))
            {
                if(!hasDisabledInput)
                    Move();
                model.GetComponent<Animator>().SetTrigger("Running");

            }
          
            if (Input.GetMouseButtonUp(1))
            {
                //  model.GetComponent<Animator>().ResetTrigger("Running");
            }
        }
       
    }
    private void OnDisable()
    {
        Finish.OnFinished -= Finish_OnFinished;
        DisablePlayerInput.OnDisablePlayerInput -= DisablePlayerInput_OnDisablePlayerInput;
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
            Vector3 modelVector = hit.point;
            // clampedVector.x = Mathf.Clamp(clampedVector.x, -4, 4);
           clampedVector.y = firsBall.localPosition.y;
           clampedVector.z = firsBall.localPosition.z;

            //modelVector.y=model.localPosition.y;
            //modelVector.z=model.localPosition.z;

            model.localPosition=Vector3.MoveTowards(model.localPosition, clampedVector + modelVectorOffset, 5* Time.deltaTime);
          
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
