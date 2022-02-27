using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollactableCollision : MonoBehaviour
{
   
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag!=transform.tag)
        {
            transform.gameObject.SetActive(false);
        }
    }
 
}
