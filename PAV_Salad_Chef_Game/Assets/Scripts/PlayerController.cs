using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    // Key Inputs name which will be varying depends on the player 
    public string horizontalKey = "Horizontal";
    public string verticalKey = "Vertical";

    //// Input keys
    //public KeyCode pickupKey;
    //public KeyCode placeKey;
    //public KeyCode chopKey;

    public bool canPickOrder = false;//taken order from customer
    public bool hasCustomer = false;// a customer is assigned 
    public bool canPickUpIng = false;// to pick ingredients
    public bool isReadyToChop = false; // acquired correct ingredients and can go towards the chopping board
    public bool canChop = false; // now player is allowed to chop as they are in chopping zome
    public bool canMove = false;//stop moving while chopping and pickingIng
    public bool isSaladReady = false;


    // The speed at which the player moves
    CharacterController player;
    public float speed;

    [SerializeField]
    List<string> pickedIng = new List<string>();
    public static PlayerController _instance;
    public Transform customerAssigned;
    Orders ordersRecived;
    //Player is in Zones
    
    public Chopping chopping;

    [SerializeField]
    IngredientsPickUp ingredientsPickedUp;

    public GameObject orderHolder;

    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;

    }
    void Start()
    {
        player = GetComponent<CharacterController>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis(horizontalKey); // -1 to 1  
        float vertical = Input.GetAxis(verticalKey);

        Vector3 moveDir = transform.right * horizontal + transform.forward * vertical;

        if(canMove == true)
            player.Move(moveDir * speed * Time.deltaTime);

        RayCastUpdate();

    }
    
    void RayCastUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (this.hasCustomer == true)
            {
                if (canPickUpIng == true)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.collider.tag == "Ingredients")
                        {
                            Debug.Log(this.name + " has picked Up " + hit.collider.name);
                            pickedIng.Add(hit.collider.name);
                        }

                    }
                    isReadyToChop = ingredientsPickedUp.GetCorectIng(customerAssigned,
                         pickedIng);
                    Debug.Log(isReadyToChop);
                }
            }

            if (canPickOrder == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.tag == "Orders")
                    {
                        customerAssigned = hit.transform.parent;
                        Debug.Log(this.name + " has taken order from " + customerAssigned.name);
                        hasCustomer = true;
                        ordersRecived = customerAssigned.GetComponent<Orders>();

                        for (int i = 0; i <= ordersRecived.saladCombo.Count-1; i++)
                        {
                            ordersRecived.CreateSpritesForOrder(i, ordersRecived.saladCombo[i], 
                                orderHolder.transform);
                        }
                        // lock orders
                         hit.transform.gameObject.SetActive(false);
                         
                    }


                }
                // Debug.Log(hit.collider.name);
            }

            if (canChop == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (hit.collider.tag == "ChoppingBoards")
                    {
                        Debug.Log(this.name + "is chopping on " +hit.collider.name);
                        chopping.choppingBoardOccupied = hit.collider.gameObject;
                        chopping.StartChopping(isReadyToChop, pickedIng.Count,
                            chopping.choppingBoardOccupied.GetComponent<Timer>());
                                        
                    }

                }
                if (this.chopping.choppingBoardOccupied != null)
                {
                    if (this.chopping.choppingBoardOccupied.GetComponent<Timer>().timerIsRunning)
                    {
                        Debug.Log(this.name + "Cant Move");
                        this.canMove = false;
                    }
                    else
                    {
                        this.canMove = true;
                        Debug.Log(this.name + "can move and time to deliver");
                        isSaladReady = true;
                    }
                }
            }
           
            if (isSaladReady == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (this.hasCustomer == true)
                    {
                        Debug.Log(customerAssigned.name);
                        if (hit.collider.name == customerAssigned.name)
                        {
                            ordersRecived.saladRecived = true;
                            
                        }
                        if (ordersRecived.saladRecived == true)
                        {
                            ordersRecived.StopTimer(ordersRecived.timer);
                            GameManager._instance.UpdateScores(ordersRecived.scoreForDelivery, this);
                        }
                    }
                }

            }
            
        }
  
    }

    
}
