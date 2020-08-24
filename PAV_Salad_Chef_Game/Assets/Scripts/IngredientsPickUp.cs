using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsPickUp : MonoBehaviour
{
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
        Debug.Log("entered ingredient pickup zone");
        //PlayerController._instance.canPickUp = true;
        other.GetComponent<PlayerController>().canPickUp = true;
    }

    private void OnTriggerExit(Collider other)
    {
        // PlayerController._instance.canPickUp = false;
        other.GetComponent<PlayerController>().canPickUp = false;
    }

    public bool GetCorectIng(Transform customer,  List<string> pickedIng)
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
}
