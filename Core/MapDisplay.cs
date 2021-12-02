using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // for text
using UnityEngine.SceneManagement; // needed to access Unity's scene manager

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private Text mapName;
    [SerializeField] private Text mapDescription;
    [SerializeField] private Image mapImage;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject lockIcon;
    public void DisplayMap(Map _map) // take in map parameter
    {
        mapName.text = _map.mapName;
        //mapName.color = _map.nameColor;
        mapDescription.text = _map.mapDescription;
        mapImage.sprite = _map.mapImage;


        // unlocking maps
        bool mapUnlocked = PlayerPrefs.GetInt("currentScene", 0) >= _map.mapIndex;
        lockIcon.SetActive(!mapUnlocked); // show lockicon when map is not unlocked
        playButton.interactable = mapUnlocked; // can press play when map is unlocked

        // original colour if unlocked
        if (mapUnlocked)
        {
            mapImage.color = Color.white;
        }
        // grey out slightly locked levels
        else
        {
            mapImage.color = Color.gray;
        }

        // make play button work
        // load correct level
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(()=> SceneManager.LoadScene(_map.sceneToLoad.name)); // use unity's scene manager to load correct scene
    }
}
