using UnityEngine;

public class TakeEntities : MonoBehaviour
{
    public GameObject Player;
    public Camera PlayerCamera;
    bool _enabled = false;
    Rigidbody selectedObject;

    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.3f;
    }
    
    void Update()
    {
        if (enabled) {
            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            if (selectedObject != null)
            {
                lineRenderer.enabled = true;

                selectedObject.transform.position = this.transform.position;
                selectedObject.linearVelocity = new Vector3(0,0,0);
                lineRenderer.SetPosition(0, this.transform.position);
                lineRenderer.SetPosition(1, selectedObject.transform.position);
            }
            else
            {
                lineRenderer.enabled = false;
            }
        }
    }

    public void mouseClicked()
    {
        _enabled = true;
        dragObject();
    }    
    void dragObject(float _range = Mathf.Infinity)
    {
        RaycastHit hit;
        if(Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, _range))
        {
            if (hit.rigidbody)
            {
                if (selectedObject != null) {
                    if (hit.rigidbody == selectedObject.GetComponent<Rigidbody>())
                    {
                        selectedObject = null;
                        return;
                    }
                }
                selectedObject = hit.rigidbody;
                Debug.Log(selectedObject.GetComponent<Rigidbody>());
                return;
            }
        }
    }

    public void reset()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        selectedObject = null;
        _enabled = false;
    }
}
