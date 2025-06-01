using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class BossMecha : Enemy
{
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private float speedbullet = 10f;
    [SerializeField] private float skillCDs = 2f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    [SerializeField] private float HoiHP = 100f;
    [SerializeField] private GameObject kedich1;
    [SerializeField] private float skillCD = 5f;
    private float nexttimeskill = 0f;
    public float totalEnergy = 100f;
    private float currentEnergy;
    protected bool isStunned = false;
    //public Animator animator;
    [SerializeField] private Image Enerybar;
    private Enemy enemy;
    private AudioManager audioManager;
    private float nextSkillTime = 0f;
    private float distance;
    public override void Start()
    {
        base.Start();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        currentEnergy = totalEnergy;
        enemy = GetComponent<Enemy>();
    }
    protected override void Update()
    {
        base.Update();
        if (Time.time >= nexttimeskill)
        {
            sudungskill();         
        }
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (enemy.isMove == false && distance <= stopRange)
        {
            if (Time.time >= nextSkillTime)
            {
                StartCoroutine(UseSkill());
            }
        }
    }
    protected override void MoveToPlayer()
    {
        if (player != null && !isStunned)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance <= followRange && distance > stopRange)
            {
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
        else
        {
            animator.SetBool("isAttack", false);
        }

    }
    public override void TakeDamage(float damage)
    {
        if (player == null)
        {
            Debug.LogWarning("Player = null");
            return;
        }

        float finalDamage = isStunned ? damage * 2f : damage;

        currentHP -= finalDamage;
        currentHP = Mathf.Max(currentHP, 0);
        UPdateHPbar();

        if (currentHP <= 0)
        {
            animator.SetBool("isDealth", true);
            StartCoroutine(DelayDeath());
        }
    }
    public void TruNangLuong(float amount)
    {
        currentEnergy -= amount;
        UPdateEnerybar();
        if (currentEnergy <= 0 && !isStunned)
        {
            Choang(10);
        }
    }
    public virtual void Choang(float duration)
    {
        if (!isStunned)
        {
            StartCoroutine(Thoigianchoang(duration));
        }
    }
    private IEnumerator Thoigianchoang(float duration)
    {
        isStunned = true;
        Debug.Log($"{gameObject.name} bị choáng trong {duration} giây!");
        MoveToPlayer();
        animator.SetBool("isStun", true);
        yield return new WaitForSeconds(duration);
        isStunned = false;
        currentEnergy = totalEnergy;
        UPdateEnerybar();
        animator.SetBool("isStun", false);
    }
    protected void UPdateEnerybar()
    {
        if (Enerybar != null)
        {
            Enerybar.fillAmount = currentEnergy / totalEnergy;
        }
    }
    public bool IsStunned()
    {
        return isStunned;
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
    private void Bandanthuong()
    {
        if (player != null)
        {

            Vector3 directionToPlayer = player.transform.position - firePoint.position;
            directionToPlayer.Normalize();
            GameObject bullet = Instantiate(bulletPrefabs, firePoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(directionToPlayer * speedbullet);
        }
    }
    private IEnumerator UseSkill()
    {
        nextSkillTime = Time.time + skillCDs;

        for (int i = 0; i < 5; i++)
        {
            Bandanthuong();
            audioManager.shotPlay();
            yield return new WaitForSeconds(0.2f);
        }
    }
    void ShootAtPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 targetPos = player.transform.position;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<TankBullet>().SetTarget(targetPos);
        }
    }
    private void skillDacbiet()
    {
        StartCoroutine(DelayAnimationShot());
    }
    public IEnumerator DelayAnimationShot()
    {
        animator.SetBool("isSkillDB", true);
        float delay = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(delay);
        ShootAtPlayer();
        animator.SetBool("isSkillDB", false);
    }
    private void taokedich1()
    {
        Instantiate(kedich1, transform.position, Quaternion.identity);
    }

    private void hoimau(float HoiHP)
    {
        currentHP = Mathf.Min(currentHP + HoiHP, MaxHp);
        UPdateHPbar();
    }
   
    private void chonskillngaunhien()
    {
        int randomskill = Random.Range(0, 4);
        switch (randomskill)
        {
           
            case 0: taokedich1(); break;
            case 1: hoimau(HoiHP); break;
            case 3:skillDacbiet();break;
          
        }
    }
    private void sudungskill()
    {
        nexttimeskill = Time.time + skillCD;
        chonskillngaunhien();
    }
}
