using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class BallManager : MonoBehaviour
{
    public List<Transform> balls=new List<Transform> ();

    [SerializeField] private float scalingTime;
    [SerializeField] private float deScalingTime;
    [SerializeField] private float waitTime=0.05f;
    [SerializeField] private float scaleFactor;
    [SerializeField] private float ballSlerpValue;
    [SerializeField] private float moveToOriginXvalue;
    [SerializeField] private float ballWaitingTime;
    [SerializeField] float moveToPreviousBallXPosition = 0.25f;
    [SerializeField] private GameObject ballNet;
    [SerializeField] private Vector3 ballNetOffset;
    [SerializeField] Transform[] pathsTransforms;
    [SerializeField] Vector3[] paths=new Vector3[5];

    private bool gameOver = false;
    public static BallManager Instance;

    private bool hasFinished=false;
    void Awake()
    {
        Instance = this;
    }

  
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if(!hasFinished)
                MoveBalls();
        }
        if (Input.GetMouseButtonUp(0))
        {
            MoveToOrigin();
        }
        if (gameOver)
        {
            StartCoroutine(Fly());
        }
    }
    private void OnEnable()
    {
        Finish.OnFinished += Finish_OnFinished;
        DisablePlayerInput.OnDisablePlayerInput += DisablePlayerInput_OnDisablePlayerInput;
       
        ToVector();
    }

    private void DisablePlayerInput_OnDisablePlayerInput()
    {
        hasFinished = true;
    }

    private void  Finish_OnFinished()
    {
        // StartCoroutine(FlyCor());
        gameOver = true;
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
    private void ToVector()
    {
        for (int i = 0; i < paths.Length; i++)
        {
            paths[i] = pathsTransforms[i].transform.position;
        }
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
            balls[i].transform.DOLocalMove(pos,moveToOriginXvalue);
            //balls[i].transform.localPosition = Vector3.MoveTowards(balls[i].localPosition, pos, 5 * Time.deltaTime);
        }
    }
  
    private IEnumerator  Fly()
    {
        for (int i = balls.Count-1; i>=0; i--)
        {
            balls[i].transform.parent = null;
            Vector3 targetPos = ballNet.transform.localPosition - balls[i].transform.position;
          balls[i].transform.DOMove(ballNet.transform.localPosition, ballSlerpValue); /*= Vector3.Lerp(balls[i].transform.position, net.transform.localPosition, ballSlerpValue * Time.deltaTime);*/

            // balls[i].transform.position = Vector3.Slerp(balls[i].transform.position, ballNet.transform.localPosition+ballNetOffset, ballSlerpValue * Time.deltaTime);

            yield return new WaitForSeconds(waitTime);
        }
        gameOver = false;
    }
    private IEnumerator FlyCor()
    {
        yield return StartCoroutine(Fly());
        gameOver = false;
    }
}
