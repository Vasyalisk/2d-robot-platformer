using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAction : AbstractAction, IResettable
{
    public AudioClip hitSound;

    HingeJoint2D joint;
    JointMotor2D motor;

    AudioSource hammerAS;
    Quaternion startRotation;
    float startMotorSpeed;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
        joint = GetComponent<HingeJoint2D>();
        motor = joint.motor;
        startMotorSpeed = motor.motorSpeed;
        hammerAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    override public void ActionOn()
    {
        motor.motorSpeed *= -1;
        joint.motor = motor;
        hammerAS.Play();
    }

    override public void ActionOff()
    {
        ActionOn();
        hammerAS.Stop();
    }

    public void ResetObject()
    {
        motor.motorSpeed = startMotorSpeed;
        joint.motor = motor;
        transform.rotation = startRotation;
        hammerAS.Stop();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hammerAS.Stop();
        hammerAS.PlayOneShot(hitSound);
    }
}
