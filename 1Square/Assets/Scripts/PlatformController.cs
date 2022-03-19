using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private int num = 1;
    [SerializeField] private float destroyTime = 0.1f;
    [SerializeField] private bool isLocked = false;

    private int count;
    private TMPro.TextMeshProUGUI txt;
    private ParticleSystem destroyParticle;
    // Start is called before the first frame update
    void Start()
    {
        destroyParticle = gameObject.transform.GetChild(2).GetComponent<ParticleSystem>();
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
        destroyParticle.Play();
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

    public void ChangeColour(Color colour)
    {
        Debug.Log(colour);
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = colour;
        transform.GetChild(0).GetChild(0).transform.GetComponent<Light2D>().color = colour;
    }

    public void NumDec()
    {
        if(!isLocked)
            count--;
    }


}
