using UnityEngine;

public class Medkit : MonoBehaviour
{
    [SerializeField] private GameObject _medkit;

    public void Init()
    {
        _medkit.SetActive(true);
    }
}
