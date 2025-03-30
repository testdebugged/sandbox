
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // public enum objectList { // cam be referenced
    //     Box,
    //     Sphere
    // }
    // EventManager for managing events. object list can be swapped out with array/prefab or resource list, maybe?
    // will act as the middleman
    public DragEntities DragScript;
    public SpawnEntities SpawnScript; // obj to spawn should be decided from player monobehaviour
    public DeleteEntities DeleteScript;
    public Camera playerCamera;
    void execute(int value = 0)
    {

    }
}
