using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderPickUp : MonoBehaviour
{
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
        Debug.Log("enterd order pickup zone");
        player = other.GetComponent<PlayerController>();
        player.canPickOrder = true;
        
    }

    private void OnTriggerExit(Collider other)
    {
        // PlayerController._instance.canPickOrder = false;
        player = other.GetComponent<PlayerController>();
        player.canPickOrder = false;
    }

}
