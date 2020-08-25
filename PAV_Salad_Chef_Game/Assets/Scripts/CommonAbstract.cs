using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CommonAbstract : MonoBehaviour
{
    public Slider slider;
    public Timer timer;
    public float waitingTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateSpritesForOrder(int xPos, Ingredients ingredient, Transform parent)
    {
        GameObject item = Instantiate(Resources.Load<GameObject>("Prefabs/" + ingredient.ToString()));
        item.transform.SetParent(parent);
        item.transform.localPosition = new Vector3(xPos, 0, 0);

    }

    public void StartTimer(bool timerCondition, int num, Timer timer, Slider slider)
    {
        StartCoroutine(BeginTimer(timerCondition, num, timer, slider));

    }

    IEnumerator BeginTimer(bool timerCondition, int num, Timer timer, Slider slider)
    {
        if (timerCondition == true)
        {
            timer.timerIsRunning = true;
            timer.waitingTime = waitingTime * num;
            timer.slider = slider;
            timer.slider.maxValue = timer.waitingTime;
        }

        yield return new WaitForSeconds(0.1f);

        //if (OrderTimer.waitingTime == 0)
        //{
        //    Debug.Log("Times up");
        //    //Deliver the salad
        //}
    }
}
