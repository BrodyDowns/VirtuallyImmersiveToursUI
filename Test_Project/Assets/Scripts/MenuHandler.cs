using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour {
    public GameObject leftController;
    public GameObject rightController;
    public GameObject menu;

    private SteamVR_LaserPointer leftLaserPointer;
    private SteamVR_LaserPointer rightLaserPointer;

    private SteamVR_TrackedController leftTracked;
    private SteamVR_TrackedController rightTracked;

    bool menuState = false;

    private void OnEnable()
    {
        //set menu to false to when starting
        menu.SetActive(menuState);

        //set up laser pointers
        leftLaserPointer = leftController.GetComponent<SteamVR_LaserPointer>();
        rightLaserPointer = rightController.GetComponent<SteamVR_LaserPointer>();

        //left
        leftLaserPointer.active = false;
        leftLaserPointer.PointerIn -= HandlePointerIn;
        leftLaserPointer.PointerIn += HandlePointerIn;
        leftLaserPointer.PointerOut -= HandlePointerOut;
        leftLaserPointer.PointerOut += HandlePointerOut;

            //right
        rightLaserPointer.active = false;
        rightLaserPointer.PointerIn -= HandlePointerIn;
        rightLaserPointer.PointerIn += HandlePointerIn;
        rightLaserPointer.PointerOut -= HandlePointerOut;
        rightLaserPointer.PointerOut += HandlePointerOut;

        //set up tracked controllers
        leftTracked = leftController.GetComponent<SteamVR_TrackedController>();
        if (leftTracked == null)
        {
            leftTracked = leftController.GetComponentInParent<SteamVR_TrackedController>();
        }

        rightTracked = rightController.GetComponent<SteamVR_TrackedController>();
        if (rightTracked == null)
        {
            rightTracked = rightController.GetComponentInParent<SteamVR_TrackedController>();
        }

            //left
        leftTracked.TriggerClicked -= HandleTriggerClicked;
        leftTracked.TriggerClicked += HandleTriggerClicked;
        leftTracked.MenuButtonClicked -= HandleMenuButton;
        leftTracked.MenuButtonClicked += HandleMenuButton;

            //right
        rightTracked.TriggerClicked -= HandleTriggerClicked;
        rightTracked.TriggerClicked += HandleTriggerClicked;
        rightTracked.MenuButtonClicked -= HandleMenuButton;
        rightTracked.MenuButtonClicked += HandleMenuButton;

    }

    private void HandleMenuButton(object sender, ClickedEventArgs e)
    {
        menuState = !menuState;
        leftLaserPointer.active = menuState;
        rightLaserPointer.active = menuState;

        //Debug.Log(leftLaserPointer.active + " " + rightLaserPointer.active);
        
        menu.SetActive(menuState);

    }

    private void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
       // Debug.Log("trigger clicked");
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
        }
    }

    private void HandlePointerIn(object sender, PointerEventArgs e)
    {
        var button = e.target.GetComponent<Button>();
        if (button != null)
        {
            button.Select();
            Debug.Log("HandlePointerIn", e.target.gameObject);
        }
    }

    private void HandlePointerOut(object sender, PointerEventArgs e)
    {

        var button = e.target.GetComponent<Button>();
        if (button != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            Debug.Log("HandlePointerOut", e.target.gameObject);
        }
    }


}
