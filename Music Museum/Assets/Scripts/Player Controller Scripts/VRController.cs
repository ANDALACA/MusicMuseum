//======= Copyright (c) ANDALACA Corporation, All rights reserved. ===============
//
// Purpose: Handles VR locomotion. Currently implemeted: Teleportation based and continious locomotion. 
//
//=============================================================================
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class VRController : MonoBehaviour
{
    #region Inspector Variables
    [Tooltip("Put in action asset.")]
    public InputActionAsset actionAsset;
    #endregion
    [Tooltip("Put in teleport action.")]
    public InputActionReference teleport;

    #region Private Variables
    private XRRayInteractor rayInteractor;
    private bool isTeleporting = false;
    //XR Rig
    private GameObject xrRig;
    private GameObject rayIndicatorObject;
    private CharacterControllerDriver characterControllerDriver;
    private TeleportationProvider teleportProvider;
    private ActionBasedContinuousMoveProvider continiousMovementProvider;
    #endregion

    #region Static Variables
    public static VRController Instance { get; private set; }
    #endregion

    /// <summary>
    /// Assign objects using tags and assign various variables and events. 
    /// </summary>
    private void Awake()
    {
        #region Singleton
        //Singleton pattern
        if (Instance == null) //Creates single ton instance and adds it to dont destory
        {
            //For Variable Controller
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        #endregion
    }

    public void LoadVRController()
    {
        rayIndicatorObject = GameObject.FindWithTag("TeleportRay").gameObject;
        rayInteractor = rayIndicatorObject.GetComponent<XRRayInteractor>();

        xrRig = GameObject.FindWithTag("Player").transform.GetChild(0).gameObject;
        teleportProvider = xrRig.GetComponent<TeleportationProvider>();
        continiousMovementProvider = xrRig.GetComponent<ActionBasedContinuousMoveProvider>();
        characterControllerDriver = xrRig.GetComponent<CharacterControllerDriver>();

        //Disable Raycast. 
        rayInteractor.enabled = false;
    }

    public void ActivateContiniousLocomotion()
    {
        xrRig.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
        xrRig.GetComponent<TeleportationProvider>().enabled = false;
        rayIndicatorObject.gameObject.SetActive(false);
        characterControllerDriver.locomotionProvider = continiousMovementProvider;
        teleport.action.performed -= TeleportActivate;
        teleport.action.canceled -= TeleportCancelled;

    }

    public void ActivateTeleportLocomotion()
    {
        xrRig.GetComponent<TeleportationProvider>().enabled = true;
        xrRig.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
        rayIndicatorObject.gameObject.SetActive(true);
        characterControllerDriver.locomotionProvider = teleportProvider;
        teleport.action.performed += TeleportActivate;
        teleport.action.canceled += TeleportCancelled;
    }

    /// <summary>
    /// When trying to teleport. 
    /// </summary>
    /// <param name="context is the current state that we are in"></param>
    private void TeleportActivate(InputAction.CallbackContext context)
    {
        rayInteractor.enabled = true;
    }

    /// <summary>
    /// When stopping to try to teleport. 
    /// </summary>
    /// <param name="context is the current state that we are in"></param>
    private void TeleportCancelled(InputAction.CallbackContext context)
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit ray) && rayInteractor.enabled == true && isTeleporting == false)
        {
            isTeleporting = true;
            TeleportRequest teleportRequest = new TeleportRequest();
            teleportRequest.destinationPosition = ray.point;
            StartCoroutine(Teleport(teleportRequest, 0.5f));
        }
        rayInteractor.enabled = false;
    }

    public IEnumerator Teleport(TeleportRequest request, float teleportDelay)
    {
        //initiate camera fade. 
        TeleportCameraFader(teleportDelay);
        //wait till we are halfway throug the fade before moving player.
        yield return new WaitForSeconds(teleportDelay);
        //Fire teleport request. 
        teleportProvider.QueueTeleportRequest(request);
        isTeleporting = false;
    }

    public void TeleportCameraFader(float fadeDuration)
    {
    }
}
