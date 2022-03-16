using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColRight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Toggle")
            GetComponentInParent<PlayerController>().IsRight(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Toggle")
            GetComponentInParent<PlayerController>().IsRight(false);
    }
}
