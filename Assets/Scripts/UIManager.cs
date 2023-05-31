using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject winPanel, gameOverPanel, gamePanel,startPanel;
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
    }
}
