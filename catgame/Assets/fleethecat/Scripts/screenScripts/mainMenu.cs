using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
   public void PlayGame ()
    {
        SceneManager.LoadScene("Playground");
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

    public void GoToMainMenu ()
    {
        SceneManager.LoadScene("startscreen");
    }

    public void GoToCredits ()
    {
        SceneManager.LoadScene("creditscreen");
    }

}
