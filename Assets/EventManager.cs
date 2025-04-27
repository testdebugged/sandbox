using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public TakeEntities GrabScript;
    public Camera playerCamera;
    int value = 0;
    public void execute(int value = 0)
    {
        switch (value)
        {
            case 0:
                break; //none, redundancy
            case 1:
                DragScript.mouseClicked(); //drag
                break;
            case 2:
                SpawnScript.mouseClicked();
                break;
            case 3:
                DeleteScript.mouseClicked();
                break;
            case 4:
                GrabScript.mouseClicked();
                break;
        } 
    }
    public void resetTools() // reset variables tool
    {
        DragScript.reset();
        SpawnScript.reset();
        DeleteScript.reset();
        GrabScript.reset();
    }
}
