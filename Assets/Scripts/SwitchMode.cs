using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMode : MonoBehaviour
{
    public enum InteractMode
    {
        MOVING, PLACING
    }
    public InteractMode interactMode = InteractMode.MOVING;
    public TMPro.TextMeshProUGUI controlsText;
    string movingControlsString;
    string placementControls;
    GameObject followCamToggle;
    FreeCam freeCam;
    BlockPlacing blockPlacing;

    private void Awake()
    {
        freeCam = FindObjectOfType<FreeCam>();
        blockPlacing = FindObjectOfType<BlockPlacing>();
        controlsText = GameObject.Find("controlsText").GetComponent<TMPro.TextMeshProUGUI>();
        followCamToggle = GameObject.Find("FollowCam");
    }

    private void Start()
    {
        interactMode = InteractMode.MOVING;
        blockPlacing.enabled = false;
        followCamToggle.SetActive(false);
        DisplayControls();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMode();
        }
    }

    public void ToggleMode()
    {
        AudioSource.PlayClipAtPoint(FindObjectOfType<SFXBrain>().toggleSFX, Vector3.zero);
        switch (interactMode)
        {
            case InteractMode.MOVING:
                interactMode = InteractMode.PLACING;
                followCamToggle.SetActive(true);
                freeCam.enabled = false;
                blockPlacing.enabled = true;
                blockPlacing.CreateViz();
                break;
            case InteractMode.PLACING:
                interactMode = InteractMode.MOVING;
                followCamToggle.SetActive(false);
                freeCam.enabled = true;
                blockPlacing.enabled = false;
                if (blockPlacing.objHeld == null) break;
                Destroy(blockPlacing.objHeld);
                Destroy(blockPlacing.objHeldShadowInstance);
                break;
            default:
                break;
        }
        DisplayControls();

    }
    public void DisplayControls()
    {
        string i = "State : " + (interactMode == InteractMode.MOVING ? "Camera" : "Placing");
        movingControlsString = $"Move : WASD or arrow keys\nAltitude : A/Q or pgUp/pgDown \nMove speed : {freeCam.movementSpeed} (+ and - key to adjust)";
        placementControls = $"Move : WASD or arrow keys\nAltitude : A/Q or pgUp/pgDown \nMove speed : {blockPlacing.movementSpeed} (+ and - key to adjust)\nPlace a block : Spacebar";
        var buffer = interactMode == InteractMode.MOVING ? movingControlsString : placementControls;
        controlsText.text = i + "\n" + buffer;
    }
}
