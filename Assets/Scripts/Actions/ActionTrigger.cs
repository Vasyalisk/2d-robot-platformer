using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTrigger : MonoBehaviour
{
    public float delay;

    AbstractAction[] actions;
    float triggerTime;

    // Start is called before the first frame update
    void Start()
    {
        actions = GetComponents<AbstractAction>();
        triggerTime = Time.time + delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= triggerTime)
        {
            foreach (AbstractAction act in actions)
            {
                act.ActionOn();
            }
        }
    }
}
