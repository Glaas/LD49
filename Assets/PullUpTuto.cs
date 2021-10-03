using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullUpTuto : MonoBehaviour
{
    public GameObject tutoPanel;

    private void Awake()
    {
        tutoPanel = GameObject.Find("TutoPanel");
    }
    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            print("pressed");
            tutoPanel.SetActive(tutoPanel.activeInHierarchy ? false : true);
        }
    }
}
