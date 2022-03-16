using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformToggle : MonoBehaviour
{
    public GameObject platform;
    // Start is called before the first frame update
    void Start()
    {
        platform.GetComponent<PlatformController>().LockUnlock(true);
    }

    public void ResetLock()
    {
        gameObject.SetActive(true);
        platform.GetComponent<PlatformController>().LockUnlock(true);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //unlocks linked platform on collision with the player
        if (collision.gameObject.tag == "Player")
        {
            platform.GetComponent<PlatformController>().LockUnlock(false);
            gameObject.SetActive(false);
        }
    }
}
