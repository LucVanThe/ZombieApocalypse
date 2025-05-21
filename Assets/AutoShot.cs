using UnityEngine;

public class AutoShot : MonoBehaviour
{
    private Animator animator;
    public float shootRange = 8f;         
    public LayerMask enemyLayer;          
    public float fireCooldown = 0.3f;
    [SerializeField] private Transform firepoint;
    [SerializeField] private float speedbullet = 10f;
    private float lastFireTime;
    [SerializeField] GameObject bulletPrefabs;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Time.time - lastFireTime >= fireCooldown)
        {
            //quet xung quanh player
            Collider2D enemy = Physics2D.OverlapCircle(transform.position, shootRange, enemyLayer);
            if (enemy != null && enemy.CompareTag("Enemy"))
            {
                animator.SetBool("isShot", true);
                Shot();
                lastFireTime = Time.time;
            }
            if (enemy !=null && enemy.CompareTag("Enemy"))
            {
                animator.SetTrigger("Shot");
            }
            else
            {
                animator.SetTrigger("unShot");
            }
           
        }
    }
    private void Shot()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, shootRange, enemyLayer);

        Transform nearestEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider2D enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy.transform;
            }
        }

        if (nearestEnemy != null)
        {
            Vector2 direction = (nearestEnemy.position - firepoint.position).normalized;

            GameObject bullet = Instantiate(bulletPrefabs, firepoint.position, Quaternion.identity);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = direction * speedbullet;
           
        }
        
    }
}
