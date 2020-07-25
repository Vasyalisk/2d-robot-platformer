using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public CheckPoint lastCheckPoint;
    public bool spawnOnStart;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnOnStart)
        {
            SpawnPlayer();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnPlayer()
    {
        player.transform.position = lastCheckPoint.transform.position;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public void RespawnPlayer()
    {
        lastCheckPoint.ResetDependencies();
        SpawnPlayer();
    }
}
