using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float maxSwerve = 0.03f;
    int triggerCounter = 0;
    [SerializeField] float _speed;
    [SerializeField] Vector3 move;
    [SerializeField] float poolTriggerZ;
    ObjectPool objPool;
    Rigidbody rb;
    int poolSize = 0;
    int activeObjCount = 0;
    int typeCount = 0;
    float lastMousePosition;
    float deltaMousePosition;
    Vector2 currentMousePosition;
    // Start is called before the first frame update
    void Start()
    {
        objPool = ObjectPool._instance;
        typeCount = objPool.GetPoolTypeCount();
        
        rb = transform.GetComponent<Rigidbody>();

        ObjectCreate();


    }
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 prevMousePosition = Input.mousePosition;
                
                lastMousePosition += prevMousePosition.x;
               
            }


            if (Input.GetMouseButton(0))
            {
                 currentMousePosition =Input.mousePosition;
              
          
            }
              deltaMousePosition  = lastMousePosition - currentMousePosition.x;
     

            float swerveAmount = -deltaMousePosition*Time.deltaTime*500;

            swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerve, maxSwerve);
            //transform.Translate(swerveAmount, 0, 0);
            rb.velocity = new Vector3(-swerveAmount,0,move.z*_speed);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.3f, 2.3f), transform.position.y, transform.position.z);
        }
       
        if (Input.GetMouseButtonUp(0))
        {
            deltaMousePosition = 0;
            lastMousePosition = 0;

        }


        ////rb.velocity = new Vector3((-(deltaMousePosition)) * Time.fixedDeltaTime, 0, 1);
        //rb.velocity = move * _speed;
        rb.velocity= move * _speed;
    }

    void ObjectCreate()
    {
        typeCount = objPool.GetPoolTypeCount();


        for (int j = 0; j < typeCount; j++)
        {
            poolSize = objPool.GetPoolSize(j);
            activeObjCount = objPool.GetActiveObjCount(j);
            for (int i = 0; i < activeObjCount; i++)
            {
                
                GameObject pooledObjs = objPool.GetPooledObject(j);
            
                objPool.GetRandomX(j);
                pooledObjs.transform.position =objPool.GetPos(j);
                objPool.IncreasePosZ(j);
                if (pooledObjs.transform.position.z>95) //clamp platform
                {
                    pooledObjs.SetActive(false);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "cube")
        {
            triggerCounter++;
            if (triggerCounter < 4)
            {
                poolTriggerZ = other.transform.position.z + poolTriggerZ;
                other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, poolTriggerZ);
            }

         
      
            for (int i = 0; i <typeCount; i++)
            {
                objPool.IncreasePosZ(i);
            }
         
            ObjectCreate();


        }
    }
}
