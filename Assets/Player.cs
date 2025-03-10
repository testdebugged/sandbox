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
    
    Vector3 velocity; // fall velocity
    bool isGrounded;
    void Start()
    {
        characterController = GetComponent<CharacterController>(); // define component
    }

    // Update is called once per frame
    void Update()
    {
    	isGrounded = Physics.CheckSphere(groundTransform.position, groundDistance, Ground); // make a sphere at target by offset distance, mask optional
    	if (isGrounded && velocity.y < 0)
    	{
    		Debug.Log(isGrounded);
    		velocity.y = -2f;
    	}
    	
	if (isGrounded && Input.GetKeyUp(KeyCode.Space))
	{
		velocity.y = Mathf.Sqrt(jump * -2f * gravity);
	}
	
    	float x = Input.GetAxis("Horizontal");
    	float z = Input.GetAxis("Vertical");
        Vector3 move = ((transform.right * x + transform.forward * z) * Time.deltaTime); // it assumes that y is 0 as transform right and transform forward is x & z, adding them together would combine both vectors
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(move * speed);
        characterController.Move(velocity);
    }
}
