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
    public TextMeshPro textMeshPro;
    public bool saladRecived;
    public bool isAngry = false;
    public bool isHappy = false;
    public int scoreForDelivery = 10;
    public int maxIngs = 2;

    public GameObject ingObjs;    
    // Start is called before the first frame update
    public Timer timer;
    void Start()
    {
        orderHolder = gameObject.transform.GetChild(0).gameObject;
        sliderHolder = gameObject.transform.GetChild(1).gameObject;
        textMeshPro = gameObject.transform.GetChild(2).GetComponent<TextMeshPro>();

        timer = GetComponent<Timer>();
        saladRecived = false;
        StartCreatingRandomOrders(maxIngs);
        CustomerWaiting(true, maxIngs);
    }
    private void OnEnable()
    {
        
    }
    public void StartCreatingRandomOrders(int count)
    {
        StartCoroutine(CreateRandomSalads(maxIngs));
    }
    public void StartCreatingNextRandomOrders(int count)
    {
        StartCoroutine(CreateNextRandomSalads(maxIngs));
    }

    IEnumerator CreateRandomSalads( int count)
    {
        //int index = 0;
        for (int i = 0; i <= count - 1; i++)
        {
            var info = System.Enum.GetValues(typeof(Ingredients));
            //Debug.Log(info.Length - 1);
            int index = Random.Range(0, info.Length - 1);
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

    IEnumerator CreateNextRandomSalads(int count)
    {
        for (int i = 0; i <= count - 1; i++)
        {
            var info = System.Enum.GetValues(typeof(Ingredients));
            //Debug.Log(info.Length - 1);
            int index = Random.Range(0, info.Length - 1);
            //Debug.Log(index);
            if (!saladCombo.Contains((Ingredients)info.GetValue(index)))
            {
                saladCombo.Add((Ingredients)info.GetValue(index));
            }
            else
            {
                if (index > 0 && index == 6)
                    index--;
                else if (index == 0)
                    index++;

                saladCombo.Add((Ingredients)info.GetValue(index + 1));
            }

        }

        for (int j = 0; j <= ingObjs.transform.childCount - 1; j++) {

            for (int k = 0; k <= count - 1; k++)
            {
                if (ingObjs.transform.GetChild(j).name.Contains(saladCombo[k].ToString()))
                {
                    ChangeSprites(orderHolder.transform.GetChild(k).GetComponent<SpriteRenderer>()
                        , ingObjs.transform.GetChild(j).GetComponent<SpriteRenderer>());
                }
            }
        }
        yield return new WaitForSecondsRealtime(2.0f);

    }

    public void CustomerWaiting(bool hasPlacedOrder, int num)
    {
        textMeshPro.text = "Waiting";
      
    }

    void Update()
    {
        if (this.timer.slider != null)
        {
           GameManager._instance.ClampUIPositions(this.sliderHolder.transform.position, this.timer.slider);
            
        }
       
    }

}
