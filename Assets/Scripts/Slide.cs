using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[ExecuteInEditMode]
public class Slide : MonoBehaviour
{
    /// <summary>
    /// True, if the player is onboard the slice.
    /// </summary>
    public bool onboard;

    /// <summary>
    /// True, if the slide is flying a tour.
    /// </summary>
    public bool playTour;

    /// <summary>
    /// Orientation (yaw) of the slide.
    /// </summary>
    [Header("Normalized range: -1 to 1 maps to -360° to 360° yaw"), Range(-1,1)]
    public float yaw = 0;

    Animator tourAnimator;

    private void Awake()
    {
        tourAnimator = GetComponent<Animator>();
        tourAnimator.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = Quaternion.Euler(0, yaw * 360, 0);
    }

    public void OnTourClipEnded()
    {
        Debug.Log("Clip ended");
        GetComponentInChildren<Interactable>().enabled = true;

        tourAnimator.GetCurrentAnimatorClipInfo(0);
        tourAnimator.speed = 0; // stop
    }

    public void OnStartOrPauseTour()
    {
        if (!onboard)
            OnEnterSlide();
        else
        {
            if (tourAnimator.speed == 0)
                tourAnimator.speed = 1;
            else
                tourAnimator.speed = 0;
        }
    }

    /// <summary>
    /// Called from <see cref="SlideEnter"/>
    /// </summary>
    internal void OnEnterSlide()
    {
        if (!onboard)
        {
            onboard = true;

            // -- parent player to this slide ---      
            Vector3 offset = this.transform.position - Player.instance.headCollider.transform.position; // check offset HMD to slide
            offset.y = 0;

            Player.instance.transform.SetParent(this.transform, worldPositionStays: true);  // parent player GO to this slide
            Player.instance.transform.Translate(offset, Space.World);
            // --
        }

        // start animator
        if (tourAnimator.speed == 0 )
            tourAnimator.speed = 1; // start
    }
}
