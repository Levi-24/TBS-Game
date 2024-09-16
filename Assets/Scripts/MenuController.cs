using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menu;
    [SerializeField] GameObject missionSeelctScreen;

    public void SelectMission()
    {
        menu.gameObject.SetActive(false);
        missionSeelctScreen.gameObject.SetActive(true);
    }

    public void Exit() { Application.Quit(); }

    public void Map1()
    {
        SceneManager.LoadScene("Map1");
    }

    public void Map2()
    {
        SceneManager.LoadScene("Map2");
    }

    public void Back()
    {
        menu.gameObject.SetActive(true);
        missionSeelctScreen.gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
