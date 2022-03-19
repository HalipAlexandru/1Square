using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
public class GameManager : MonoBehaviour
{
    //MOVE TO A UI SCRIPT
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject flood;
    [SerializeField] private GameObject levels;

    private TMPro.TextMeshProUGUI floodButtonTxt;
    private FloodMove floodOnOff;
    private bool floodState;

    private int platformNr;
    private int platformActive;
   
    private GameObject[] platforms;
    private GameObject[] toggles;

    private int sceneNum;
    private int sceneindex;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        floodButtonTxt = flood.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
        floodOnOff = GameObject.FindWithTag("Flood").GetComponent<FloodMove>();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeScene();
    }

    void Update()
    {

        //Esc opens menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeSelf);
            levels.SetActive(false);
        }

        //Reset when pressing R
        if (Input.GetKeyDown(KeyCode.R))
            Reset();

        if (platformActive == 0)
        {
            if (!menu.activeSelf)
            { 
                if (SceneManager.GetActiveScene().buildIndex != sceneNum - 1)
                {
                    LoadScene(sceneindex += 1);
                    
                }
                else
                {
                    LoadScene(0);
                    menu.SetActive(!menu.activeSelf);
                    floodOnOff.OnOFF(false);
                }
                InitializeScene();
                
            }
            Reset();
        }
    }

    void InitializeScene()
    {
        sceneNum = SceneManager.sceneCountInBuildSettings;
        sceneindex = SceneManager.GetActiveScene().buildIndex;

        platforms = GameObject.FindGameObjectsWithTag("Platform");
        toggles = GameObject.FindGameObjectsWithTag("Toggle");
        platformNr = platforms.Length;
        platformActive = platforms.Length;
        
    }

    public void StartGame()
    {
        menu.SetActive(!menu.activeSelf);
        floodOnOff.OnOFF(floodState);
        Reset();
    }

    public void Back()
    {
        menu.SetActive(!menu.activeSelf);
        levels.SetActive(!levels.activeSelf);
    }

    //resets the player, all the platforms and the locks
    public void Reset()
    {
        floodOnOff.Reset();
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

    public void FloodState()
    {
        if(floodButtonTxt.text == "Flood : OFF")
        {
            floodButtonTxt.text = "Flood : ON";
            floodState = true;
        }
        else
        {
            floodButtonTxt.text = "Flood : OFF";
            floodState = false;
        }
    }

    public void PlatformDestroyed()
    {
        platformActive--;
    }

    //used by the level select buttons so it can start at 1
    public void SelectLevel(int lvl)
    {
        LoadScene(lvl - 1);
        floodOnOff.OnOFF(false);
        floodOnOff.Reset();
    }

    void LoadScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

}
