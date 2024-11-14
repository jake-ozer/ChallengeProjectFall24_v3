using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private MainInput input;
    private bool paused = false;
    [SerializeField] private GameObject menuObj;
    private GameObject crosshairGO;

    private void Awake()
    {
        input = new MainInput();
        menuObj.SetActive(false);
        crosshairGO = GameObject.Find("Crosshair");
    }

    // --------- enable/disbale input when script toggled on/off -------------- 
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
    // ------------------------Do not touch this------------------------------

    private void Update()
    {
        if (input.Ground.Pause.triggered)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        paused = !paused;
        menuObj.SetActive(paused);
        FindObjectOfType<PlayerShoot>().enabled = !paused;
        FindObjectOfType<PlayerSlide>().enabled = !paused;
        FindObjectOfType<PlayerCamera>().enabled = !paused;
        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = paused;
        crosshairGO.SetActive(!paused);
        Time.timeScale = paused ? 0 : 1;
    }
}
