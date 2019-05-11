using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public RectTransform controlsPanel;

    public void Continue()
    {
        SceneManager.LoadScene(1);
    }

    public void NewGame()
    {
        if (PlayerPrefs.HasKey("SceneOne")) PlayerPrefs.DeleteKey("SceneOne");
        SceneManager.LoadScene(1);
    }

    public void ControlsOpen()
    {
        controlsPanel.gameObject.SetActive(true);
    }

    public void ControlsClosed()
    {
        controlsPanel.gameObject.SetActive(false);
    }
}
