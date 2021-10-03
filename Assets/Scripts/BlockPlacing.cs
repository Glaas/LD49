using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacing : MonoBehaviour
{
    public GameObject block, blockViz, shadowPrefab;
    public GameObject plankPrefab;
    public GameObject plankViz;

    public GameObject itemChosen;

    public GameObject objHeld, objHeldShadowInstance;
    SwitchMode switchMode;
    FreeCam freeCam;
    public float movementSpeed = 10;

    bool followCursor = false;

    private void Awake()
    {
        switchMode = FindObjectOfType<SwitchMode>();
        freeCam = FindObjectOfType<FreeCam>();

    }
    // Start is called before the first frame update
    void Start()
    {
        itemChosen = blockViz;

    }

    // Update is called once per frame
    void Update()
    {

        MoveCube();
        InstantiateCube();



    }
    void SwitchObject()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            itemChosen = blockViz;
            CreateViz();
            print("Chose block");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            itemChosen = plankViz;
            CreateViz();
            print("Chose plank");
        }
    }
    public void CreateViz()
    {
        Destroy(objHeld);
        Destroy(objHeldShadowInstance);
        if (objHeld == null)
        {
            if (itemChosen == null) itemChosen = blockViz;

            objHeld = GameObject.Instantiate(itemChosen, Camera.main.transform.forward, Quaternion.identity);

            objHeld.transform.position = (Camera.main.transform.position + (Camera.main.transform.forward * 3));
            Transform tr = Camera.main.transform;
            objHeld.transform.forward = tr.forward;
            objHeld.transform.right = tr.right;

            objHeldShadowInstance = GameObject.Instantiate(shadowPrefab, objHeld.transform.position + (Vector3.down), shadowPrefab.transform.rotation);

        }
    }

    void MoveCube()
    {

        if (objHeld == null) CreateViz();
        SwitchObject();


        if (Input.GetKeyDown(KeyCode.F))
        {
            followCursor = followCursor ? false : true;
            FindObjectOfType<UnityEngine.UI.Toggle>().isOn = followCursor;
        }
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            movementSpeed /= 1.2f;
            FindObjectOfType<SwitchMode>().DisplayControls();
        }
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            movementSpeed *= 1.2f;
            FindObjectOfType<SwitchMode>().DisplayControls();
        }

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
            if (objHeld.transform.position.y <= 0.5f) return;
            objHeld.transform.position = objHeld.transform.position + (-objHeld.transform.up * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.PageUp))
        {
            //objHeld.transform.position = objHeld.transform.position + (Vector3.up * movementSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.PageDown))
        {
            //objHeld.transform.position = objHeld.transform.position + (-Vector3.up * movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Comma) || Input.mouseScrollDelta.y == -1)
        {
            objHeld.transform.Rotate((Vector3.up * movementSpeed * 10) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Period) || Input.mouseScrollDelta.y == 1)
        {
            objHeld.transform.Rotate((-Vector3.up * movementSpeed * 10) * Time.deltaTime);
        }

        if (objHeld == null) return;
        Ray ray = new Ray(objHeld.transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            hitpos = hit.point;

        }
        objHeldShadowInstance.transform.position = hitpos + (Vector3.up / 100);// + new Vector3(0, .001f, 0);


        if (followCursor) Camera.main.transform.LookAt(objHeld.transform);
    }
    public Vector3 hitpos;
    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(hitpos, 1);
    }
    void InstantiateCube()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (objHeld.GetComponent<OverlapCheck>().isOverlapping) return;
            GameObject.Instantiate(itemChosen == blockViz ? block : plankPrefab, objHeld.transform.position, objHeld.transform.rotation);
            AudioSource.PlayClipAtPoint(FindObjectOfType<SFXBrain>().placedSFX, Vector3.zero);

            Destroy(objHeld);
            Destroy(objHeldShadowInstance);
        }

    }
}
