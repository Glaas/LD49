using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClass : MonoBehaviour
{
    public float xAngle, yAngle, zAngle;
    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator rotate()
    {

        transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
        yield return new WaitForSeconds(0.5f);
        var col = new Color(Random.Range(0f, .1f), Random.Range(0f, .1f), Random.Range(0f, .1f));
        GetComponent<MeshRenderer>().material.color = col;
        StartCoroutine(nameof(rotate));


    }
    // Update is called once per frame
    void Update()
    {

    }
}
