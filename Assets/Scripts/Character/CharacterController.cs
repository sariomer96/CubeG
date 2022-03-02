using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{

    [SerializeField] Transform _player;

    public bool canSwerve = true;
    [SerializeField] float maxSwerve = 1f;
    int triggerCounter = 0;
    [SerializeField] private float _speed;
   
    [SerializeField] private int _diamondsCount;
    public float Speed { get { return _speed; } set { _speed = value; } }
    public int DiamondsCount { get { return _diamondsCount; } set { _diamondsCount = value; } }
    [SerializeField] Vector3 move;
    [SerializeField] float poolTriggerZ;
    ObjectPool objPool;

    int poolSize = 0;
    int activeObjCount = 0;
    int typeCount = 0;
    [SerializeField] private Animator _anim;
   
  
    float lastPosX;
    float moveX;
    private string _idleAnim = "idle";
    public static CharacterController _instance;
    [SerializeField] float swerveSpeed = 5;
    [SerializeField] float ClampX;

    private void Awake()
    {
        _instance = this;
    }
    public void SetSpeed(float speed)
    {
        Speed = speed;
    }
    
    // Start is called before the first frame update
    void Start()
    {
      
        _anim.Play(_idleAnim);

        objPool = ObjectPool._instance;
        typeCount = objPool.GetPoolTypeCount();
       
        ObjectCreate();


    }
  
    public Animator GetAnim()
    {
        return _anim;
    }

   void CharacterMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPosX = Input.mousePosition.x;
        }

        else if (Input.GetMouseButton(0))
        {

            moveX = Input.mousePosition.x - lastPosX;

            lastPosX = Input.mousePosition.x;

        }

        else if (Input.GetMouseButtonUp(0))
        {
            moveX = 0f;

        }

        if (canSwerve==true&&GameManager._instance._isStart==true)
        {
            float swerveAmount = moveX * Time.deltaTime * swerveSpeed;
          
            transform.Translate(swerveAmount, 0, _speed * Time.deltaTime);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -ClampX, ClampX), transform.position.y, transform.position.z);
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        CharacterMove();       
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
