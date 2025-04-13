using UnityEngine;

public class DeleteEntities : MonoBehaviour
{
    public Camera PlayerCamera;

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
        if(Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, _range))
        {
            Destroy(hit.collider.gameObject);
        }
    }
    public void reset() {
        Debug.Log("res");
    }
}
