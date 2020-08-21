using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Key Inputs name which will be varying depends on the player 
    public string horizontalKey = "Horizontal";
    public string verticalKey = "Vertical";

    // The speed at which the player moves
    CharacterController player;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis(horizontalKey); // -1 to 1  
        float vertical = Input.GetAxis(verticalKey);

        Vector3 moveDir = transform.right * horizontal + transform.forward * vertical;

        player.Move(moveDir * speed * Time.deltaTime);


    }
}
