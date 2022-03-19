using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class PlatformToggle : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private Color colour;

    private PlatformController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = platform.GetComponent<PlatformController>();
        transform.GetComponent<SpriteRenderer>().color = colour;
        transform.GetChild(0).transform.GetComponent<Light2D>().color = colour;
        controller.LockUnlock(true);
        controller.ChangeColour(colour);
    }

    public void ResetLock()
    {
        gameObject.SetActive(true);
        controller.LockUnlock(true);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //unlocks linked platform on collision with the player
        if (collision.gameObject.tag == "Player")
        {
            controller.LockUnlock(false);
            gameObject.SetActive(false);
        }
    }
}
