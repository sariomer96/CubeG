using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollactableCollision : MonoBehaviour
{
   
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag=="obstacle")
        {
            transform.gameObject.SetActive(false);
        }
    }
 
}
