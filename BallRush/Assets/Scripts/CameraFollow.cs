using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3  offset;
    [SerializeField] private Vector3  rotaionOffset;
    [SerializeField] private float distance;
    [SerializeField] private float lerpValue;
    [SerializeField] private float slepValue;
    [SerializeField] private Vector3 finishOffset;
    [SerializeField] private float y;

    [SerializeField] private GameObject target;
    private bool finished = false;
    private bool gameOver;

    void Awake()
    {
        Finish.OnFinished += Finish_OnFinished;
    }

    private void Finish_OnFinished()
    {
        gameOver = true;
        finished = true;
       
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!gameOver && !finished)
           transform.position = player.position+offset;

        if (gameOver)
        {
            StartCoroutine(FinishCam());
        }
       // Folloow();
    }
    private void Folloow()
    {
        Vector3 direction=player.position-transform.position;
        Quaternion lookDirection=Quaternion.LookRotation(direction);

        Vector3 desiredPosion = player.position + (-player.forward * distance);

        transform.position=Vector3.MoveTowards(transform.position, desiredPosion+offset,5*Time.deltaTime);
        transform.rotation=Quaternion.Slerp(transform.rotation, lookDirection,slepValue*Time.deltaTime);
        
    }
    private IEnumerator Came()
    {
        //transform.position = Vector3.Lerp(transform.position, transform.position + finishOffset, lerpValue * Time.deltaTime);
        transform.DOMove(target.transform.position+finishOffset,lerpValue);    
        yield return null;
    }
    IEnumerator FinishCam()
    {
        yield return StartCoroutine(Came());
        gameOver = false;
    }
}
