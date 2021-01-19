using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Key Inputs name which will be varying depends on the player 
    public string horizontalKey = "Horizontal";
    public string verticalKey = "Vertical";

    
    public bool canPickOrder = false;//taken order from customer
    public bool hasCustomer = false;// a customer is assigned 
    public bool canPickUpIng = false;// to pick ingredients
    public bool isReadyToChop = false; // acquired correct ingredients and can go towards the chopping board
    public bool canChop = false; // now player is allowed to chop as they are in chopping zome
    public bool canMove = false;//stop moving while chopping and pickingIng
    public bool isSaladReady = false; // Done Chopping
    
    [SerializeField]
    List<string> pickedIng = new List<string>();
    
    public Transform customerAssigned;
    Orders ordersRecived;
    //Player is in Zones

    public Chopping chopping;

    [SerializeField]
    IngredientsPickUp ingredientsPickedUp;

    public GameObject orderHolder;
    public TextMeshPro textMeshPro;
    public PickUp[] pickupObjects;

    public static PlayerController _instance;
    // Start is called before the first frame update
    public string message = "";
    public float speed;
    int keyCount;
    

    public CharacterController player;
    private void Awake()
    {
        _instance = this;
        pickupObjects = GameObject.FindObjectsOfType<PickUp>();
        
    }
    void Start()
    {
        player = GetComponent<CharacterController>();
        canMove = true;
        message = "Take orders";

        keyCount = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis(horizontalKey); // -1 to 1  
        float vertical = Input.GetAxis(verticalKey);

        Vector3 moveDir = transform.right * horizontal + transform.forward * vertical;

        if (canMove == true)
            player.Move(moveDir * speed * Time.deltaTime);

        if (GameManager._instance.useMouse == true)
        {
            RayCastUpdate();
        }

      if (ordersRecived != null)
        {
            if (ordersRecived.isHappy == true)
            {
                if (GameManager._instance.bonusObjs[0].activeInHierarchy == true)
                {
                    if (GameManager._instance.bonusObjs[0].name.Contains("Speed"))
                    {
                        //check for player collision
                        //GameManager._instance.AddSpeed(this, 10.0f);
                    }
                    if (GameManager._instance.bonusObjs[0].name.Contains("Time"))
                    {
                        //check for player collision
                        //GameManager._instance.AddTime(this, 10.0f);
                    }
                }
            }
        }
    }

    void RayCastUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
           // Debug.Log(hit.collider.name);
            if (canPickOrder == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    textMeshPro.text = "";
                    if (hit.collider.tag == "Orders")
                    {
                        customerAssigned = hit.transform.parent;
                        //Debug.Log(this.name + " has taken order from " + customerAssigned.name);
                        hasCustomer = true;
                        ordersRecived = customerAssigned.GetComponent<Orders>();
                        textMeshPro.text = "Taking Orders from " + customerAssigned.name;
                        for (int i = 0; i <= ordersRecived.saladCombo.Count - 1; i++)
                        {
                            ordersRecived.CreateSpritesForOrder(i, ordersRecived.saladCombo[i],
                                orderHolder.transform);
                            //ordersRecived.ChangeSprites(orderHolder.transform.GetChild(i).GetComponent<SpriteRenderer>(),
                            //    ordersRecived.orderHolder.transform.GetChild(i).GetComponent<SpriteRenderer>());
                        }
                        // lock orders
                        hit.transform.gameObject.SetActive(false);
                        textMeshPro.text = "";
                    }

                }
                // Debug.Log(hit.collider.name);
            }

            if (this.hasCustomer == true)
            {
                if (canPickUpIng == true)
                {
                    textMeshPro.text = "Pick Ingredients";
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.collider.tag == "Ingredients")
                        {
                            Debug.Log(this.name + " has picked Up " + hit.collider.name);
                            if (pickedIng.Count < ordersRecived.saladCombo.Count)
                                pickedIng.Add(hit.collider.name);
                            textMeshPro.text = "Picking " + hit.collider.name;
                        }
                        
                    }
                    isReadyToChop = ingredientsPickedUp.GetCorectIng(customerAssigned,
                         pickedIng);
                    //Debug.Log(isReadyToChop);
                    textMeshPro.text = "";
                }
            }

            if (hasCustomer == true)
            {
                if (isReadyToChop == true)
                {
                    if (canChop == true)
                    {
                        textMeshPro.text = "Start Chopping";
                        if (Input.GetMouseButtonDown(0))
                        {
                            textMeshPro.text = "";
                            if (hit.collider.tag == "ChoppingBoards")
                            {
                                Debug.Log(this.name + "is chopping on " + hit.collider.name);
                                textMeshPro.text = "Chopping" + pickedIng[0] + " then" + pickedIng[1];
                                chopping.choppingBoardOccupied = hit.collider.gameObject;
                                chopping.StartChopping(chopping.choppingBoardOccupied.GetComponent<Timer>());

                            }

                        }
                        if (this.chopping.choppingBoardOccupied != null)
                        {
                            if (this.chopping.choppingBoardOccupied.GetComponent<Timer>().timerIsRunning)
                            {
                                // Debug.Log(this.name + "Cant Move");
                                this.canMove = false;
                            }
                            else
                            {
                                this.canMove = true;
                                textMeshPro.text = "Done Chopping. Deliver it to " + customerAssigned.name;
                                isSaladReady = true;
                            }
                        }
                    }
                }
                else
                {    // trash Ingredients         
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (hit.collider.tag == "TrashCan")
                        {   //Debug.Log("Pick the correct Ingredients");
                            textMeshPro.text = "Pick the correct Ingredients";
                            pickedIng.Clear();
                        }
                    }

                }
            }
            if (isSaladReady == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    textMeshPro.text = "Time to deliver to " + customerAssigned.name;
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
                            pickedIng.Clear();
                            ordersRecived.saladRecived = false;
                            textMeshPro.text = "Done";
                        }
                        textMeshPro.text = message;
                    }
                }

            }

        }
     
    }

}
