using System;
using UnityEngine;

public class EnemyDogClothes : MonoBehaviour
{
    [SerializeField] private GameObject[] _lvl2Clothes;
    [SerializeField] private GameObject[] _lvl3Clothes;
    [SerializeField] private GameObject[] _lvl4Clothes;
    [SerializeField] private GameObject[] _lvl5Clothes;

    public void Initialize(int lvl)
    {
        DisableAllClothes();
        switch (lvl)
        {
            case 1:
                break;
            case 2:
                EnableClothes(_lvl2Clothes);
                break;
            case 3:
                EnableClothes(_lvl3Clothes);
                break;
            case 4:
                EnableClothes(_lvl4Clothes);
                break;
            case 5:
                EnableClothes(_lvl5Clothes);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void EnableClothes(GameObject[] clothes)
    {
        DisableAllClothes();
        int randomClothes = UnityEngine.Random.Range(0, clothes.Length);
        clothes[randomClothes].SetActive(true);
    }
    
    private void DisableAllClothes()
    {
        foreach (var clothes in _lvl2Clothes)
        {
            clothes.SetActive(false);
        }
        foreach (var clothes in _lvl3Clothes)
        {
            clothes.SetActive(false);
        }
        foreach (var clothes in _lvl4Clothes)
        {
            clothes.SetActive(false);
        }
        foreach (var clothes in _lvl5Clothes)
        {
            clothes.SetActive(false);
        }
    }
    
}
