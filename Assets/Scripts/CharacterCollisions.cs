using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCollisions : MonoBehaviour
{
    bool isFinished = false;

    [SerializeField] UIController uiController;
    [SerializeField] Camera cam;
    [SerializeField] Vector3 finishCamPos;
    [SerializeField] Vector3 finishCamRot;
    [SerializeField] Text _healthTxt,_remainingTxt;
    int _remaining = 3;
    CharacterController characterController;
    [SerializeField] private int _health = 3;
    private int _diamondsValueBlue = 1;
    private int _diamondsValueYellow = 2;

    Events _events;
    // Start is called before the first frame update
    void Start()
    {
        _healthTxt.text = _health.ToString();
       _events = Events._instance;
        characterController = CharacterController._instance;
        _events.onTriggerColllectDiamonds += OnCollectDiamonds;
        _events.damageHealth += HealthDecrease;
    }

    private void Update()
    {
        FinishCamView(isFinished);
    }
    void FinishCamView(bool isFinished)
    {
        if (isFinished==true)
        {
            cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition,finishCamPos, Time.deltaTime * 2);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation,Quaternion.Euler(finishCamRot), Time.deltaTime * 2);
            
        }

    }
    private void HealthDecrease()
    {
        if (_health>0)
        {
            _health--;
            _healthTxt.text = _health.ToString();
        }
        
    }
    private void OnCollectDiamonds(int diamondsValue)
    {
        characterController.DiamondsCount+=diamondsValue;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Finish")
        {
            uiController.WinUi();
            characterController.Money = characterController.DiamondsCount;
            PlayerPrefs.SetInt("money", characterController.Money);
            print(characterController.Money);
            isFinished = true;
            characterController.SetSpeed(0);
            characterController.canSwerve = false;
            characterController.GetAnim().SetBool("isWin", true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="obstacle")
        {
            StartCoroutine(RespawnCharacter());
            _events.DamageHealth();
            characterController.canSwerve = false;
            characterController.GetAnim().SetBool("isRun", false);
            characterController.GetAnim().SetBool("isRespawn", false);
            characterController.GetAnim().SetBool("isFall", true);
            characterController.SetSpeed(0);
            
         
            other.enabled = false;
        }
        if (other.tag=="diamonds")
        {
            other.gameObject.SetActive(false);
            _events.TriggerCollectDiamonds(_diamondsValueBlue);
            print(characterController.DiamondsCount);
        }
        if (other.tag=="diamondsYellow")
        {
           
            other.gameObject.SetActive(false);
            _events.TriggerCollectDiamonds(_diamondsValueYellow);
            print(characterController.DiamondsCount);
        }
    }
   
    IEnumerator RespawnCharacter()
    {
        if (_health>0)
        {
            
            yield return new WaitForSeconds(1.8f);
            characterController.GetAnim().SetBool("isRespawn", true);
            StartCoroutine(RemainingCounter());
            transform.position += new Vector3(0, 0, 2);
        }
        else
        {
            uiController.LoseUi();
        }
      
    }
    IEnumerator RemainingCounter()
    {
        
        int remaining = _remaining;
       
        while (remaining> 0)
        {
            _remainingTxt.text = remaining.ToString();
            remaining--;

            yield return new WaitForSeconds(1f);
        }
        _remainingTxt.text = "";
        
        Recover();
    }
    void Recover()
    {
        characterController.SetSpeed(5);
        characterController.canSwerve = true;
        characterController.GetAnim().SetBool("isRun", true);
        characterController.GetAnim().SetBool("isFall", false);
        characterController.GetAnim().SetBool("isRespawn", false);
    }
    private void OnDestroy()
    {
        Events._instance.onTriggerColllectDiamonds -= OnCollectDiamonds;
    }
}
