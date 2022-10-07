using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int _health;

    public bool _isDeath;

    private MeshRenderer _bodyMeshRenderer;

    //[SerializeField] private Material _damagedMaterial;

    public GameObject _explosionParticleSystem;

    public GameObject _fireParticleSystem;


    void Start()
    {
        _bodyMeshRenderer = GetComponentInChildren<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        CalculateHealth();
    }

    private void CalculateHealth()
    {
        if (_health == 0)
        {
            _isDeath = true;
        }

        if (_health <= 2)
        {
            //_bodyMeshRenderer.material = _damagedMaterial;

            _fireParticleSystem.SetActive(true);
        }
    }
}
