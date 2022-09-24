using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    public float radius;
    public float force;

    public Collider[] colliders;
    public void ExplodePlane(Vector3 forceStartPos)
    {
        Debug.Log("vzriv");

        colliders = Physics.OverlapSphere(forceStartPos, radius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddExplosionForce(force, forceStartPos, radius);
            }
        }
    }
}
