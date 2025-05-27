using UnityEditor.Rendering;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector3 moveDirection;
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    public void SetMovementDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    void Update()
    {
        if (moveDirection != Vector3.zero)
        {
            transform.position += moveDirection * Time.deltaTime;
        }
    }
}
