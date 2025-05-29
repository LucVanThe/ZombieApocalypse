using UnityEngine;

public class SaveGamePoint : MonoBehaviour
{
    //public Transform gamepoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           
            SaveManager saveManager =GameObject.Find("SaveManager").GetComponent<SaveManager>();
            saveManager.SaveGame();
        }
    }
}
