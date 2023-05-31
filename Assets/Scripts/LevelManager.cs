using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public GameObject LevelP; 
    public TextMeshProUGUI LevelCountText;
    int nextLevelCount;

    private void Awake()
    {
        if (!instance)
            instance = this;

    }
 
    private void Start()
    {
        for (int i = 0; i < LevelP.transform.childCount; i++)
        {
            LevelP.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void LevelTextControl()
    {
        int levelTextCount = PlayerPrefs.GetInt("LevelCount") + 1;
        LevelCountText.text = "LEVEL " + levelTextCount;

    }
    public void NextLevel()
    {
        if (LevelP.transform.childCount <= PlayerPrefs.GetInt("nextLevel"))
        {
            PlayerPrefs.SetInt("nextLevel", 0);
            nextLevelCount = PlayerPrefs.GetInt("nextLevel");
        }
        else
        {
            nextLevelCount = PlayerPrefs.GetInt("nextLevel");
            
        }
        LevelP.transform.GetChild(nextLevelCount).gameObject.SetActive(true);
    }


}
