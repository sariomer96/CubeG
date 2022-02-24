using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] Vector3 move;
   
    [SerializeField] float posX, posY, posZ;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        for (int i = 0; i <15; i++)
        {
            GameObject pooledObj = ObjectPool._instance.GetPooledObject(0);
            float posx = Random.RandomRange(-1.8f, 2);
            posX = posx;
            pooledObj.transform.position = new Vector3(posX, posY, posZ);
            posZ+=2;
        }
 
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = move * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="cube")
        {
            print("hit");
            posZ += 7;
            for (int i = 0; i < 15; i++)
            {
                GameObject pooledObj = ObjectPool._instance.GetPooledObject(0);
                float posx = Random.RandomRange(-1.8f, 2);
                posX = posx;
                pooledObj.transform.position = new Vector3(posX, posY, posZ);
                posZ += 2;
            }
        }
    }
}
