using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private CharacterController characterController; // define object type
    public float speed = 1f;
    public float gravity = -1.81f;
    public float jump = 1f;
    public Transform groundTransform;
    public float groundDistance = 0.4f;

    public LayerMask GroundLayer;
    public LayerMask RayLayer;
    public Camera PlayerCamera;

    Vector3 velocity; // fall velocity
    bool isGrounded;
    private GameObject selectedObject; // for dragging obj?
   
    enum moveState
    {
    	Prone,
    	Walk,
    	Run,
        Jump
    }
    private moveState Movement;
    
    void Start()
    {
        characterController = GetComponent<CharacterController>(); // define component
    }

    // Update is called once per frame
    void Update()
    {
    	isGrounded = Physics.CheckSphere(groundTransform.position, groundDistance, GroundLayer); // make a sphere at target by offset distance, mask optional
    	if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            Movement = moveState.Run;
        }
        else if (isGrounded)
        {
            Movement = moveState.Walk;
        }
        // else
        // {
        //     Movement = moveState.Jump;
        // }

    	if (isGrounded && velocity.y < 0)
    	{
    		velocity.y = 0f;
    	}
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = ((transform.right * x + transform.forward * z) * Time.deltaTime); // it assumes that y is 0 as transform right and transform forward is x & z, adding them together would combine both vectors
        
        if (isGrounded && Input.GetKeyUp(KeyCode.Space)) 
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
            // velocity.x = move.x * speed * Mathf.Abs(gravity);
            // velocity.z = move.z * speed * Mathf.Abs(gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        if (True)//Movement != moveState.Jump)
        {
            characterController.Move(move * (Movement == moveState.Run ? speed*4 : speed)); // ternary
        } 
        characterController.Move(velocity);

        if (Input.GetKey(KeyCode.Space))
        {
            emitRay();
        }
    }

    void emitRay()
    {
        RaycastHit hit;
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, Mathf.Infinity))
        {
            Debug.Log(hit.point);
        }
    }
}
