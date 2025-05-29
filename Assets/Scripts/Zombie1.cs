using UnityEngine;

public class Zombie1 : Enemy
{
    //  [SerializeField] private float damage = 10f;
    AudioManager AudioManager;
    private void Awake()
    {
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamage(Enterdamage);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamage(Staydamage);
            }
        }
    }
    protected override void MoveToPlayer()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance <= followRange && distance > stopRange)
            {
                AudioManager.ZombiePlay();
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    player.transform.position,
                    movespeed * Time.deltaTime
                );
            }
            if (distance <= stopRange)
            {
                animator.SetBool("isAttack", true);
            }
            else
            {
                animator.SetBool("isAttack", false);
            }
        }
    }
}
