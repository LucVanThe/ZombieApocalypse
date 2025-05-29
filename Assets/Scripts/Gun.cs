

using System.Collections;
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
    public bool isReloading=false;
    public float reloadtime = 2f;
    private float currreloadtime = 0f;
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
        currreloadtime = playerStat.reloadtime;
        //Debug.Log("delay co ban = " + shotdelay);
        //Debug.Log("delay them = " + playerStat.shotdelay);
        //Debug.Log(" tong delay = " + currentshotdelay);
    }

    void Update()
    {
        RotateGun();
        if (inventoryManager.isCursor)
        {
            Shoot();
        }

        // reload();
        if (Input.GetMouseButtonDown(1) && currentAmmo < maxAmmo && !isReloading)
        {
            StartCoroutine(Reload());
        }

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
        Player player = FindFirstObjectByType<Player>();

        if (player != null && !player.isMoving && Input.GetMouseButton(0) && currentAmmo > 0 && Time.time > nextShot)
        {
            nextShot = Time.time + currentshotdelay;
            Instantiate(bulletPrefabs, firepoint.position, firepoint.rotation);
            currentAmmo--;
            UpdateArmoText();
            AudioManager.shotPlay();
        }
    }
    IEnumerator Reload()
    {
        isReloading = true;

       
        AudioManager.reLoadPlay();


        //yield return new WaitForSeconds(2f);
        float timer = currreloadtime;

       
        while (timer > 0)
        {
            armoText.text = "Đang nạp đạn " + timer.ToString("F1"); 
            yield return null;
            timer -= Time.deltaTime;
        }
        currentAmmo = maxAmmo;
        UpdateArmoText();
        isReloading = false;
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
    //void reload()
    //{
    //    if(Input.GetMouseButtonDown(1) && currentAmmo < maxAmmo)
    //    {
    //        AudioManager.reLoadPlay();
    //        yield 
    //        currentAmmo = maxAmmo;
    //        UpdateArmoText();

    //    }
    //}
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
