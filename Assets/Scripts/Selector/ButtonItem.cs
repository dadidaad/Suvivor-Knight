using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonItem : MonoBehaviour
{
    [HideInInspector]
    public GameObject spriteObject;
    [HideInInspector]
    public ButtonListControl buttonListControl;
    [HideInInspector]
    public ScriptableObject selectedObject;
    [SerializeField]
    GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        child.GetComponent<Image>().sprite = spriteObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void OnButtonClick()
    {
        buttonListControl.OnButtonClick(selectedObject);
    }
}
