using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
public class Orders : OrderSpriteCreator
{
    public List<Ingredients> saladCombo = new List<Ingredients>();
    [SerializeField]
    GameObject ingObjs;
    
    // Start is called before the first frame update
    void Start()
    {
        
       StartCoroutine( CreateRandomSalads(2));
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

            CreateSpritesForOrder(i, saladCombo[i], gameObject.transform.GetChild(0));
            //GameObject item = Instantiate(Resources.Load<GameObject>("Prefabs/" + saladCombo[i].ToString()));
            //item.transform.SetParent(gameObject.transform.GetChild(0));
            //item.transform.localPosition = new Vector3(xPos, 0, 0);
            //xPos++;
        }
       
        yield return new WaitForSecondsRealtime(2.0f);

       // Debug.Log(this.name +" Ordering.." + saladCombo[0].ToString() + " " + saladCombo[1].ToString());


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
    // Update is called once per frame
    void Update()
    {
        
    }
}
