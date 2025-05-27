using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameManager GameManager;
  

    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
            Player player = GetComponent<Player>();
            player.TakeDamage(10f);
        }
        if (collision.CompareTag("Keywin"))
        {
            GameManager.gameWinMenu();
        }
    }
}
