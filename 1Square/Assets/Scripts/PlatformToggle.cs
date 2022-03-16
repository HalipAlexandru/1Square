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

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetLock()
    {
        gameObject.SetActive(true);
        platform.GetComponent<PlatformController>().LockUnlock(true);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            platform.GetComponent<PlatformController>().LockUnlock(false);
            gameObject.SetActive(false);
        }
    }
}
