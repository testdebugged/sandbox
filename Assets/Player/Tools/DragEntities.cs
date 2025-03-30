using UnityEngine;

public class DragEntities : MonoBehaviour
{
    public GameObject Player;
    public Camera PlayerCamera;
    Rigidbody selectedObject;
    public LayerMask RayLayer;
    public float forceMultiplier = 10f; // for dragging objects
    // Update is called once per frame

    
    void Update()
    {
        if (selectedObject != null)
        {
            float forceStrength = Vector3.Distance(transform.position, selectedObject.transform.position); //Mathf.Atan2(Axis.transform.position.x - selectedObject.transform.position.x, Axis.transform.position.y - selectedObject.transform.position.y);
            Vector3 resultantForce = ((transform.position - selectedObject.transform.position).normalized * forceStrength) * forceMultiplier;
            selectedObject.AddForce(resultantForce);
        }
    }

    void mouseClicked()
    {
        dragObject();
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
}
