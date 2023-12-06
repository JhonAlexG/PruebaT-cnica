using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuFunctions : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ClicklStart(){
        SceneManager.LoadScene("GameMain");
    }

    public void ClickExit(){
        Application.Quit();
    }
}
