using UnityEngine;

public class tankturret : MonoBehaviour
{
    private Player player;
    public enum Direction
    {
        Up, Down, Left, Right,
        UpLeft, UpRight, DownLeft, DownRight
    }

    public Sprite[] directionSprites = new Sprite[8];
    public SpriteRenderer spriteRenderer;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    protected void Update()
    {
        MoveToPlayer();
    } 
    protected void MoveToPlayer()
    {
        if (player != null)
        {           
            Vector2 direction = (player.transform.position - transform.position);
            UpdateSpriteDirection(direction);                     
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
}
