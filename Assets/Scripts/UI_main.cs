using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_main : MonoBehaviour
{

    public void OnMouseUp()
    {
        Debug.Log("asdasdas");
        SceneManager.LoadScene("SampleScene");
    }

    public void ExitGame(){
        Debug.Log("quit");
        Application.Quit();
    }
}
