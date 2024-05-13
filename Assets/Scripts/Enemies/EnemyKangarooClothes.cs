using UnityEngine;

public class EnemyKangarooClothes : MonoBehaviour
{
    [SerializeField] private GameObject[] _boots;
    [SerializeField] private GameObject[] _shirts;
    
    public void Initialize(int lvl)
    {
        DisableAllClothes();
        if (lvl > 1)
        {
            int randomClothes = Random.Range(0, 101);
            if (randomClothes > 50)
            {
                int randomBoot = Random.Range(0, _boots.Length);
                _boots[randomBoot].SetActive(true);
            }
            if (randomClothes > 75)
            {
                int randomShirt = Random.Range(0, _shirts.Length);
                _shirts[randomShirt].SetActive(true);
            }
        }
    }
    
    private void DisableAllClothes()
    {
        foreach (var boot in _boots)
        {
            boot.SetActive(false);
        }
        foreach (var shirt in _shirts)
        {
            shirt.SetActive(false);
        }   
    }
}
