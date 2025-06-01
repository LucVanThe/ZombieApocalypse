using System.Collections;
using UnityEngine;

public class Tank :Enemy
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    [SerializeField] private float skillCD = 2f;
    private float nextSkillTime = 0f;
    [SerializeField] private GameObject ExploPrefabs;
    public enum Direction
    {
        Up, Down, Left, Right,
        UpLeft, UpRight, DownLeft, DownRight
    }

    public Sprite[] directionSprites = new Sprite[8];
    public SpriteRenderer spriteRenderer;
    protected override void Update()
    {
        base.Update();
        MoveToPlayer();
    }

   
    private void CreateExplosion()
    {
        Instantiate(ExploPrefabs, transform.position, Quaternion.identity);
    }
    protected override void Die()
    {
        CreateExplosion();
        base.Die();
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
    protected override void FlipEnemy()
    {

    }
    protected override void MoveToPlayer()
    {
        if (player != null)
        {

            float distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = (player.transform.position - transform.position);
            UpdateSpriteDirection(direction);
            if (distance <= followRange && distance > stopRange)
            {

                transform.position = Vector2.MoveTowards(
                    transform.position,
                    player.transform.position,
                    movespeed * Time.deltaTime);
                
            }
            if (distance <= stopRange)
            {
                if (Time.time >= nextSkillTime)
                {
                    StartCoroutine(UseSkill());
                }
            }
        }
    }
    public void UpdateSpriteDirection(Vector2 dir)
    {
        if (dir == Vector2.zero) return;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = (angle + 360f) % 360f;

        Direction direction;

        if (angle >= 350 || angle < 10)
            direction = Direction.Right;
        else if (angle >= 10 && angle < 80)
            direction = Direction.UpRight;
        else if (angle >= 80 && angle < 100)
            direction = Direction.Up;
        else if (angle >= 100 && angle < 170)
            direction = Direction.UpLeft;
        else if (angle >= 170 && angle < 190)
            direction = Direction.Left;
        else if (angle >= 190 && angle < 260)
            direction = Direction.DownLeft;
        else if (angle >= 260 && angle < 280)
            direction = Direction.Down;
        else if (angle >= 280 && angle < 350)
            direction = Direction.DownRight;
        else
            return;

        spriteRenderer.sprite = directionSprites[(int)direction];
    }
    private IEnumerator UseSkill()
    {
        nextSkillTime = Time.time + skillCD;
        ShootAtPlayer();
        yield return new WaitForSeconds(5f);

    }
}
