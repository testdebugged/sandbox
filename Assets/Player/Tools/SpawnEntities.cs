using UnityEngine;

public class SpawnEntities : MonoBehaviour
{
    public Camera PlayerCamera;
    public DropManager prefabs;
    public GameObject prefabObject;

    void Update() {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            int scrollDelta = Mathf.RoundToInt((Input.GetAxis("Mouse ScrollWheel")*10)); // default scroll is 0.1
            assignPrefab(scrollDelta); // should be -ve or +ve 
        }
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
            GameObject spawnedObj = Instantiate(prefabObject, hit.point, Quaternion.Euler(0,PlayerCamera.transform.rotation.y,0));
            Renderer objRenderer = spawnedObj.GetComponent<Renderer>();
            objRenderer.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 1f);
            // MaterialPropertyBlock materialProperty = new MaterialPropertyBlock();
            // materialProperty.SetColor("_Color", Random.ColorHSV()); // randomized color
            // spawnedObj.GetComponent<Renderer>().SetPropertyBlock(materialProperty);
        }
    }

    public void reset() {
        Debug.Log("res");
    }

    void assignPrefab(int select) {
        prefabObject = (prefabs.selectPrefab(select));
    }
}
