using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Ingredients
{
    Chicken, 
    Shrimp,
    Fish,
    Onion,
    Avocado,
    Pepper,
    Lettuce
}
public class Orders : CommonAbstract
{
    public List<Ingredients> saladCombo = new List<Ingredients>();
    public GameObject orderHolder; // holds sprites
    public GameObject sliderHolder; // to hold slider
    public bool saladRecived;
    public int scoreForDelivery = 10;
    int maxIngs = 2;    
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine( CreateRandomSalads(maxIngs));
       CustomerWaiting(true,  maxIngs);
        saladRecived = false;
    }

    IEnumerator CreateRandomSalads( int count)
    {
        int index = 0;
        for (int i = 0; i <= count - 1; i++)
        {
            var info = System.Enum.GetValues(typeof(Ingredients));
            //Debug.Log(info.Length - 1);
            index = Random.Range(0, info.Length - 1);
            //Debug.Log(index);
            if (!saladCombo.Contains((Ingredients)info.GetValue(index)))
            {
                saladCombo.Add((Ingredients)info.GetValue(index));
            }
            else
            {
                if (index > 0 && index ==6)
                    index--;
                else if (index == 0)
                    index++;

                saladCombo.Add((Ingredients)info.GetValue(index + 1));
            }

            CreateSpritesForOrder(i, saladCombo[i], orderHolder.transform);
            
        }
       
        yield return new WaitForSecondsRealtime(2.0f);

    }

    public bool GetCorectIng(Transform customer, List<string> pickedIng)
    {
        bool result = false;
        List<string> correctIngs = new List<string>();

           for (int i = 0; i <= pickedIng.Count - 1; i++)
            {
                if (pickedIng.Contains(
                    customer.GetComponent<Orders>().saladCombo[i].ToString()))
                {
                    Debug.Log("comparing ing picked vs ing ordered");
                    correctIngs.Add(pickedIng[i]);
                }
                else
                {
                    result = false;
                }
            }
        
        if (correctIngs.Count == pickedIng.Count)
        {
            result = true;
        }
        
        Debug.Log(result);
        return result;

    }

    public void CustomerWaiting(bool hasPlacedOrder, int num)
    {
        StartTimer(hasPlacedOrder, 1, this.timer, this.slider);
        
        if (this.timer.waitingTime == 0)
        {
            Debug.Log("Done Waiting");
            
            //else reduce points for players

        }
    }
    // Update is called once per frame
    void Update()
    {
        GameManager._instance.ClampUIPositions(this.sliderHolder.transform.position, this.slider);
    }
}
