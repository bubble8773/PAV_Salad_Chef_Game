using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chopping : CommonAbstract
{
    public GameObject choppingBoardOccupied = null;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered chopping zone");
        
        player = other.GetComponent<PlayerController>();
        player.canChop = true;
    }

    private void OnTriggerExit(Collider other)
    {
        player = other.GetComponent<PlayerController>();
        player.canChop = false;
    }

    public void StartChopping(bool clickedOnChoppingBoard, int num)
    {
        //StartCoroutine(ChopIngredients(clickedOnChoppingBoard, num));
        slider = choppingBoardOccupied.GetComponentInChildren<Slider>();
        timer = choppingBoardOccupied.GetComponent<Timer>();
        StartTimer(clickedOnChoppingBoard, num, this.timer, slider );
        if (this.timer.waitingTime == 0)
        {
            Debug.Log("Done Chopping");
        }
    }

    //IEnumerator ChopIngredients( bool clickedOnChoppingBoard, int num)
    //{
    //    if (clickedOnChoppingBoard == true)
    //    {
    //        chopTimer.timerIsRunning = true;
    //        chopTimer.waitingTime = choppingTime * num;
    //        chopTimer.slider = choppingBoardOccupied.
    //            GetComponentInChildren<Slider>();
    //    }

    //    yield return new WaitForSeconds(chopTimer.waitingTime);

    //    if (chopTimer.waitingTime == 0)
    //    {
    //        Debug.Log("done chopping");
    //        //Deliver the salad
    //    }
    //}
}
