using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void TriggerMenuBehavior(int i)
    {
        switch (i)
        {
            case 2:
                SceneManager.LoadScene("Menu");
                break;
            default:
            case 0:
                SceneManager.LoadScene("level");
                break;
            case 1:
                Application.Quit();
                break;
        }
    }

}
