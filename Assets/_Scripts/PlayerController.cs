using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f, gravity = -10f, playerViewDistance = 10f, runSpeed = 5f;
    public new Camera camera;
    public GameObject dotWhite, dotRed, flashLight, holdPoint;

    public AudioSource walkSound;
    public LayerMask layerMask;
    Animator animator;
    CharacterController controller;
    float mouseX, mouseY, rotationX;
    float mouseSensitivity = 300f;
    [HideInInspector]
    public Vector3 direction, velocity;
    StateMachine stateMachine;
    
    [HideInInspector]
    public IdleState idleState;
    [HideInInspector]
    public RunState runState;
    [HideInInspector]
    public WalkState walkState;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        stateMachine = new StateMachine();
        idleState = new IdleState(this, stateMachine, "Idle", animator);
        runState = new RunState(this, stateMachine, "Run", animator);
        walkState = new WalkState(this, stateMachine, "Walk", animator);

        stateMachine.Initialize(idleState);
        rotationX = 0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (GameManagerScript.activeEvent.id < 16) HandlePlayerMovement();

        HandleLookMovement();
        HandleRayCast();
        stateMachine.CurrentState.LogicUpdate();
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            GetComponent<AudioSource>().Play();
            flashLight.SetActive(!flashLight.activeSelf);
        }
    }

    void HandleLookMovement()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -75f, 75f);

        camera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandlePlayerMovement()
    { 
        direction = (transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical")).normalized;
        if (Input.GetKey(KeyCode.LeftShift)) velocity = new Vector3(direction.x * runSpeed, velocity.y, direction.z * runSpeed);
        else velocity = new Vector3(direction.x * speed, velocity.y, direction.z * speed);
        velocity.y = controller.isGrounded ? 0f : velocity.y + gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);        
    }

    // ray camera
    void HandleRayCast()
    {
        RaycastHit hit;
        if(Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, playerViewDistance, layerMask))
        {
            Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();

            if (interactable != null && interactable.canInteract)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0)) interactable.Interact();
                if (!dotRed.activeSelf)
                {
                    dotRed.SetActive(true);
                    dotWhite.SetActive(false);
                }

                return;
            }
        }

        if (!dotWhite.activeSelf)
        {
            dotRed.SetActive(false);
            dotWhite.SetActive(true);
        }
    }

    public void SetMouseSensitivity(float sliderValue)
    {
        mouseSensitivity = sliderValue;
    }

    void PlayWalkSound()
    {
        walkSound.pitch = Random.Range(.9f, 1.2f);
        walkSound.Play();
    }
}
