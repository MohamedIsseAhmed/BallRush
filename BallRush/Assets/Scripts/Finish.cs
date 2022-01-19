using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public static Finish instance;
    public static event System.Action OnFinished;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //FLY UP BALLS
           
            OnFinished?.Invoke();
        }
    }
}
