using UnityEngine;

public class DeleteEntities : MonoBehaviour
{
    public Camera PlayerCamera;
    public LayerMask RayLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void mouseClicked()
    {
        deleteObject();
    }
    void deleteObject(float _range = Mathf.Infinity)
    {
        RaycastHit hit;
        if(Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, _range, RayLayer))
        {
            if (hit.collider.gameObject.GetComponent<Rigidbody>() != null) {
                Destroy(hit.collider.gameObject);
            }
        }
        return;
    }
    public void reset() {
        Debug.Log("res");
    }
}
