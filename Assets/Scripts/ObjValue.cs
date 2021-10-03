using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ObjValue : MonoBehaviour
{
    [SerializeField]
    private int baseValue;
    public long computedValue;

    public bool isColliding = false;

    public List<GameObject> inContactWith;

    private void Start()
    {
        name = "Cube " + FindObjectsOfType<Transform>().Length;
    }

    private void Update()
    {
        if (inContactWith.Count < 2)
        {
            computedValue = 0;
            return;
        }
        computedValue = Mathf.RoundToInt(baseValue * transform.position.y);
    }

    private void OnCollisionEnter(Collision other)
    {
        isColliding = true;

    }
    private void OnCollisionExit(Collision other)
    {
        isColliding = false;
        computedValue = 0;

    }

    private void FixedUpdate()
    {
        inContactWith.Clear();

        var col = GetComponent<BoxCollider>();
        Collider[] results = Physics.OverlapBox(transform.position, (col.size / 2) + new Vector3(.1f, .1f, .1f), transform.rotation);
        foreach (var item in results)
        {
            if (item.name == this.name) continue;
            inContactWith.Add(item.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        var col = GetComponent<BoxCollider>();

        Gizmos.color = new Color(1, 0, 0, .2f);
        Gizmos.DrawCube(transform.position, col.size + Vector3.one * 0.1f);
    }

}
