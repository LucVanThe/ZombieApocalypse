using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    // Start is cal led once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] protected float movespeed = 1f;
    protected Player player;
    [SerializeField] protected float MaxHp = 100f;
    public float currentHP;
    [SerializeField] private Image HPbar;
    [SerializeField] protected float Enterdamage = 10f;
    [SerializeField] protected float Staydamage = 1f;
    private Vector2 lastPosition;
    private Animator animator;
    public bool isMove;
    [SerializeField] private float followRange = 5f;
    [SerializeField] protected float stopRange = 0.1f;

    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        player = FindAnyObjectByType<Player>();
        currentHP = MaxHp;
        UPdateHPbar();
    }
    protected virtual void Update()
    {
        MoveToPlayer();
        FlipEnemy();
        UpdateAnimation();

    }
    
    protected void MoveToPlayer()
    {
        if (player != null)
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
            if(distance <= stopRange)
            {
                animator.SetBool("isAttack", true);
            }
            else
            {
                animator.SetBool("isAttack", false);
            }
        }

        
    }
    private void UpdateAnimation()
    {
        Vector2 currentPosition = transform.position;

        if (currentPosition != lastPosition)
        {
            animator.SetBool("isRun", true);
            isMove = true;
        }
        else
        {
            animator.SetBool("isRun", false);
            isMove = false;
        }

        lastPosition = currentPosition;
    }

    protected void FlipEnemy()
    {
        if (player != null)
        {
            transform.localScale = new Vector3(player.transform.position.x < transform.position.x ? -1f : 1f,1f, 1);
        }
    }
    public virtual void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);
        UPdateHPbar();
        if (currentHP <= 0)
        { 
            animator.SetBool("isDealth", true);
            StartCoroutine(DelayDeath());
        }
    }
    private IEnumerator DelayDeath()
    {
        float delay = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(delay);
        
        Die();
    }

    protected virtual void Die()
    {
       
        Destroy(gameObject);
    }
    protected void UPdateHPbar()
    {
        if (HPbar != null)
        {
            HPbar.fillAmount = currentHP / MaxHp;
        }
    }
}
