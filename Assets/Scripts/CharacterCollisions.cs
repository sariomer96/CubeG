using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCollisions : MonoBehaviour
{
    bool isFinished = false;

    [SerializeField] ParticleSystem _hitParticle, _collectParticleBlue, _collectParticleYellow;
    [SerializeField] GameObject _confetti;
    [SerializeField] UIController uiController;
    [SerializeField] Camera cam;
    [SerializeField] Text goldTxtOnLeft,endingCurrentGoldTxt,endingTotalTxt;
    [SerializeField] Vector3 finishCamPos;
    [SerializeField] Vector3 finishCamRot;
    [SerializeField] public Text _healthTxt,_remainingTxt;
    int _remaining = 3;
    CharacterController characterController;
    [SerializeField] private int _health = 3;
    public int Health { get { return _health; } set { _health = value; } }

    [SerializeField] private int _diamondsValueBlue = 1;
    [SerializeField] private int _diamondsValueYellow = 2;
    public int DiaValueBlue { get { return _diamondsValueBlue; } set { _diamondsValueBlue = value; } }
    public int DiaValueYellow { get { return _diamondsValueYellow; } set { _diamondsValueYellow = value; } }
    public static CharacterCollisions _instance;
    Events _events;

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        _health = PlayerPrefs.GetInt("health", 3);
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
        goldTxtOnLeft.text = characterController.DiamondsCount.ToString();

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag=="Finish")
        {
            _confetti.SetActive(true);
          
            uiController.WinUi();
            int getMoney = PlayerPrefs.GetInt("money");
            CoinStatus._instance.Money = characterController.DiamondsCount;
            endingCurrentGoldTxt.text = characterController.DiamondsCount.ToString();
            PlayerPrefs.SetInt("money", getMoney + CoinStatus._instance.Money);
            int totalMoney = PlayerPrefs.GetInt("money");
            CoinStatus._instance.moneyTxt.text = CoinStatus._instance.totalCoin+ totalMoney.ToString();
            endingTotalTxt.text = CoinStatus._instance.totalCoin + totalMoney.ToString();

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
           
                _hitParticle.Play();
            
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
            _collectParticleBlue.Play();
            other.gameObject.SetActive(false);
            _diamondsValueBlue= PlayerPrefs.GetInt("diaBlue",1);
            _events.TriggerCollectDiamonds(_diamondsValueBlue);
            print(characterController.DiamondsCount);
        }
        if (other.tag=="diamondsYellow")
        {
            _collectParticleYellow.Play();
            other.gameObject.SetActive(false);
            _diamondsValueYellow= PlayerPrefs.GetInt("diaYellow",2);
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
            endingCurrentGoldTxt.text = characterController.DiamondsCount.ToString();
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
