using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject levels;
    public static GameManager Instance;

    private GameObject[] platforms;
    private GameObject[] toggles;
    int platformNr;
    int platformActive;


    int sceneNum;
    int sceneindex;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneNum = SceneManager.sceneCountInBuildSettings;
        sceneindex = SceneManager.GetActiveScene().buildIndex;

        platforms = GameObject.FindGameObjectsWithTag("Platform");
        toggles = GameObject.FindGameObjectsWithTag("Toggle");
        platformNr = platforms.Length;
        platformActive = platforms.Length;

        
    }

    // Update is called once per frame
    void Update()
    {

        //Esc opens menu
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeSelf);
        }

        /*
        if(Input.GetKeyDown(KeyCode.K))
        {
            if (SceneManager.GetActiveScene().buildIndex == sceneNum - 1)
                LoadScene(0);
            else
                LoadScene(sceneindex += 1);
        }
        */

        //Reset when pressing R
        if (Input.GetKeyDown(KeyCode.R))
            Reset();

        if (platformActive == 0)
        {
            if(menu.activeSelf)
            {
                //The player can still play the game while the menu is up but it will not progress to the next level
                Reset();
            }
            else
                if (SceneManager.GetActiveScene().buildIndex != sceneNum - 1)
                {
                    LoadScene(sceneindex += 1);
                }
                else
                {
                    LoadScene(0);
                }
            //Reset();
        }
    }


    public void StartGame()
    {
        menu.SetActive(!menu.activeSelf);
    }

    public void Back()
    {
        menu.SetActive(!menu.activeSelf);
        levels.SetActive(!levels.activeSelf);
    }

    //resets the player all the platforms and the locks
    public void Reset()
    {
        
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().ResetPosition();
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].GetComponent<PlatformController>().ResetPlatform();
        }
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].GetComponent<PlatformToggle>().ResetLock();
        }
        platformActive = platformNr;
    }

    public void PlatformDestroyed()
    {
        platformActive--;
    }

    //used by the level select buttons so it can start at 1
    public void SelectLevel(int lvl)
    {
        LoadScene(lvl - 1);
    }

    void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

}
