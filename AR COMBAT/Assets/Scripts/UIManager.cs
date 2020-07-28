using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void QuitApp()
    {

        Application.Quit();

    }

    public void GameScene()
    {

        SceneManager.LoadScene("GameScene");

    }

    public void ManuScene()
    {

        SceneManager.LoadScene("Menu_Scene");

    }


}
