using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public bool isMoving = false;
    [SerializeField] private float basespeed = 0;
    private float movespeed;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    //public bool canMove = true;
    // private SpriteRenderer Armosprite;
    [SerializeField] private float baseHp = 0;
    public float maxHP;
    private float currentHP;
    [SerializeField] private Image HPbar;

    [SerializeField] private SpriteRenderer armosprite;
    private PlayerStat playerStat;
    private AudioManager AudioManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer =GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerStat = GameObject.Find("StatManager").GetComponent<PlayerStat>();
        // Armosprite = GameObject.Find("Armo").GetComponent<SpriteRenderer>();
        AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

    }
   
    
    void Start()
    {   
        UpdateHP();
       // Debug.Log("HP toi da = " + maxHP);
        currentHP = maxHP;
        UPdateHPbar();
        
        
    }
    public void UpdateHP()
    {
        maxHP = baseHp + playerStat.health;
        movespeed = basespeed + playerStat.speed;
        
    }
    // Update is called once per frame
    void Update()
    {
        ShotAnimator();
        
            Moveplayer();
            
        
    }
    private void Footstep()
    {
    //    AudioManager.FootStep();
    }
    private void Moveplayer()
    {

        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        isMoving = playerInput != Vector2.zero; // Cập nhật trạng thái
        
        rb.linearVelocity = playerInput.normalized * movespeed;
       
        if (Input.GetMouseButtonDown(0))
        {
            FlipTowardsMouse();
        }

        if (playerInput.x < 0)
            spriteRenderer.flipX = true;
        else if (playerInput.x > 0)
            spriteRenderer.flipX = false;

        if (playerInput != Vector2.zero)
        {
            animator.SetBool("isRun", true);
            animator.SetBool("isShot", false);
        }
        else
        {
            animator.SetBool("isRun", false);

        }
        
    }

    //private void Moveplayer()
    //{

    //    Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    //    rb.linearVelocity = playerInput.normalized * movespeed;
    //    if (Input.GetMouseButtonDown(0)) 
    //    {
    //        FlipTowardsMouse();
    //    }
    //    if (playerInput.x < 0)
    //    {
    //        spriteRenderer.flipX = true;
    //        // Armosprite.flipX = true;

    //    }
    //    else if (playerInput.x > 0)
    //    {
    //        spriteRenderer.flipX = false;
    //        //  Armosprite.flipX = false;
    //    }
//        if (playerInput != Vector2.zero)
//        {
//            animator.SetBool("isRun", true);
//            animator.SetBool("isShot", false);
//        }
//        else
//{
//    animator.SetBool("isRun", false);

//}

//}
private void FlipTowardsMouse()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mouseWorldPosition.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
            // Armosprite.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
            // Armosprite.flipX = false;
        }
    }

    public void ShotAnimator()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isShot",true);
           
          
        }
        if (Input.GetMouseButtonUp(0))
        {
        
            animator.SetBool("isShot", false);
            
        }


    }

    public  void TakeDamage(float damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0);
        UPdateHPbar();
        if (currentHP <= 0)
        {
            Die();
        }
    }
    public  void Die()
    {
        Destroy(gameObject);
    }
    protected void UPdateHPbar()
    {
        if (HPbar != null)
        {
            HPbar.fillAmount = currentHP / maxHP;
        }
    }
    public void HoiMau(float hp)
    {
        if (currentHP < maxHP)
        {
            currentHP += hp;
            UPdateHPbar();
        }
        
    }
}
