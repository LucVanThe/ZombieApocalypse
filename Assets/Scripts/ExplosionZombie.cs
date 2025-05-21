using UnityEngine;

public class ExplosionZombie : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject ExploPrefabs;
    private void CreateExplosion()
    {
        Instantiate(ExploPrefabs, transform.position, Quaternion.identity);
    }
    protected override void Die()
    {
        CreateExplosion();
        base.Die();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CreateExplosion();
        }
    }
}
