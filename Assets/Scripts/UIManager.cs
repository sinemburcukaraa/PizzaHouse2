using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject winPanel, gameOverPanel, gamePanel,startPanel;
    public int chairCount;
    public int fieldCount;
    public int counterCount;
    public int FieldAreaCount;
    public int mushroomAreaCount;    
    public int pepperAreaCount;
    public int PineappleAreaCount;

    private void Awake()
    {
        if (!instance)
            instance = this;
    }
    public void OpenWinPanel()
    {
        winPanel.gameObject.SetActive(true);

    }
    public void OpengameOverPanel()
    {
        gameOverPanel.gameObject.SetActive(true); 

    }
    public void OpengamePanel()
    {
        gamePanel.gameObject.SetActive(true);
    }

    public void OpenStartPanel()
    {
        startPanel.SetActive(false);
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        winPanel.SetActive(false);
        PlayerPrefs.DeleteAll();
    }
}
