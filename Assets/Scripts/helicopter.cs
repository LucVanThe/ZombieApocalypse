using System.Collections;
using UnityEngine;

public class helicopter : Enemy
{
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform firepoint;
    [SerializeField] private float speedbullet = 10f;
    [SerializeField] private float skillCD = 2f;
    private float distance;  
    private AudioManager audioManager;
    private float nextSkillTime = 0f;
    public float heightOffsetX= 10f;public float heightOffsetY= 10f;
    public float heightSoilder = 8f;
    [SerializeField] private GameObject minienemy;
    [SerializeField] private float skillCDsoilder = 3f;
    private float nextskilltimw = 0f;
    private Enemy enemy;
    [SerializeField] private GameObject ExploPrefabs;
    Vector3 targetPosition;
    public override  void Start()
    {
       
        enemy = GetComponent<Enemy>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        Player player = GameObject.Find("Player").GetComponent<Player>();
        base.Start();
        //enemy = GetComponent<Enemy>();
    }
    protected override void Update()
    {
        base.Update();
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (enemy.isMove == false && distance <= followRange)
        {
            if (Time.time >= nextSkillTime)
            {
                StartCoroutine(UseSkill());
            }
        }  
        targetPosition = new Vector3(player.transform.position.x + heightOffsetX, player.transform.position.y + heightOffsetY, transform.position.z);
        if (Time.time >= nextskilltimw)
        {
            useskill();
        }
       
    }

    private void CreateExplosion()
    {
        Instantiate(ExploPrefabs, transform.position, Quaternion.identity);
    }
    protected override void MoveToPlayer()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance <= followRange && distance > stopRange)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,  targetPosition,              
                    movespeed * Time.deltaTime
                );
            }
          
        }
    }
    protected override void Die()
    {
        CreateExplosion();
        base.Die();
    }
    private void CreateZombie()
    {
        Vector3 spawnPosition = transform.position + Vector3.down * heightSoilder;
        Instantiate(minienemy, spawnPosition, Quaternion.identity);
    }
    private void useskill()
    {

        nextskilltimw = Time.time + skillCDsoilder;
        CreateZombie();
    }
    private void Bandanthuong()
    {
        if (player != null)
        {

            Vector3 directionToPlayer = player.transform.position - firepoint.position;
            directionToPlayer.Normalize();
            GameObject bullet = Instantiate(bulletPrefabs, firepoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(directionToPlayer * speedbullet);
        }
    }
    private IEnumerator UseSkill()
    {
        nextSkillTime = Time.time + skillCD;

        for (int i = 0; i < 5; i++)
        {
            //Debug.Log("Bắn đạn: " + i);
            Bandanthuong();
            audioManager.shotPlay();
            yield return new WaitForSeconds(0.2f);
        }
    }
}
