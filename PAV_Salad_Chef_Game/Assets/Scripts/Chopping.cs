using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chopping : MonoBehaviour
{
    public float choppingTime;
    public Timer chopTimer;
    public GameObject choppingBoardOccupied = null;
    // Start is called before the first frame update
    void Start()
    {
        choppingTime = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered chopping zone");
        other.GetComponent<PlayerController>().canChop = true;
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<PlayerController>().canChop = true;
    }

    public void StartChopping(bool clickedOnChoppingBoard, int num)
    {
        StartCoroutine(ChopIngredients(clickedOnChoppingBoard, num));

    }

    IEnumerator ChopIngredients( bool clickedOnChoppingBoard, int num)
    {
        if (clickedOnChoppingBoard == true)
        {
            chopTimer.timerIsRunning = true;
            chopTimer.waitingTime = choppingTime * num;
            chopTimer.slider = choppingBoardOccupied.
                GetComponentInChildren<Slider>();
        }

        yield return new WaitForSeconds(chopTimer.waitingTime);

        if (chopTimer.waitingTime == 0)
        {
            Debug.Log("done chopping");
            //Deliver the salad
        }
    }
}
