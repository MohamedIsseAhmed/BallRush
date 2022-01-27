using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ball")
        {
            if (!BallManager.Instance.balls.Contains(other.gameObject.transform))
            {
                other.GetComponent<SphereCollider>().isTrigger = false;
             
                other.gameObject.AddComponent<CollisionDetection>();
                
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                BallManager.Instance.StackingBalls(other.gameObject.transform, BallManager.Instance.balls.Count - 1);

            }
        }
    }
}
