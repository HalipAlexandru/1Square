using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float timeMove = 0.5f;
    public float timeJump = 0.2f;
    public float centerOffset = 1f;
    public float jumpDist = 2f;
    public float moveDist = 2f;
    public bool isMoving = false;
    public bool isOnGround = false;
    public bool needJumpLeft = false;
    public bool needJumpRight = false;
    public bool needJumpTop = false;


    private Vector2 playerStartPos;
    private Rigidbody2D rb; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStartPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.D) && !isMoving && isOnGround)
        {    
            if (needJumpRight && !needJumpTop)
                StartCoroutine(Jump(new Vector3(moveDist, 0)));
            else if(!needJumpRight)
                StartCoroutine(Move(new Vector3(moveDist, 0))); 
        }
        if (Input.GetKey(KeyCode.A) && !isMoving && isOnGround)
        {
            if (needJumpLeft && !needJumpTop)
                StartCoroutine(Jump(new Vector3(-moveDist, 0)));
            else if (!needJumpLeft)
                StartCoroutine(Move(new Vector3(-moveDist, 0)));
        }
    }

    IEnumerator Jump(Vector3 direction)
    {
        isMoving = true;

        Vector3 origPos = transform.position;
        Vector3 newPos = transform.position + new Vector3(0, jumpDist);
        for (float i = 0; i < timeJump; i += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(origPos, newPos, (i / timeMove));
            yield return null;
        }

        transform.position = newPos;

        yield return StartCoroutine(Move(direction));
    }

    IEnumerator Move(Vector3 direction)
    {
        isOnGround = false;
        isMoving = true;
        Vector3 origPos = transform.position;
        Vector3 newPos = transform.position + direction;

        Vector3 pivot = (origPos + newPos) / 2f;
        pivot -= new Vector3(0, centerOffset);

        for(float i = 0; i < timeMove; i += Time.deltaTime)
        {
            transform.position = Vector3.Slerp(origPos - pivot, newPos - pivot, (i / timeMove)) + pivot;
            yield return null;
        }

        transform.position = newPos;
        isMoving = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;
        if (collision.gameObject.tag == "Platform")
            collision.gameObject.GetComponent<PlatformController>().NumDec();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "GameOver")
        {
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().Reset();
        }
    }

    public void ResetPosition()
    {
        gameObject.transform.position = playerStartPos;
    }

    public void IsLeft(bool state)
    {
        needJumpLeft = state;
    }
    public void IsRight(bool state)
    {
        needJumpRight = state;
    }

    public void IsTop(bool state)
    {
        needJumpTop = state;
    }
}
