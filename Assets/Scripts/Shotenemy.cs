using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Shotenemy : Enemy
{
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform firepoint;
    [SerializeField] private float speedbullet = 10f;
    [SerializeField] private float skillCD = 2f;
    private float distance;
    Enemy enemy;
  
    private float nextSkillTime = 0f;
    public override void Start()
    {
        enemy = GetComponent<Enemy>();
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (enemy.isMove == false && distance <= stopRange)
        {
            if (Time.time >= nextSkillTime)
            {
                StartCoroutine(UseSkill());
            }
        }
       

       
        
    }
    private void Bandanthuong()
    {
        if(player != null)
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
            yield return new WaitForSeconds(0.2f); 
        }
    }

}
