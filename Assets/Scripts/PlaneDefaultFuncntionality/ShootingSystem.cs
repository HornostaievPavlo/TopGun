using UnityEngine;

[RequireComponent(typeof(GameEntity))]
public class ShootingSystem : MonoBehaviour
{
    private PlaneType _planeType;

    public Transform _bulletSpawner;

    public Transform _bombSpawner;

    public GameObject _bulletPrefab;

    public GameObject _bombPrefab;

    private void Start()
    {
        _planeType = GetComponent<GameEntity>()._type;

        AssignShootingSystem();
    }

    private void AssignShootingSystem()
    {
        switch (_planeType)
        {
            case PlaneType.Player:

                Debug.Log("We have a player");

                gameObject.AddComponent<PlayerShootingSystem>();

                break;

            case PlaneType.Shooter_Enemy:

                Debug.Log("We have a shooter enemy");

                gameObject.AddComponent<EnemyShootingSystem>();

                break;
        }
    }
}