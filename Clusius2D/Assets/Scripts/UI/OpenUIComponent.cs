using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OpenUIComponent : MonoBehaviour
{  
    [SerializeField] GameObject _UIComponent;

    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = this.gameObject.GetComponent<Button>();
        button.onClick.AddListener(ToggleUIComponent);
    }

    public void ToggleUIComponent()
    {
        _UIComponent.SetActive(!_UIComponent.activeSelf);
    }
}
