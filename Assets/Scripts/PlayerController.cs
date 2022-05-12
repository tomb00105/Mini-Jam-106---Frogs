using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    Animator tongueAnimator;

    [SerializeField]
    BoxCollider2D tongueCollider;

    [SerializeField, Range(0f, 10f)]
    float jumpPower = 0f;

    public float jumpMultiplier = 5f;

    private bool isJumping = false;

    private Vector2 jumpToPoint;

    [SerializeField]
    private Transform targetJump;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        gameManager.lastLilypad = CheckForLilypad(transform.position);
    }

    private void Update()
    {
        if (gameManager.isAlive)
        {
            //Turns on the tongue collider if it is animating.
            tongueCollider.enabled = tongueAnimator.GetCurrentAnimatorStateInfo(0).IsName("Tongue");

            //Gets direction to mouse cursor from the player.
            Vector2 dirToMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            dirToMouse.Normalize();

            //Triggers the tongue animation when the left mouse button is clicked.
            if (Input.GetMouseButtonDown(0))
            {
                tongueAnimator.SetTrigger("Active");
                tongueCollider.enabled = true;
            }

            //Moves the player towards the calculated target position and handles logic for if they land on a lilypad or not.
            if (isJumping)
            {
                transform.position = Vector2.Lerp(transform.position, jumpToPoint, jumpMultiplier * Time.deltaTime);
                if (Vector2.Distance(transform.position, jumpToPoint) < 0.1f)
                {
                    if (targetJump != null)
                    {
                        gameManager.lastLilypad = targetJump;
                        isJumping = false;
                        targetJump = null;
                    }
                    else
                    {
                        targetJump = null;
                        isJumping = false;
                        gameManager.HitWater();
                    }
                }
            }

            //Handles logic for when the player is not currently jumping.
            if (!isJumping)
            {
                //Sets player rotation towards the mouse cursor.
                float rot_z = Mathf.Atan2(dirToMouse.y, dirToMouse.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

                //While the player holds down the space key, the jump distance increases.
                if (Input.GetKey(KeyCode.Space))
                {
                    jumpPower += jumpMultiplier * Time.deltaTime;
                    if (jumpPower >= 10f)
                    {
                        jumpPower = 10f;
                    }
                }
                //If the player right clicks, jump power is set back to 0 so they can cancel a jump if they wish.
                if (Input.GetKey(KeyCode.Mouse1))
                {
                    jumpPower = 0;
                }
                //Handles logic for when the space key is released.
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    if (jumpPower > 0)
                    {
                        //Sets isJumping to true to ensure the player can't control the frog while in flight.
                        isJumping = true;
                        //Calculates the distance the player will jump and what position this ends at.
                        Vector2 jumpDistance = dirToMouse * jumpPower;
                        jumpToPoint = new Vector2(transform.position.x + jumpDistance.x, transform.position.y + jumpDistance.y);

                        /*
                        Debugging for jump distances
                        Debug.Log("Direction to mouse: " + dirToMouse.ToString());
                        Debug.Log("Jump Power: " + jumpPower.ToString());
                        Debug.Log("Jump to point: " + jumpToPoint.ToString());
                        */

                        //Resets jump power for next jump.
                        jumpPower = 0f;

                        //Sets the target lilypad for the jump if the player is going to land on one.
                        if (targetJump == null)
                        {
                            targetJump = CheckForLilypad(jumpToPoint);
                        }
                    }
                }
            }
        }
        
    }

    //Checks whether there is a lilypad at the given position and, if there is, returns it's transform.
    private Transform CheckForLilypad(Vector2 target)
    {
        foreach (GameObject lilypad in GameObject.FindGameObjectsWithTag("Lilypad"))
        {
            if (lilypad.GetComponent<CircleCollider2D>().bounds.Contains(target))
            {
                return lilypad.transform;
            }
        }
        return null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fence"))
        {
            //Debug.Log("HIT FENCE");
            targetJump = null;
            isJumping = false;
            gameManager.HitFence();
        }
    }
}
