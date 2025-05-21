using UnityEngine;

public class bulletAR : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 50f;
    [SerializeField] private float timedestroy = 0.5f;
    [SerializeField] public float basedamage = 10f;
    public float currentDamage;
    [SerializeField] private GameObject bloodPrefabs;
    private PlayerStat playerStat;
    void Start()
    {
        Destroy(gameObject, timedestroy);
        playerStat = GameObject.Find("StatManager").GetComponent<PlayerStat>();
        UpdateDamage();
    }
    public void UpdateDamage()
    {
        //Debug.Log("sat thuong co ban = " + basedamage);
        //Debug.Log("sat thuong them = " + playerStat.attack);
        currentDamage = basedamage + playerStat.attack;
    }
    void Update()
    {
        Movebullet();
    }
    void Movebullet()
    {
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(currentDamage);
               // Debug.Log("sat thuong = " + currentDamage);
                GameObject blood = Instantiate(bloodPrefabs, transform.position, Quaternion.identity);
                Destroy(blood, 1f);
            }
            Destroy(gameObject);
        }
    }
}
