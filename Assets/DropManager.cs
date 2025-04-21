using System.Collections.Generic; // needed for list
using UnityEngine;

public class DropManager : MonoBehaviour
{
    public List<GameObject> prefabList;
    int pagination = 0; // (0 > n > n+1
    public GameObject selectPrefab(int select) {
        pagination += (select > .1 ? 1 : -1);
        if (0 > pagination) {
            pagination = 0; // restart/cycle back
        }
        if ((prefabList.Count-1) < pagination) {
            pagination = prefabList.Count-1; // restart/cycle back
        }
        print(prefabList[pagination]);
        return prefabList[pagination];
    }
}
//please you
//i please you