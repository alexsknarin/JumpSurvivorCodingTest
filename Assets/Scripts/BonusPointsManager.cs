using UnityEngine;

public class BonusPointsManager : MonoBehaviour
{
    [SerializeField] private IntVariable _bonusPoints;
    [SerializeField] private int _kangarooBonus;

    private void OnEnable()
    {
        PlayerCollisionHandler.OnBonusCollided += AddBonusPoints;
    }

    private void OnDisable()
    {
        PlayerCollisionHandler.OnBonusCollided -= AddBonusPoints;
    }

    private void Start()
    {
        _bonusPoints.Value = 0;
    }

    private void AddBonusPoints(int enemy, Vector3 collisionPosition)
    {
        if (enemy == 0)
        {
            _bonusPoints.Value += _kangarooBonus;
        }
    }
}
