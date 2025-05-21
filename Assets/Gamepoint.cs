using UnityEngine;

public class Gamepoint : MonoBehaviour
{
    [SerializeField] public Transform gamepoint;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject allain;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player =GameObject.Find("Player").GetComponent<Player>();
            player.transform.position = gamepoint.position;
            if(enemy != null)
            {
                enemy.SetActive(true);
            }
            if(allain != null)
            {
                allain.SetActive(true);
            }
          
        }
    }
}
