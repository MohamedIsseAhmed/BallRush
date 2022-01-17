using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BallManager : MonoBehaviour
{
    public List<Transform> balls=new List<Transform> ();

    [SerializeField] private float scalingTime;
    [SerializeField] private float deScalingTime;
    [SerializeField] private float waitTime=0.05f;
    [SerializeField] private float scaleFactor;
    [SerializeField] float moveToPreviousBallXPosition = 0.25f;

    public static BallManager Instance;
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveBalls();
        }
        if(Input.GetMouseButtonUp(0))
        {
            MoveToOrigin();
        }
    }


    public void StackingBalls(Transform ball,int index)
    {
        ball.transform.SetParent(transform);    
        balls.Add(ball);
        Vector3 pos = balls[index].localPosition;
        pos.z += 1;
        ball.localPosition = pos;
        StartCoroutine(ScalingObjects());

    }

    IEnumerator ScalingObjects()
    {
        
        for (int i = balls.Count-1;  i >0; i--)
        {
            int index = i;
            Vector3 prevScale = new Vector3(1, 1, 1);
            prevScale *= scaleFactor;
            balls[index].DOScale(prevScale, scalingTime).OnComplete(()=>
            {
                balls[index].DOScale(new Vector3(1,1,1),deScalingTime);
            });
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void MoveBalls()
    {
        for (int i = 1; i < balls.Count; i++)
        {
            Vector3 pos = balls[i].transform.localPosition;
            pos.x = balls[i - 1].transform.localPosition.x;
            balls[i].transform.DOLocalMove(pos, moveToPreviousBallXPosition);
            //balls[i].transform.localPosition = Vector3.MoveTowards(balls[i].transform.localPosition, pos, 5 * Time.deltaTime);
        }
    }
    public void MoveToOrigin()
    {
        for (int i = 1; i < balls.Count; i++)
        {
            Vector3 pos = balls[i].transform.localPosition;
            pos.x = balls[0].transform.localPosition.x;
            balls[i].transform.DOLocalMove(pos, 0.75f);
            //balls[i].transform.localPosition = Vector3.MoveTowards(balls[i].localPosition, pos, 5 * Time.deltaTime);
        }
    }
}
