using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;

public class ARControl : MonoBehaviour
{
    [SerializeField] GameObject placementIndicator = null;

    [SerializeField] ARSessionOrigin sessionOrigin = null;
    [SerializeField] ARSession arSession = null;
    [SerializeField] ARRaycastManager raycastManager;

    [SerializeField] ARCustomObject theObject = null;
    bool isObjectPlaced = false;

    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    Pose placementPose;
    bool placementPoseValid = false;

    Vector3 screenCenter;
    Vector3 cameraBearing = new Vector3();

    [SerializeField] GameObject worldCam = null;
    [SerializeField] GameObject arCam = null;
    Vector3 camOriginPos;
    Quaternion camOriginRot;

    IEnumerator Start()
    {
        if ((ARSession.state == ARSessionState.None) || (ARSession.state == ARSessionState.CheckingAvailability))
        {
            yield return ARSession.CheckAvailability();
        }

        if (ARSession.state == ARSessionState.Unsupported)
        {
            // Start some fallback experience for unsupported devices
        }
        else
        {
            // Start the AR session
            arSession.enabled = false;
        }

        screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        raycastManager = sessionOrigin.GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        if (Input.GetMouseButtonDown(0))
            PlayerInput();
    }

    void PlayerInput()
    {
        if (!placementPoseValid)
            return;
        if (!arSession.isActiveAndEnabled)
            return;
        if (isObjectPlaced)
            return;

        theObject.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        theObject.SetPosAndRot();
        isObjectPlaced = true;
    }

    void UpdatePlacementPose()
    {
        raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);
        placementPoseValid = (hits.Count > 0);

        if (placementPoseValid)
        {
            placementPose = hits[0].pose;

            Vector3 cameraForward = Camera.current.transform.forward;
            cameraBearing.Set(cameraForward.x, 0, cameraForward.z);
            cameraBearing = cameraBearing.normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    void UpdatePlacementIndicator()
    {
        if (placementPoseValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    public void ToggleARMode()
    {
        if (arSession.isActiveAndEnabled)
        {
            arSession.enabled = !arSession.isActiveAndEnabled;
            ARDisabled();
        }
        else
        {
            AREnabled();
            arSession.enabled = !arSession.isActiveAndEnabled;
        }

    }

    public void RepositionObject()
    {
        if (!arSession.isActiveAndEnabled)
            return;

        isObjectPlaced = false;
    }

    void ARDisabled()
    {
        theObject.ResetPosAndRot();
        worldCam.SetActive(true);
        arCam.SetActive(false);
    }

    void AREnabled()
    {
        theObject.PlaceToLastPosAndRot();
        worldCam.SetActive(false);
        arCam.SetActive(true);
    }
}
