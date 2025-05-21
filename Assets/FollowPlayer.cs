using UnityEngine;
using UnityEngine.InputSystem;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;          
    public float followSpeed = 3f;   
    public float followDistance = 2f;
    public float detectRange = 8f;
    public LayerMask enemyLayer;
    private Animator animator;
    private Vector2 lastPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    void Update()
    {
        transformPlayer();
    }
    private void transformPlayer()
    {
        if (player == null) return;

       
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > followDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, followSpeed * Time.deltaTime);
        }

     
        Transform targetToLookAt = FindClosestEnemyInRange();
        if (targetToLookAt == null)
        {
            targetToLookAt = player;
        }

      
        Vector3 dir = targetToLookAt.position - transform.position;
        if (dir.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (dir.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

      
        Vector2 currentPosition = transform.position;
        Vector2 movement = currentPosition - lastPosition;
        bool isMoving = movement.sqrMagnitude > 0.001f;

        animator.SetBool("isRun", isMoving);
        animator.SetBool("isShot", false);
        lastPosition = currentPosition;
    }
    Transform FindClosestEnemyInRange()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectRange, enemyLayer);
        Transform closestEnemy = null;
        float minDist = Mathf.Infinity;

        foreach (Collider2D enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }
}

