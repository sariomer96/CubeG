using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject win, lose;
    // Start is called before the first frame update
  

    public void LoseUi()
    {
        _panel.SetActive(true);
        lose.SetActive(true);
    }
    public void WinUi()
    {
        _panel.SetActive(true);
        win.SetActive(true);
    }
    public void ClickFinishLevelUp() // load  scene onClick
    {
        LevelStatus._instance.CurrentLevel++;
        PlayerPrefs.SetInt("currentLevel", LevelStatus._instance.CurrentLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Update is called once per frame
  
}
