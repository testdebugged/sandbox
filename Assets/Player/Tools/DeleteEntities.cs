using UnityEngine;

public class DeleteEntities : MonoBehaviour
{
    public Camera PlayerCamera;
    public LayerMask RayLayer;
    bool _enabled = false; // eventmanager
    public void mouseClicked()
    {
        _enabled = true;
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
        _enabled = false;
    }
}
