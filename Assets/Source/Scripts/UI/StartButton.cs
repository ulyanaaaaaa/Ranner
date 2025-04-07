using System;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public Action OnStart;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => 
        {
            OnStart?.Invoke();
        });
    }

    public void Exit()
    {
        Application.Quit();
    }
}
