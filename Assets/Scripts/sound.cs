using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    private GestureInfo gesture;
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume=0.5f;

    void Update()
    {
        gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
        UseTriggerGesture(gesture);
    }

    
    void UseTriggerGesture(GestureInfo gesture)
    {
        if (gesture.mano_gesture_trigger == ManoGestureTrigger.PICK)
        {
            if (gesture.left_right_hand == LeftOrRightHand.RIGHT_HAND)
            {
                audioSource.PlayOneShot(clip, volume);
            }
        }
    }
}
