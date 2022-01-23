using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOver : MonoBehaviour
{
    public static event System.Action OnOver;
    [SerializeField] private TextMeshProUGUI levelComplatefText;
    [SerializeField] private GameObject winParticle;
    [SerializeField] private float waiteTime = 2.5f;

    [SerializeField] private Vector3 winParticleOffset;
    void OnEnable()
    {
        levelComplatefText.gameObject.SetActive(false);
        Finish.OnFinished += Finish_OnFinished;
    }
    private void OnDisable()
    {
        Finish.OnFinished -= Finish_OnFinished;
    }
    private void Finish_OnFinished()
    {
        StartCoroutine(EnablingGameOverUIS());
    }
    
    private IEnumerator EnablingGameOverUIS()
    {
        yield return new WaitForSeconds(waiteTime);
        levelComplatefText.gameObject.SetActive(true);

        Vector3 targetPosition=Camera.main.ScreenToWorldPoint(levelComplatefText.transform.localPosition);
        GameObject newWinParticle=Instantiate(winParticle);
        newWinParticle.transform.position = targetPosition+winParticleOffset;
        //Destroy(winParticle,3);

        OnOver?.Invoke();
    }
}
