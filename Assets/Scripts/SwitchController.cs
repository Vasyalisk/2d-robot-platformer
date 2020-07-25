using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : AbstractSwitchController, IResettable, IDetectable
{
    public AbstractAction connectedAction;
    public bool onOnStart;

    AudioSource switchAudioSource;
    bool playerIsNear;

    protected override void AfterStart()
    {
        switchAudioSource = GetComponent<AudioSource>();
        if (onOnStart)
        {
            Switch();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.E))
        {
            Switch();
        }
    }

    public void Switch()
    {
        switchAudioSource.Play();
        if (_isOn)
        {
            _isOn = false;
            connectedAction.ActionOff();
        }
        else
        {
            _isOn = true;
            connectedAction.ActionOn();
        }
        SetSprite();
    }

    public void ResetObject()
    {
        Start();
    }

    public void OnPlayerEnter()
    {
        playerIsNear = true;
    }

    public void OnPlayerExit()
    {
        playerIsNear = false;
    }
}
