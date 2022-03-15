using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public int num = 1;
    public float destroyTime = 0.1f;
    TMPro.TextMeshProUGUI txt;
    // Start is called before the first frame update
    void Start()
    {
        txt = this.gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        txt.text = num.ToString();
        if (num == 0)
        {
            StartCoroutine(Destroy());
        }
    }

    IEnumerator Destroy()
    {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            txt.enabled = false;
            yield return new WaitForSeconds(destroyTime);
            Object.Destroy(gameObject);
    }

    public void NumDec()
    {
        num--;
    }
}
