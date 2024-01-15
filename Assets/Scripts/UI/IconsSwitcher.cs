using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconsSwitcher : MonoBehaviour
{
    [SerializeField] private List<GameObject> _buttons1;
    [SerializeField] private List<GameObject> _buttons2;
    [SerializeField] private List<GameObject> _buttons3;
    private int _currentButton = 0;

    public void SwitchToNextButton()
    {
        _currentButton++;
        if (_currentButton == 4)
        {
            _currentButton = 0;
        }

        for (int i = 0; i < _buttons1.Count; i++)
        {
            _buttons1[i].SetActive(false);
            _buttons2[i].SetActive(false);
            _buttons3[i].SetActive(false);
        }
        _buttons1[_currentButton].SetActive(true);
        _buttons2[_currentButton].SetActive(true);
        _buttons3[_currentButton].SetActive(true);
    }
}
