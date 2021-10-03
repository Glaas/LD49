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

    FreeCam freeCam;
    BlockPlacing blockPlacing;

    private void Awake()
    {
        freeCam = FindObjectOfType<FreeCam>();
        blockPlacing = FindObjectOfType<BlockPlacing>();
    }

    private void Start()
    {
        interactMode = InteractMode.MOVING;
        blockPlacing.enabled = false;
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
        switch (interactMode)
        {
            case InteractMode.MOVING:
                interactMode = InteractMode.PLACING;
                freeCam.enabled = false;
                blockPlacing.enabled = true;
                blockPlacing.CreateViz();
                break;
            case InteractMode.PLACING:
                interactMode = InteractMode.MOVING;
                freeCam.enabled = true;
                blockPlacing.enabled = false;
                if (blockPlacing.objHeld == null) break;
                Destroy(blockPlacing.objHeld);
                Destroy(blockPlacing.objHeldShadowInstance);
                break;
            default:
                break;
        }

    }
    private void OnGUI()
    {
        string typedisplay = interactMode == InteractMode.MOVING ? "Moving" : "Placing";
        GUI.Label(new Rect(30, 50, 200, 100), $"Interaction type : {typedisplay}");
    }
}
