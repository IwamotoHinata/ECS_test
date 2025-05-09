using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AvatorURLRemainder : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TMP_Dropdown _dropDown;
    [SerializeField] private Toggle _toggle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _toggle.onValueChanged.AddListener(ChangeField);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeField(bool value)
    {
        if (value)
        {
            _inputField.gameObject.SetActive(true);
            _dropDown.gameObject.SetActive(false);
        }
        else
        {
            _inputField.gameObject.SetActive(false);
            _dropDown.gameObject.SetActive(true);
        }
    }
}
