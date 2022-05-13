using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If collides with a fly, destroys the fly.
        if (collision.gameObject.CompareTag("Fly"))
        {
            //Debug.Log("TONGUE HIT FLY");
            collision.gameObject.GetComponent<FlyController>().DestroyFly(true);
        }
    }
}
