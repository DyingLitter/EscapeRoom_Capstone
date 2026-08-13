using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScreenResChange : MonoBehaviour
{
    public TMP_Dropdown ResDrop;
    public Toggle FullTogg;

    Resolution[] AllResolutions;
    bool isFullScreen;
    int SelectedRes;
    List<Resolution> SelectedResList = new List<Resolution>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        ResDrop.value = SelectedRes;
        isFullScreen = true;
        AllResolutions = Screen.resolutions;

        List<string> ResOptions = new List<string>();
        string newRes;
        foreach (Resolution res in AllResolutions)
        {
            newRes = res.width.ToString() + " x " + res.height.ToString();
           
            if(!ResOptions.Contains(newRes))
            {
              ResOptions.Add(newRes);
              SelectedResList.Add(res);  
            }
            
        }

        ResDrop.AddOptions(ResOptions);
    }

    public void ChangeResolution()
    {
        SelectedRes = ResDrop.value;
        Screen.SetResolution(SelectedResList[SelectedRes].width, SelectedResList[SelectedRes].height, isFullScreen);
    }

    public void ChangeFullScreen()
    {
        isFullScreen = FullTogg.isOn;
        Screen.SetResolution(SelectedResList[SelectedRes].width, SelectedResList[SelectedRes].height, isFullScreen);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
