using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class InputManager : MonoBehaviour
{


    public static Action<Vector2> fingerDownBroadCast;
    public static Action<Vector2> fingerDragBroadCast;
    public static Action fingerUpBroadCast;



    private void OnEnable(){
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
        // UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
        // UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove += FingerMove;
        // UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += FingerUp;

    }

    private void OnDisable(){
        
        TouchSimulation.Disable();
        // UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
        // UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove -= FingerMove;
        // UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp -= FingerUp;

        EnhancedTouchSupport.Disable();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // private void FingerDown(Finger finger){
    //     fingerDownBroadCast?.Invoke(finger.screenPosition);
    // }

    // private void FingerMove(Finger finger){
    //     fingerDragBroadCast?.Invoke(finger.screenPosition);
    // }

    // private void FingerUp(Finger finger){
    //     fingerUpBroadCast?.Invoke();
    // }
}
