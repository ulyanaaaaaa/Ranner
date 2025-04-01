using System;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public Action OnClickButton;

    public void Click()
    {
        OnClickButton?.Invoke();
    }
}
