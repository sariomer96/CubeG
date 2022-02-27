using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] Camera mainCam;
    private Vector3 lastMousePos=Vector3.zero;
    private Vector3 mousePos;
    private Vector3 newPos;
    [SerializeField] float _speed = 10;
    [SerializeField] float _swipeSpeed = 5;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos.x = Input.mousePosition.x;
        }
        if (Input.GetMouseButton(0))
        {
            mousePos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,18f));
            float dif = mousePos.x - lastMousePos.x;
            print(dif);
            dif = Mathf.Clamp(dif, -0.15f, 0.15f);
            if (Mathf.Abs(dif)>0.1f)
            {
                transform.Translate(dif, 0, 0);
            }
        
            //newPos = new Vector3(transform.position.x + dif * Time.fixedDeltaTime * _swipeSpeed, transform.position.y, transform.position.z);
            //transform.position = newPos + transform.forward * _speed * Time.deltaTime;
            lastMousePos = mousePos;
        }
        if (Input.GetMouseButtonUp(0))
        {
            lastMousePos = Vector3.zero;
        }
        rb.velocity = new Vector3(0, 0, 1) * _speed;
    }
}
