using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private CharacterController characterController; // define object type
    public float speed = 1f;
    public float gravity = -1.81f;
    public float jump = 1f;
    public float forceMultiplier = 10f; // for dragging objects
    public Transform groundTransform;
    public float groundDistance = 0.4f;

    public LayerMask GroundLayer;
    public LayerMask RayLayer;
    public Camera PlayerCamera;
    public GameObject Axis;

    Vector3 velocity; // fall velocity
    bool isGrounded;
    private Rigidbody selectedObject; // for dragging obj?

    public GameObject prefabObject;
   
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
        if (true)//Movement != moveState.Jump)
        {
            characterController.Move(move * (Movement == moveState.Run ? speed*4 : speed)); // ternary
        } 
        characterController.Move(velocity);

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            instantiateObject();
            // #nullable disable
            // RaycastHit spawnLocation = emitRay();
            // if (spawnLocation != null) {
            //     instantiateObject(prefabObject, spawnLocation.point);
            // }
            // #nullable enable
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            dragObject();
        }

        if (selectedObject != null)
        {
            float forceStrength = Vector3.Distance(Axis.transform.position, selectedObject.transform.position); //Mathf.Atan2(Axis.transform.position.x - selectedObject.transform.position.x, Axis.transform.position.y - selectedObject.transform.position.y);
            Vector3 resultantForce = ((Axis.transform.position - selectedObject.transform.position).normalized * forceStrength) * forceMultiplier;
            selectedObject.AddForce(resultantForce);
        }
    }

    void instantiateObject(float _range = Mathf.Infinity)
    {
        RaycastHit hit;
        if(Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, _range))
        {
            Instantiate(prefabObject, hit.point, PlayerCamera.transform.rotation);
        }
    }

    void dragObject(float _range = Mathf.Infinity)
    {
        RaycastHit hit;
        if(Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, _range))
        {
            if (hit.rigidbody)
            {
                selectedObject = hit.rigidbody;
                Debug.Log(selectedObject.GetComponent<Rigidbody>());
                return;
            }
        }
        selectedObject = null;
    }

    // RaycastHit emitRay(float _range = Mathf.Infinity)
    // {
    //     RaycastHit hit;
    //     if(!Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, _range)) {
    //         return null;
    //     }
    //     return hit;
    //         //Instantiate(prefabObject, hit.point, PlayerCamera.transform.rotation);
    // }

    // void instantiateObject(GameObject _object, Vector3 _origin, Quaternion _rotation)
    // {
    //     if (_origin != null && _object != null)
    //     {
    //         Instantiate(_object, _origin, _rotation);
    //     }
    // }

    // void instantiateObject(GameObject _object, Vector3 _origin)
    // {
    //     if (_origin != null && _object != null)
    //     {
    //         Instantiate(_object, _origin, PlayerCamera.transform.rotation);
    //     }
    // }
}
