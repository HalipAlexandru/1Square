using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public int num = 1;
    public float destroyTime = 0.1f;
    public bool isLocked = false;

    private int count;
    TMPro.TextMeshProUGUI txt;

    // Start is called before the first frame update
    void Start()
    {
        txt = this.gameObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        count = num;
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = count.ToString();
        if (count == 0)
        {
            StartCoroutine(Destroy());
        }
    }

    public void ResetPlatform()
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        txt.enabled = true;
        count = num;
    }

    //it disables the sprite renderer first so the player can move off the platform without falling 
    IEnumerator Destroy()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        txt.enabled = false;
        yield return new WaitForSeconds(destroyTime);
        gameObject.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        txt.enabled = true;
        count = num;
        GameManager.Instance.PlatformDestroyed();
    }

    public void LockUnlock(bool toggle)
    {
        isLocked = toggle;
        if (toggle == true)
            transform.GetChild(0).gameObject.SetActive(true);
        else
            transform.GetChild(0).gameObject.SetActive(false);
    }

    public void NumDec()
    {
        if(!isLocked)
            count--;
    }


}
