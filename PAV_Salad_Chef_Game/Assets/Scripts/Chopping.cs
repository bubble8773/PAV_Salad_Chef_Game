using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chopping : CommonAbstract
{
    public GameObject choppingBoardOccupied = null;
    public List<GameObject> sliderHolders;
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

    public void StartChopping(bool allowedToChop, int num, Timer timer)
    {
        if (choppingBoardOccupied.name.Contains("_1"))
        {
            slider = GameObject.Find("ChoppingSlider1").GetComponent<Slider>();
            GameManager._instance.ClampUIPositions(sliderHolders[0].transform.position, slider);
            
        }

        if (choppingBoardOccupied.name.Contains("_2"))
        {
            slider = GameObject.Find("ChoppingSlider2").GetComponent<Slider>();
            GameManager._instance.ClampUIPositions(sliderHolders[1].transform.position, slider);
        }
        
        timer = choppingBoardOccupied.GetComponent<Timer>();
        StartTimer(allowedToChop, num, timer, slider);
        
    }

    
}
