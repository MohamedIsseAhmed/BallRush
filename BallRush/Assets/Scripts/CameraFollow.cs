using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3  offset;
    [SerializeField] private Vector3  rotaionOffset;
    [SerializeField] private float distance;
    [SerializeField] private float lerpValue;
    [SerializeField] private float slepValue;
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = player.position+offset;
        Folloow();
    }
    private void Folloow()
    {
        Vector3 direction=player.position-transform.position;
        Quaternion lookDirection=Quaternion.LookRotation(direction);

        Vector3 desiredPosion = player.position + (-player.forward * distance);

        transform.position=Vector3.MoveTowards(transform.position, desiredPosion+offset,5*Time.deltaTime);
        transform.rotation=Quaternion.Slerp(transform.rotation, lookDirection,slepValue*Time.deltaTime);
        
    }
}
