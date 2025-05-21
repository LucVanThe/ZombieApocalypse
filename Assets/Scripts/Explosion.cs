using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float damage = 30f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                Debug.Log("phat hien plAYER");
                player.TakeDamage(damage);
            }
            else
            {
                Debug.Log(" ko phat hien plAYER");
            }
        }

        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}
