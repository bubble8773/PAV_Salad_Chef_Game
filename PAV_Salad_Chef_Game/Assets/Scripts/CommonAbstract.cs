using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CommonAbstract : MonoBehaviour
{
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

    
    public void StopTimer(Timer timer)
    {
        timer.timerIsRunning = false;
        timer.RestartTimer(5.0f);
    }
}
