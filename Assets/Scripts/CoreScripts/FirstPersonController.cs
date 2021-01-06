using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour
{

    public InventoryObject inventory;

    public Transform cameraTransform;
    public CharacterController characterController;
    Camera cam;

    [Header("Movement")]

  
    public float moveSpeed;
    public float moveInputDeadZone;

    Vector2 moveTouchStartPoint;
    Vector2 moveInput;
    Vector2 beginTouchPosition;
    Vector2 endTouchPosition;


    [Header("Camera")]

    Vector2 lookInput;
    float cameraPitch;
    public Slider sensSlider;
    public float cameraSensitivity;
    



    int leftFingerID, rightFingerID;
    float halfScreenWidth;

    [Header("Gravity & Jumping")]
    
    public float stickToGroundForce = 10;
    public float gravity = 10;
    public float verticalVelocity;

    [Header("Ground check")]

    public Transform groundCheck;
    public LayerMask groundLayers;
    public float groundCheckRadius;
    private bool isOnGround;

    public Interactable focus;
 



    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        leftFingerID = -1;
        rightFingerID = -1;
        halfScreenWidth = Screen.width / 2;
        
    }
    private void OnEnable()
    {
        lookInput = Vector2.zero;
        moveInput = Vector2.zero;
        leftFingerID = -1;
        rightFingerID = -1;
    }



    // Update is called once per frame
    void Update()
    {
        cameraSensitivity = sensSlider.value;
        GetTouchInput();
        JumpControl();
        if (rightFingerID != -1)
        {
            LookAround();
        }
        if (leftFingerID != -1)
        {
            Move();
        }

       
        

    }

        public void GetTouchInput () 
        { 
        for (int i = 0; i < Input.touchCount; i++)
            {
                Touch t = Input.GetTouch(i);
                switch (t.phase)
                {
                    case TouchPhase.Began:
                    

                        if (t.position.x < halfScreenWidth && leftFingerID == -1)

                        {
                            leftFingerID = t.fingerId;
                          
                            moveTouchStartPoint = t.position;

                        }
                        else if (t.position.x > halfScreenWidth && rightFingerID == -1)
                        {
                            rightFingerID = t.fingerId;
                            
                        }
                    beginTouchPosition = t.position;

                    break;
                    case TouchPhase.Ended:
                                            
                    case TouchPhase.Canceled:

                    if (t.fingerId == rightFingerID)
                    {
                        rightFingerID = -1;
                        lookInput = Vector2.zero;
                    }
                    else if (t.fingerId == leftFingerID)
                    {
                        leftFingerID = -1;
                        moveInput = Vector2.zero;
                    }
                    endTouchPosition = t.position;

                    if (beginTouchPosition == endTouchPosition)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                        RaycastHit hit;


                        if (Physics.Raycast(ray, out hit, 100))
                        {
                            Interactable interact = hit.collider.GetComponent<Interactable>();
                            if (interact)
                            {
                                float rad = interact.radius;
                                Transform intTrans = interact.interactableTransform;
                                float dist = Vector3.Distance(cameraTransform.position, intTrans.position);
                                if (hit.collider != null && interact && dist <= rad)
                                {
                                    SetFocus(interact);
                                }
                            }
                        }
                        
                    }

                    
                    break;
                    case TouchPhase.Moved:
                    if (t.fingerId == rightFingerID)
                    {   
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }
                    else if (t.fingerId == leftFingerID)
                    {
                        moveInput = t.position - moveTouchStartPoint;    
                    }
                    break;

                    

                    case TouchPhase.Stationary:
                    if (t.fingerId == rightFingerID)
                    {
                        lookInput = Vector2.zero;
                    }
                   

                    break;
                    
                }




            }

        }

    void LookAround()
    {
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        transform.Rotate(transform.up, lookInput.x);
    }
    void Move()
    {
        //XZ movement (Horizontal)
        if (moveInput.sqrMagnitude <= moveInputDeadZone) return;

        Vector2 movementDirection = moveInput.normalized * moveSpeed * Time.deltaTime;

        characterController.Move(transform.right * movementDirection.x + transform.forward * movementDirection.y);
        
    }
    void JumpControl()
    {
        // Y movement (Vertical)

        if (isOnGround && verticalVelocity <= 0) { verticalVelocity = -stickToGroundForce * Time.deltaTime; }

        else verticalVelocity -= gravity * Time.deltaTime;

        Vector3 verticalMovement = transform.up * verticalVelocity;
        characterController.Move(verticalMovement * Time.deltaTime);
    }
    void FixedUpdate()
    {
      isOnGround = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayers);
     }

   

    void SetFocus(Interactable newFocus)
    {
        if (newFocus != focus)
        {
            if (focus != null)
                focus.OnDefocused();
            focus = newFocus;
        }
        newFocus.OnFocused(transform);
    }

    void RemoveFocus()
    {
        if (focus != null)
            focus.OnDefocused();
        focus = null;
        
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[25];
     }



}

