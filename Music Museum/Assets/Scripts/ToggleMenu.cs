using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleMenu : MonoBehaviour
{
    public InputActionReference playableInstrumentsMenuInput = null;
    public InputActionReference exhibtionInstrumentsMenuInput = null;
    public GameObject playableInstrumentsMenu;
    public GameObject exhibitionInstrumentsMenu;
    public GameObject menuSpawn;

    private GameObject vrCamera;

    private void Awake()
    {
        vrCamera = GameObject.FindGameObjectWithTag("MainCamera");
        playableInstrumentsMenuInput.action.started += TogglePlayableInstruments;
        exhibtionInstrumentsMenuInput.action.started += ToggleExhibitionInstruments;
    }

    private void OnDestroy()
    {
        playableInstrumentsMenuInput.action.started -= TogglePlayableInstruments;
        exhibtionInstrumentsMenuInput.action.started -= ToggleExhibitionInstruments;
    }

    private void TogglePlayableInstruments(InputAction.CallbackContext context)
    {
        bool isActive = !playableInstrumentsMenu.activeSelf;
        playableInstrumentsMenu.SetActive(isActive);

        if (exhibitionInstrumentsMenu.activeSelf)
            exhibitionInstrumentsMenu.SetActive(false);

        SetPosAndRot(playableInstrumentsMenu);
    }

    private void ToggleExhibitionInstruments(InputAction.CallbackContext context)
    {
        bool isActive = !exhibitionInstrumentsMenu.activeSelf;
        exhibitionInstrumentsMenu.SetActive(isActive);

        if (playableInstrumentsMenu.activeSelf)
            playableInstrumentsMenu.SetActive(false);

        SetPosAndRot(exhibitionInstrumentsMenu);
    }

    private void SetPosAndRot(GameObject menu)
    {
        menu.transform.position = new Vector3(menuSpawn.transform.position.x, vrCamera.transform.position.y - 0.3f, menuSpawn.transform.position.z);
        menu.transform.rotation = Quaternion.Euler(-70, vrCamera.transform.rotation.eulerAngles.y, 0);
    }
}
