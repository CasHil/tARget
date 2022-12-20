using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField]
    private FingerInfoGizmo fingerInfoGizmo;

    // Start is called before the first frame update
    private void Start()
    {
        if (fingerInfoGizmo == null)
        {
            try
            {
                fingerInfoGizmo = GameObject.Find("TryOnManager").GetComponent<FingerInfoGizmo>();
            }
            catch
            {
                Debug.Log("Failed to find TryOnManager");
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        /*        fingerInfoGizmo.ShowFingerInformation();*/
        Vector3 ringPlacement = ManomotionManager.Instance.Hand_infos[0].hand_info.tracking_info.skeleton.joints[8];
        Debug.Log("Finger info at: " + ringPlacement);
        GameObject.Find("Cube").transform.position = ringPlacement;
    }
}