using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vibrate : MonoBehaviour
{

    private GestureInfo gesture;

    void Update()
    {
        gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
        UseTriggerGesture(gesture);
    }
    
    /// <summary>
    /// Checks if the current visable hand performs a gesture from pinch family
    /// if so, then the code will be executed in this case the phone will vibrate.
    /// </summary>
    /// <param name="gesture">The current gesture being made</param>
    void UseTriggerGesture(GestureInfo gesture)
    {
        if (gesture.mano_gesture_trigger == ManoGestureTrigger.PICK)
        {
            if (gesture.left_right_hand == LeftOrRightHand.LEFT_HAND)
            // Your code here
            {Handheld.Vibrate();}
        }
    }
}








