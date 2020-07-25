using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    IDetectable detectableObj;

    private void Start()
    {
        detectableObj = GetComponent<IDetectable>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            detectableObj.OnPlayerEnter();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            detectableObj.OnPlayerExit();
        }
    }
}
