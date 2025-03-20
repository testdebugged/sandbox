using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public bool cursorLockState = true; 
    float xRotation = 0f;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        Cursor.lockState = cursorLockState ? CursorLockMode.None : CursorLockMode.Locked;
        playerBody.Rotate(Vector3.up * mouseX); 
    }
}
