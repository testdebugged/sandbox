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
    public LayerMask Ground;
    public Rigidbody objectToSpawn; // Placeholder
    Vector3 velocity; // fall velocity
    bool isGrounded;
   
    enum moveState
    {
    	Prone,
    	Walk,
    	Run
    }
    private moveState Movement;
    
    void Start()
    {
        characterController = GetComponent<CharacterController>(); // define component
    }

    // Update is called once per frame
    void Update()
    {
    	isGrounded = Physics.CheckSphere(groundTransform.position, groundDistance, Ground); // make a sphere at target by offset distance, mask optional
    	if (Input.GetKey(KeyCode.LeftShift))
        {
            Movement = moveState.Run;
        }
        else
        {
            Movement = moveState.Walk;
        }

    	if (isGrounded && velocity.y < 0)
    	{
    		velocity.y = 0f;
    	}
    	
	if (isGrounded && Input.GetKeyUp(KeyCode.Space))
	{
		velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        Rigidbody spawnedObject;
        spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation) as Rigidbody;
	}
	
    	float x = Input.GetAxis("Horizontal");
    	float z = Input.GetAxis("Vertical");
        Vector3 move = ((transform.right * x + transform.forward * z) * Time.deltaTime); // it assumes that y is 0 as transform right and transform forward is x & z, adding them together would combine both vectors
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(move * (Movement == moveState.Walk ? speed : speed*4));
        characterController.Move(velocity);
    }
}
