using UnityEngine;

public class SpawnEntities : MonoBehaviour
{
        public Camera PlayerCamera;
        public GameObject prefabObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void mouseClicked()
    {
        instantiateObject();
    }
    void instantiateObject(float _range = Mathf.Infinity)
    {
        RaycastHit hit;
        if(Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, _range))
        {
            Instantiate(prefabObject, hit.point, PlayerCamera.transform.rotation);
        }
        Debug.Log(hit);
    }
    public void reset() {
        Debug.Log("res");
    }
}
