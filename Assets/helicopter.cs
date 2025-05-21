using UnityEngine;

public class helicopter : MonoBehaviour
{
    public Transform player;
    public float heightOffset = 5f;
    [SerializeField] private GameObject minienemy;
    [SerializeField] private float skillCD = 3f;
    private float nextskilltimw = 0f;
    private Enemy enemy;
    [SerializeField] private GameObject ExploPrefabs;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }
    protected void Update()
    {
       
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y + heightOffset, transform.position.z);
        if (Time.time >= nextskilltimw)
        {
            useskill();
        }
        Die();
    }
    private void CreateExplosion()
    {
        Instantiate(ExploPrefabs, transform.position, Quaternion.identity);
    }
    protected void Die()
    {
        if(enemy.currentHP == 0)
        CreateExplosion();
       
    }
    private void CreateZombie()
    {
        Instantiate(minienemy, transform.position, Quaternion.identity);
    }
    private void useskill()
    {

        nextskilltimw = Time.time + skillCD;
        CreateZombie();
    }
}
