using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSwitchController : MonoBehaviour
{
    public Sprite onSprite;
    public Sprite offSprite;
    public bool isOn { get { return _isOn; } }

    SpriteRenderer switchRenderer;
    protected bool _isOn;

    protected void Start()
    {
        _isOn = false;
        switchRenderer = GetComponent<SpriteRenderer>();
        SetSprite();
        AfterStart();
    }

    protected abstract void AfterStart();

    protected void SetSprite()
    {
        if (isOn)
        {
            switchRenderer.sprite = onSprite;
        }
        else
        {
            switchRenderer.sprite = offSprite;
        }
    }
}
