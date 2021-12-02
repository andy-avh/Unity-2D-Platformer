using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="New Map", menuName ="Scriptable Objects/Map")]

public class Map : ScriptableObject // data container in Unity
{
    // all variables public as we wnat to edit and access them from other scripts
    public int mapIndex;
    public string mapName;
    public string mapDescription;
    public Color nameColor;
    public Sprite mapImage;
    public Object sceneToLoad;

}
