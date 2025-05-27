using System.Collections;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private float timecd = 4f;
    private float time;
    [SerializeField] private GameObject ExploPrefabs;
    [SerializeField] private GameObject WarningCircle;
    private void Start()
    {
        time = timecd;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            WarningCircle.SetActive(true);
            StartCoroutine(Explosion());
           
        }
    }
     IEnumerator Explosion()
   
   // private void Explosion()
    {
       // time -= Time.deltaTime;
       yield return new WaitForSeconds(timecd);
       //if(time <= 0)
        {
           
            CreateExplosion();
            Destroy(gameObject);
        }
       
    }
    private void CreateExplosion()
    {
        Instantiate(ExploPrefabs, transform.position, Quaternion.identity);
    }
}
