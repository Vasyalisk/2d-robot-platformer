using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryPortalAction : AbstractAction
{
    public GameObject playerPrefab;
    public string nextSceneName;
    public SwitchController portalSwitch;
    public GameObject victoryStarPrefab;

    AudioSource portalAudioSource;
    bool isActive;
    bool playerNear;
    GameObject victoryStar;
    GameObject currentPlayer;
    float starAnimationDuration;
    float removeTime;
    float spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        playerNear = false;
        portalAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerNear && Time.time >= removeTime)
        {
            removeTime = spawnTime + 500f;
        }
        else if (playerNear && Time.time >= spawnTime)
        {
            playerNear = false;
            portalAudioSource.Stop();
            Destroy(victoryStar);
            portalSwitch.Switch();
            LoadNextScene();
        }
    }

    public override void ActionOn()
    {
        isActive = true;
        portalAudioSource.Play();
    }

    public override void ActionOff()
    {
        isActive = false;
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && isActive)
        {
            isActive = false;
            playerNear = true;
            other.gameObject.GetComponent<PlayerController>().SetDisabled();
            victoryStar = Instantiate(victoryStarPrefab, transform);
            starAnimationDuration = victoryStar.GetComponent<Animation>().clip.length;
            float currentTime = Time.time;
            removeTime = currentTime + starAnimationDuration / 2;
            spawnTime = currentTime + starAnimationDuration;
        }
    }
}
