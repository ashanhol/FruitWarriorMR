using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour, IInputClickHandler{

    public void OnInputClicked(InputEventData eventData)
    {
        SceneManager.LoadScene("Main");
    }

    void OnStart()
    {
        SceneManager.LoadScene("Main");
    }

}
