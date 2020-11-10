using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectManager : MonoBehaviour
{
    public string[] levelTags;

    public GameObject[] locks;
    public bool[] levelUnlocked;

    private void Start()
    {
        for (int i = 0; i < levelTags.Length; i++)
        {
            // The level is not unlocked yet
            if (PlayerPrefs.GetInt(levelTags[i]) == null)
            {
                levelUnlocked[i] = false;
            } else if (PlayerPrefs.GetInt(levelTags[i]) == 0)
            {
                levelUnlocked[i] = false;
            }
            else
            {
                levelUnlocked[i] = true;
            }
            
            // Remove the lock icon
            if (levelUnlocked[i])
            {
                locks[i].SetActive(false);
            }
        }
    }

    private void Update()
    {
        
        
    }
}
