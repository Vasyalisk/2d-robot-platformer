using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PairedSwitchController : AbstractSwitchController, IResettable, IDetectable
{
    public SwitchController pairedSwitch;

    bool playerIsNear;

    protected override void AfterStart()
    {
    }

    private void Update()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.E))
        {
            Switch();
        }

        if (pairedSwitch.isOn != _isOn)
        {
            _isOn = pairedSwitch.isOn;
            SetSprite();
        }
    }

    void Switch()
    {
        pairedSwitch.Switch();
    }

    public void OnPlayerEnter()
    {
        playerIsNear = true;
    }

    public void OnPlayerExit()
    {
        playerIsNear = false;
    }

    public void ResetObject()
    {
        Start();
    }
}
