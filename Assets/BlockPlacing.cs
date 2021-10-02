using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacing : MonoBehaviour
{
    public GameObject block, blockViz;
    public GameObject objHeld;
    SwitchMode switchMode;
    FreeCam freeCam;
    bool followCursor = false;

    private void Awake()
    {
        switchMode = FindObjectOfType<SwitchMode>();
        freeCam = FindObjectOfType<FreeCam>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (switchMode.interactMode)
        {
            case SwitchMode.InteractMode.MOVING:
                break;
            case SwitchMode.InteractMode.PLACING:
                MoveCube();
                InstantiateCube();
                break;
            default:
                break;
        }

    }
    public void CreateViz()
    {
        if (objHeld == null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                objHeld = GameObject.Instantiate(blockViz, objectHit.transform.position, Quaternion.identity);
                objHeld.transform.position = (Camera.main.transform.position + (Camera.main.transform.forward * 3));
                Transform tr = Camera.main.transform;
                objHeld.transform.forward = tr.forward;
                objHeld.transform.right = tr.right;

            }
        }
    }
    void MoveCube()
    {

        // objHeld.transform.rotation = Quaternion.identity;

        var movementSpeed = freeCam.movementSpeed;


        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            objHeld.transform.position = objHeld.transform.position + (-objHeld.transform.right * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            objHeld.transform.position = objHeld.transform.position + (objHeld.transform.right * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            objHeld.transform.position = objHeld.transform.position + (objHeld.transform.forward * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            objHeld.transform.position = objHeld.transform.position + (-objHeld.transform.forward * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            objHeld.transform.position = objHeld.transform.position + (objHeld.transform.up * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            objHeld.transform.position = objHeld.transform.position + (-objHeld.transform.up * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.PageUp))
        {
            objHeld.transform.position = objHeld.transform.position + (Vector3.up * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.PageDown))
        {
            objHeld.transform.position = objHeld.transform.position + (-Vector3.up * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Comma) || Input.mouseScrollDelta.y == -1)
        {
            objHeld.transform.Rotate((Vector3.up * movementSpeed * 10) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Period) || Input.mouseScrollDelta.y == 1)
        {
            objHeld.transform.Rotate((-Vector3.up * movementSpeed * 10) * Time.deltaTime);
        }
        if (followCursor) Camera.main.transform.LookAt(objHeld.transform);
    }
    void InstantiateCube()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Instantiate(block, objHeld.transform.position, objHeld.transform.rotation);
        }

    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(30, 80, 100, 35), "Grab Block"))
        {
            objHeld = blockViz;
        }
        followCursor = GUI.Toggle(new Rect(30, 120, 200, 100), followCursor, "Camera follow cursor");
    }
}
