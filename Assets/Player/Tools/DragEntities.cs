using UnityEngine;

public class DragEntities : MonoBehaviour
{
    public GameObject Player;
    public Camera PlayerCamera;
    Rigidbody selectedObject;

    public float forceMultiplier = 10f; // for dragging objects
    // Update is called once per frame

    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.3f;
    }
    
    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        if (selectedObject != null)
        {
            lineRenderer.enabled = true;
            float forceStrength = Vector3.Distance(this.transform.position, selectedObject.transform.position); //Mathf.Atan2(Axis.transform.position.x - selectedObject.transform.position.x, Axis.transform.position.y - selectedObject.transform.position.y);
            Vector3 resultantForce = ((this.transform.position - selectedObject.transform.position).normalized * forceStrength) * forceMultiplier;
            selectedObject.AddForce(resultantForce);
            lineRenderer.SetPosition(0, this.transform.position);
            lineRenderer.SetPosition(1, selectedObject.transform.position);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    public void mouseClicked()
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
        Debug.Log("unselect");
        selectedObject = null;
    }

    public void reset()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        selectedObject = null;
    }
}
