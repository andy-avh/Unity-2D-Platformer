using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOMapChanger : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] scriptableObjects; // array storing all scriptable objects / maps 
    [SerializeField] private MapDisplay mapDisplay;

    private int currentIndex; // change map

    private void Awake()
    {
        //mapDisplay.DisplayMap((Map)serializableObjects[0]); // pass 1st value into mapdisplay, test

        ChangeScriptableObject(0); // 0 for first map when we start the game
    }

    public void ChangeScriptableObject(int _change)
    {
        currentIndex += _change;

        // go to the 1st map when you press next on the last map
        if(currentIndex < 0)
        {
            currentIndex = scriptableObjects.Length - 1;
        }
        else if(currentIndex > scriptableObjects.Length - 1)
        {
            // and if on the first and press for the previous map, go to the last
            currentIndex = 0;
        }

        if(mapDisplay != null)
        {
            mapDisplay.DisplayMap((Map)scriptableObjects[currentIndex]); // display current map
        }
    }

}
