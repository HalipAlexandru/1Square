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
    public float raycastDist = 1f;
    public bool isMoving = false;
    public bool isOnGround = false;
    

    RaycastHit2D up;
    RaycastHit2D left;
    RaycastHit2D right;
    RaycastHit2D topLeft;
    RaycastHit2D topRight;

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
        //raycasts are used to check for platforms around the player
        up = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up),raycastDist);
        left = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.left), raycastDist);
        right = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), raycastDist);
        topLeft = Physics2D.Raycast(transform.position + new Vector3(0, raycastDist), transform.TransformDirection(new Vector2(-raycastDist, 0)),raycastDist);
        topRight = Physics2D.Raycast(transform.position + new Vector3(0, raycastDist), transform.TransformDirection(new Vector2(raycastDist, 0)),raycastDist);
        //DrawRaycast();

        if (Input.GetKey(KeyCode.D) && !isMoving && isOnGround)
        {    
            if (right && !up && !topRight)
                StartCoroutine(Jump(new Vector3(moveDist, 0)));
            else if(!right)
                StartCoroutine(Move(new Vector3(moveDist, 0))); 
        }
        if (Input.GetKey(KeyCode.A) && !isMoving && isOnGround)
        {
            if (left && !up && !topLeft)
                StartCoroutine(Jump(new Vector3(-moveDist, 0)));
            else if (!left)
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

        //the pivot determines the shape of the path
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
    
    //if the player falls off the level is reset
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

    void DrawRaycast()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(new Vector2(0, raycastDist)));
        Debug.DrawRay(transform.position, transform.TransformDirection(new Vector2(-raycastDist, 0)));
        Debug.DrawRay(transform.position, transform.TransformDirection(new Vector2(raycastDist, 0)));
        Debug.DrawRay(transform.position + new Vector3(0, raycastDist), transform.TransformDirection(new Vector2(-raycastDist, 0)));
        Debug.DrawRay(transform.position + new Vector3(0, raycastDist), transform.TransformDirection(new Vector2(raycastDist, 0)));
    }

}
