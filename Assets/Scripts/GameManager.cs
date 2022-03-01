using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject tapToPlay,improvePanel;
    public bool _isStart = false;
    public static GameManager _instance;
    // Start is called before the first frame update
    private void Awake()
    {
        _instance = this;
    }

    void TapToPlay()
    {
       
     
        if (Input.GetMouseButtonDown(0)&&!_isStart&&!EventSystem.current.currentSelectedGameObject)
        {
            tapToPlay.SetActive(false);
            improvePanel.SetActive(false);
            CharacterController._instance.GetAnim().SetBool("isRun",true);
            _isStart = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        TapToPlay();
    }
}
