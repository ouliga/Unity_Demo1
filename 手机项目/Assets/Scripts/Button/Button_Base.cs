using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class Button_Base : MonoBehaviour
{
    //public Button _button {  get; private set; }
    private void OnEnable()
    {
        Button button = GetComponent<Button>();
        if (button == null)
        {
            button = gameObject.AddComponent<Button>();
        }
        button.onClick.AddListener(OnClick);
    }
    private void OnDisable()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
    }
    public abstract void OnClick();

}
