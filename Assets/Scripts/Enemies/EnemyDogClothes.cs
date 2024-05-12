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
        Debug.Log("Initialize Clothes");
        Debug.Log("Lvl: " + lvl.ToString());
       
        switch (lvl)
        {
            case 1:
                break;
            case 2:
                EnableClothes2(35, 80, 95);
                break;
            case 3:
                EnableClothes2(25, 40, 90);
                break;
            case 4:
                EnableClothes2(15, 20, 30);
                break;
        }
    }
    
    private void EnableClothes(GameObject[] clothes)
    {
        DisableAllClothes();
        int randomClothes = UnityEngine.Random.Range(0, clothes.Length);
        clothes[randomClothes].SetActive(true);
    }

    private void EnableClothes2(int noClotthesMax, int lvl3Max, int lvl4Max)
    {
        DisableAllClothes();
        int randomClothes = UnityEngine.Random.Range(0, 101);
        Debug.Log("Random Clothes: " + randomClothes.ToString());
        
        // No Clothes
        if (randomClothes < noClotthesMax)
        {
            return;
        }
        
        // lvl2 and lvl3 clothes 
        if (randomClothes > noClotthesMax && randomClothes < lvl3Max)
        {
            int randomClotheLvl = UnityEngine.Random.Range(0, 2);
            if (randomClotheLvl == 0)
            {
                _lvl2Clothes[0].SetActive(true);
            }
            else
            {
                _lvl3Clothes[0].SetActive(true);
            }
            return;
        }
        
        // lvl4 clothes
        if (randomClothes > lvl3Max && randomClothes < lvl4Max)
        {
            int randomClothesLvl = UnityEngine.Random.Range(0, _lvl4Clothes.Length);
            _lvl4Clothes[randomClothesLvl].SetActive(true);
            return;
        }
        
        // lvl4 clothes
        if (randomClothes > lvl4Max && randomClothes < 101)
        {
            int randomClothesLvl = UnityEngine.Random.Range(0, _lvl5Clothes.Length);
            _lvl5Clothes[randomClothesLvl].SetActive(true);
        }
        
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
