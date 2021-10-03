using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullUpTuto : MonoBehaviour
{
    public GameObject tutoPanel;
    public TMPro.TextMeshProUGUI unlockable;
    private void Awake()
    {
        tutoPanel = GameObject.Find("TutoPanel");
        unlockable = GameObject.Find("Unlockable").GetComponent<TMPro.TextMeshProUGUI>();

    }
    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Backspace))
        {
            tutoPanel.SetActive(tutoPanel.activeInHierarchy ? false : true);
        }

        if (FindObjectOfType<ScoreKeeper>().maxScore > 200)
        {
            unlockable.text = "Congrats, you unlocked the plank!\n\nIn Placement mode : \n\t1 : Block\n\t2 : Plank";
        }
    }
}
