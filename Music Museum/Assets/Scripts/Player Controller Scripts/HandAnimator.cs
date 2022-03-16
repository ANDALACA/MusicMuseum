using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimator : MonoBehaviour
{

    //Speed that fingers animate.
    public float speed = 5.0f;
    //Ref to the controller
    public XRController controller = null;

    private Animator animator = null;

    public class Finger
    {
        public FingerType type = FingerType.None;
        public float current = 0.0f;
        public float target = 0.0f;

        public Finger(FingerType type)
        {
            this.type = type;
        }
    }

    //List of fingers that we use during a grip motion.
    private readonly List<Finger> gripFingers = new List<Finger>()
    {
        new Finger(FingerType.Middle),
        new Finger(FingerType.Ring),
        new Finger(FingerType.Pinky)
    };

    //List of fingers that we use during a pointing motion. 
    private readonly List<Finger> pointFingers = new List<Finger>()
    {
        new Finger(FingerType.Index),
        new Finger(FingerType.Thumb),
    };

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Store input
        CheckGrip();
        CheckPointer();

        //Smooth input values
        SmoothFinger(pointFingers);
        SmoothFinger(gripFingers);

        //Apply smoothed values
        AnimateFinger(pointFingers);
        AnimateFinger(gripFingers);

    }

    //Gets the value for our grip and animates fingers according to that. 
    private void CheckGrip()
    {
        //Gets the value of grip. 
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            SetFingerTargets(gripFingers, gripValue);
        }
    }

    //Try to get trigger value and animates fingers according to that. 
    private void CheckPointer()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float pointerValue))
        {
            SetFingerTargets(pointFingers, pointerValue);
        }
    }

    //Sets the target value of where to animate fingers to. 
    private void SetFingerTargets(List<Finger> fingers, float value)
    {
        foreach (Finger finger in fingers)
        {
            finger.target = value;
        }
    }

    //Smooths finger animation over time.
    private void SmoothFinger(List<Finger> fingers)
    {
        foreach (Finger finger in fingers)
        {
            float time = speed * Time.unscaledDeltaTime;
            finger.current = Mathf.MoveTowards(finger.current, finger.target, time);
        }
    }

    //Animates all fingers´.
    private void AnimateFinger(List<Finger> fingers)
    {
        foreach (Finger finger in fingers)
        {
            AnimateFinger(finger.type.ToString(), finger.current);
        }
    }

    //Tells the animator which finger to change and the value. 
    private void AnimateFinger(string finger, float blend)
    {
        animator.SetFloat(finger, blend);
    }

    public enum FingerType
    {
        None,
        Thumb,
        Index,
        Middle,
        Ring,
        Pinky
    }

}