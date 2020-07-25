using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignWithRays : MonoBehaviour
{
    public int raysCount;
    public float switchTimeMax;
    public float switchTimeMin;

    float nextSwitchTime;
    float[] angles;

    // Start is called before the first frame update
    void Start()
    {
        float minAngle = 360f / raysCount;
        angles = new float[raysCount];
        int i = 0;
        foreach (float a in angles)
        {
            angles[i] = i * minAngle;
            i++;
        }
        SetSwitchTime();
    }

    // Update is called once per frame
    void Update()
    {
        nextSwitchTime -= Time.deltaTime;
        if (nextSwitchTime <= 0f)
        {
            SwitchRotation();
            SetSwitchTime();
        }
    }

    void SwitchRotation()
    {
        Vector3 newRotation = transform.eulerAngles;
        int i = Random.Range(0, angles.Length);
        newRotation.z = angles[i];
        transform.eulerAngles = newRotation;
    }

    void SetSwitchTime()
    {
        nextSwitchTime = Random.Range(switchTimeMin, switchTimeMax);
    }
}
