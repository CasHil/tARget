using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vibrate : MonoBehaviour
{

    public GestureInfo gesture;

    void Update()
    {
        gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
        DetectManoClass(gesture);
    }
    
    /// <summary>
    /// Checks if the current visable hand performs a gesture from pinch family
    /// if so, then the code will be executed in this case the phone will vibrate.
    /// </summary>
    /// <param name="gesture">The current gesture being made</param>
    void DetectManoClass(GestureInfo gesture)
    {
        
    }
}
