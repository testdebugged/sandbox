using System.Collections;
using UnityEngine;

public class SpawnEntities : MonoBehaviour
{
    public Camera PlayerCamera;
    public DropManager prefabs;
    public GameObject prefabObject;
    bool _enabled = false;
    public TextManager selection;


    void Update() {
        if (enabled) {
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                int scrollDelta = Mathf.RoundToInt((Input.GetAxis("Mouse ScrollWheel")*10)); // default scroll is 0.1
                assignPrefab(scrollDelta); // should be -ve or +ve 
            }
            selection.write(prefabObject.name);
        }
    }
    public void mouseClicked()
    {
        _enabled = true;
        instantiateObject();
    }
    void instantiateObject(float _range = Mathf.Infinity)
    {
        RaycastHit hit;
        if(Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, _range))
        {
            Vector3 prefSize = prefabObject.GetComponent<MeshRenderer>().bounds.size; // get size to calculate pos
            GameObject spawnedObj = Instantiate(prefabObject, hit.point + new Vector3(0, prefSize.y/2, 0), Quaternion.Euler(0,PlayerCamera.transform.rotation.y,0));
            Renderer objRenderer = spawnedObj.GetComponent<Renderer>();
            objRenderer.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0f, 1f);
            // MaterialPropertyBlock materialProperty = new MaterialPropertyBlock();
            // materialProperty.SetColor("_Color", Random.ColorHSV()); // randomized color
            // spawnedObj.GetComponent<Renderer>().SetPropertyBlock(materialProperty);
        }
    }

    public void reset() {
        _enabled = false;
        selection.visible(false, 0f);
    }

    void assignPrefab(int select) {
        prefabObject = (prefabs.selectPrefab(select));
        selection.visible(false, 1.5f);
    }
}
