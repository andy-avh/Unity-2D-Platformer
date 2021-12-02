using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // needed to access Unity's scene manager
public class FinishLevel : MonoBehaviour
{
    public void FinishLvl()
    {


        PlayerPrefs.SetInt("currentScene", SceneManager.GetActiveScene().buildIndex); // this will unlock the next scene when you finish a level
        SceneManager.LoadScene(0); // load the level selection scene when complete 


    }
}
