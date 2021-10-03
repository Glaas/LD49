using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapCheck : MonoBehaviour
{
   public Material transparentNormal, transparentWarning;

   public bool isOverlapping;

   private void OnTriggerStay(Collider other) {
       isOverlapping = true;
       GetComponent<MeshRenderer>().material = transparentWarning;
   }
   private void OnTriggerExit(Collider other) {
       isOverlapping = false;
       GetComponent<MeshRenderer>().material = transparentNormal;
   }

}
