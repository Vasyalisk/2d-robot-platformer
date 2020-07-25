using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float aliveTime;

    float deadTime;

    // Start is called before the first frame update
    void Start()
    {
        deadTime = Time.time + aliveTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= deadTime)
        {
            Destroy(gameObject);
        }
    }
}
