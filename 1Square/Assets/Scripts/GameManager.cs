using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    public static GameManager Instance;

    private GameObject[] platforms;
    private GameObject[] toggles;
    int platformNr;
    int platformActive;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        platforms = GameObject.FindGameObjectsWithTag("Platform");
        toggles = GameObject.FindGameObjectsWithTag("Toggle");
        platformNr = platforms.Length;
        platformActive = platforms.Length; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeSelf);
        }

        if(platformActive == 0 || Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

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
}
