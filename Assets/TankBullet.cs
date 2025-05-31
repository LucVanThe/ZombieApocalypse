using UnityEngine;

public class TankBullet : MonoBehaviour
{
    public float speed = 5f;
    public GameObject explosionEffect;

    private Vector3 targetPosition;
    private bool isInitialized = false;

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
        isInitialized = true;
        Vector3 dir = (targetPosition - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void Update()
    {
        if (!isInitialized) return;

        
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

       
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Explode();
        }
    }

    void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); 
    }
}
