

using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private float rotateoffset = 180f;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] public float shotdelay = 0.15f;
    [SerializeField] public float currentshotdelay = 0f;
    private float nextShot;
    [SerializeField] private int maxAmmo = 30;
    [SerializeField] private AudioManager AudioManager;
    private PlayerStat playerStat;
    public int currentAmmo;
    private InventoryManager inventoryManager;
    [SerializeField] private TextMeshProUGUI armoText;
    private void Awake()
    {
        playerStat = GameObject.Find("StatManager").GetComponent<PlayerStat>();
    }
    void Start()
    {
        
        UpdateShotdelay();
        currentAmmo = maxAmmo;
        UpdateArmoText();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
      
    }
    public void UpdateShotdelay()
    {
        currentshotdelay = Mathf.Max(0.1f, shotdelay + playerStat.shotdelay);
        Debug.Log("delay co ban = " + shotdelay);
        Debug.Log("delay them = " + playerStat.shotdelay);
        Debug.Log(" tong delay = " + currentshotdelay);
    }

    void Update()
    {
        RotateGun();
        if (inventoryManager.isCursor)
        {
            Shoot();
        }

        reload();
        
    }
    
    void RotateGun()
    {
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
        {
            return;
        }
        Vector3 displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + rotateoffset);
        if (angle < -90 || angle > 90)
        {
            transform.localScale = new Vector3(1f, 1f, 1);
        }
        else
        {
            transform.localScale = new Vector3(1f, -1f, 1);
        }
    }
    void Shoot()
    {
        Player player = FindObjectOfType<Player>();

        if (player != null && !player.isMoving && Input.GetMouseButton(0) && currentAmmo > 0 && Time.time > nextShot)
        {
            nextShot = Time.time + currentshotdelay;
            Instantiate(bulletPrefabs, firepoint.position, firepoint.rotation);
            currentAmmo--;
            UpdateArmoText();
            AudioManager.shotPlay();
        }
    }

    //    void Shoot()
    //{
    //    if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time > nextShot)
    //    {
    //        nextShot = Time.time + shotdelay;
    //        Instantiate(bulletPrefabs, firepoint.position, firepoint.rotation);
    //        currentAmmo--;
    //        AudioManager.shotPlay();

    //    }
    //}



    void reload()
    {
        if(Input.GetMouseButtonDown(1) && currentAmmo < maxAmmo)
        {
            currentAmmo = maxAmmo;
            UpdateArmoText();
            AudioManager.reLoadPlay();
        }
    }
    private void UpdateArmoText()
    {
        if(armoText!= null)
        {
            if(currentAmmo > 0)
            {
                armoText.text = currentAmmo.ToString();
            }
            else
            {
                armoText.text = "Hết đạn";
            }
        }
    }
}
