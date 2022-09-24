using UnityEngine;
using System.Collections;
using System.Linq.Expressions;

public class CollisionController : MonoBehaviour
{
    //[SerializeField] private ParticleSystem fire;
    //[SerializeField] private ParticleSystem hitSmoke;
    //[SerializeField] private ParticleSystem playerFire;

    //[SerializeField]
    //[Range(0, 10)] private float fallRotationSpeed;

    private EnemyController _enemyController;

    public BulletExplosion _bulletExplosion; 

    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("baza");

        AmmoType hitType = other.gameObject.GetComponent<AmmoController>().type;

        // better change to Dict<>
        switch (hitType)
        {
            case AmmoType.Bullet:

                ApplyBulletDamage();
                break;

            case AmmoType.Bomb:

                ApplyBombDamage();
                break;
        }
        Destroy(other.gameObject);
    }

    private void ApplyBulletDamage()
    {
        Debug.LogWarning("Bullet hit");

        _bulletExplosion.ExplodePlane(transform.position);
    }

    private void ApplyBombDamage()
    {
        Debug.LogWarning("Bomb hit");
    }    

    //private void UpdateState()
    //{
    //    if (isDeath)
    //    {
    //        _enemyController.enabled = false;

    //        // explosion experiment

    //        foreach (Transform item in children)
    //        {
    //            item.gameObject.AddComponent<BoxCollider>();
    //        }

    //        StartCoroutine(EnableGravity());

    //        isDeath = false;
    //    }

    //    float lowerBorder = Screen.width / -154;

    //    if (transform.position.y < lowerBorder)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //private IEnumerator EnableGravity()
    //{
    //    yield return new WaitForSeconds(3f);
    //}
}