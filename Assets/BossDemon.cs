using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossDemon : Enemy
{
    
    [SerializeField] private float HoiHP = 100f;
    [SerializeField] private GameObject kedich1;
    [SerializeField] private GameObject kedich2,kedich3;
    [SerializeField] private float skillCD=5f;
    private float nexttimeskill = 0f;
    public float totalEnergy = 100f;
    private float currentEnergy;
    protected bool isStunned = false;
    //public Animator animator;
    [SerializeField] private Image Enerybar;
    private Enemy enemy;
    public override void Start()
    {
        base.Start();
        currentEnergy = totalEnergy;
      //  animator = GetComponent<Animator>();
    }
    protected override void Update()
    {
        base.Update();
        if(Time.time >= nexttimeskill)
        {
            sudungskill();
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
    
    private void taokedich1()
    {
        Instantiate(kedich1, transform.position, Quaternion.identity);
    }
    private void taokedich2()
    {
        Instantiate(kedich2, transform.position, Quaternion.identity);
        Instantiate(kedich3, transform.position, Quaternion.identity);
    }

    private void hoimau( float HoiHP)
    {
        currentHP = Mathf.Min(currentHP + HoiHP, MaxHp);
        UPdateHPbar();
    }
    private void dichchuyen()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if(distance <= 20)
            {
                Vector3 newPos = player.transform.position + new Vector3(4f, 0f, 0f);
                transform.position = newPos;
            }
            
        }
    }
    private void chonskillngaunhien()
    {
        int randomskill = Random.Range(0, 4);
        switch (randomskill)
        {
            case 0: taokedich2();break;
            case 1: taokedich1(); break;
            case 2: hoimau(HoiHP); break;
            case 3: dichchuyen(); break;
        }
    }
    private void sudungskill()
    {
        nexttimeskill = Time.time + skillCD;
        chonskillngaunhien();
    }
}
