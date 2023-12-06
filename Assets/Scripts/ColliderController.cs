using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public int score = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == gameObject.tag)
        {
            score++;
            //Debug.Log("Score " + gameObject.tag + ": " + score); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == gameObject.tag)
        {
            score--;
            //Debug.Log("Score " + gameObject.tag + ": " + score); 
        }
    }
}
