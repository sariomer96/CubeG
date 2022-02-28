using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelStatus : MonoBehaviour
{
    int _currentLevel = 1;
    [SerializeField] Text levelTxt;
    private string _level = "LEVEL ";
    public static LevelStatus _instance;

    public int CurrentLevel{ get { return _currentLevel; } set { _currentLevel = value; } }
    // Start is called before the first frame update

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();      
        _instance = this;
    }
    private void Start()
    {
        _currentLevel = PlayerPrefs.GetInt("currentLevel",1);
    
        levelTxt.text = _level+_currentLevel.ToString();
    }

}
