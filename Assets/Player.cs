using UnityEngine;


public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private CharacterController characterController; // define object type
    public float speed = 1f;
    public float gravity = -1.81f;
    public float jump = 1f;
    public int health = 100;

    public Transform groundTransform;
    public float groundDistance = 0.4f;
    public bool noclip = false; // not really no-clip
    public bool godmode = false; // use in conjunction of hp

    public LayerMask GroundLayer;
    public LayerMask RayLayer;
    public Camera PlayerCamera;
    public TextManager selection;


    Vector3 velocity; // fall velocity
    bool isGrounded; 
    public EventManager EventManager;

    enum playerTools {
        None = 0,
        Drag = 1,
        Spawn = 2,
        Delete = 3,
        Grab = 4
    }
    private playerTools tool = playerTools.None;
   
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
        selection.write(tool.ToString());
        if (!noclip) {
    	    isGrounded = Physics.CheckSphere(groundTransform.position, groundDistance, GroundLayer); // make a sphere at target by offset distance, mask optional
        }
        else
        {
            isGrounded = true;
        }
        if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            Movement = moveState.Run;
        }
        else if (isGrounded && !noclip)
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

        Vector3 move; // declaration
        if (!noclip) {
            move = ((transform.right * x + transform.forward * z) * Time.deltaTime); // it assumes that y is 0 as transform right and transform forward is x & z, adding them together would combine both vectors
        }
        else
        {
            move = ((PlayerCamera.transform.right * x + PlayerCamera.transform.forward * z) * Time.deltaTime);
        }

        if (!noclip) {
            if (isGrounded && Input.GetKeyUp(KeyCode.Space)) 
            {
                velocity.y = Mathf.Sqrt(jump * -2f * gravity);
                // velocity.x = move.x * speed * Mathf.Abs(gravity);
                // velocity.z = move.z * speed * Mathf.Abs(gravity);
            }
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = Input.GetKeyUp(KeyCode.Space) ? 1f * speed * Time.deltaTime : 0f;
        }

        characterController.Move(move * (Movement == moveState.Run ? speed*4 : speed)); // ternary
        characterController.Move(velocity);

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            nextTool();
            //instantiateObject();
            // #nullable disable
            // RaycastHit spawnLocation = emitRay();
            // if (spawnLocation != null) {
            //     instantiateObject(prefabObject, spawnLocation.point);
            // }
            // #nullable enable
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            selectTool();
        }


    }

    void nextTool() // streamline between tools
    {
        selection.visible(false, 1.0f);
        EventManager.resetTools(); // switcharoo
        switch (tool)
        {
            case playerTools.None:
                tool = playerTools.Drag;
                Debug.Log("drag");
                break;
            case playerTools.Drag:
                tool = playerTools.Spawn;
                Debug.Log("spawn");
                break;
            case playerTools.Spawn:
                tool = playerTools.Delete;
                Debug.Log("delete");
                break;
            case playerTools.Delete:
                tool = playerTools.Grab;
                Debug.Log("take");
                break;
            case playerTools.Grab:
                tool = playerTools.None;
                Debug.Log("nome");
                break;
            default:
                Debug.Log("Unknown");
                break;
        }
    }

    void selectTool() // OR use tool
    {
        switch (tool)
        {
            case playerTools.None:
                break;
            case playerTools.Drag:
                EventManager.execute(1);
                break;
            case playerTools.Spawn:
                EventManager.execute(2);
                break;
            case playerTools.Delete:
                EventManager.execute(3);
                break;
            case playerTools.Grab:
                EventManager.execute(4);
                break;
            
        }
    }
    // void instantiateObject(float _range = Mathf.Infinity)
    // {
    //     RaycastHit hit;
    //     if(Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, _range))
    //     {
    //         Instantiate(prefabObject, hit.point, PlayerCamera.transform.rotation);
    //     }
    // }



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
