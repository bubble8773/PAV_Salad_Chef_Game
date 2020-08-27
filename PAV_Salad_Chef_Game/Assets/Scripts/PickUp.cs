using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public string pickUpName;
    //public string pickUpTag;
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
        Debug.Log("entered common pickup");
        //Debug.Log(other.name);
        //Debug.Log(this.name);
        //Debug.Log(this.tag);
        pickUpName = this.name;
        //pickUpTag = this.tag;
        if (this.tag == "Ingredients")
        {
            other.GetComponent<PlayerController>().canPickUpIng = true;
        }

        if (this.tag == "Orders")
        {
            other.GetComponent<PlayerController>().canPickOrder = true;
        }

        if (this.tag == "ChoppingBoards")
        {
            other.GetComponent<PlayerController>().canChop = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        pickUpName = "";
        if (this.tag == "Ingredients")
        {
            other.GetComponent<PlayerController>().canPickUpIng = false;
        }

        if (this.tag == "Orders")
        {
            other.GetComponent<PlayerController>().canPickOrder = false;
        }

        if (this.tag == "ChoppingBoards")
        {
            other.GetComponent<PlayerController>().canChop = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("collided with " + this.name);
        //pickUpName = this.name;
    }
}
