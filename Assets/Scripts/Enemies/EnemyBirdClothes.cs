using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBirdClothes : MonoBehaviour
{
    [SerializeField] private GameObject[] _pants;
    
    public void Initialize(int lvl)
    {
        DisableAllClothes();
        if (lvl > 1)
        {
            int randomClothes = Random.Range(0, 101);
            if (randomClothes > 65)
            {
                int randomPants = Random.Range(0, _pants.Length);
                _pants[randomPants].SetActive(true);
            }
        }
    }
    
    private void DisableAllClothes()
    {
        foreach (var pant in _pants)
        {
            pant.SetActive(false);
        }
    }
}
