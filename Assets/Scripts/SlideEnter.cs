using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SlideEnter : MonoBehaviour
{

    public SteamVR_ActionSet actionSet;
    public SteamVR_Action_Single enterAction;

    public Slide slide;

    // Start is called before the first frame update
    void Start()
    {
        //slide = transform.parent.GetComponent<Slide>();

        //actionSet.Activate(SteamVR_Input_Sources.Any, 0);

        if (enterAction.activeBinding)
            Debug.Log("Bound");

        if (enterAction.active)
            Debug.Log("active");
    }

    /// <summary>
    /// Message received from Interactable
    /// </summary>
    /// <param name="hand"></param>
    public void HandHoverUpdate(Hand hand)
    {
       if (enterAction.GetAxis(SteamVR_Input_Sources.Any) > 0.5f)
        {

            GetComponent<Interactable>().enabled = false; // turn off Interactable

            slide.OnEnterSlide();
        }
    }

}
