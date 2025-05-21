using UnityEngine;

public class Cursormanager : MonoBehaviour
{
    private InventoryManager inventoryManager;
    [SerializeField] private Texture2D cursorNormal;
    [SerializeField] private Texture2D cursorshoot;
    [SerializeField] private Texture2D cursorReload;
    [SerializeField] private Texture2D cursorDefault;
    private Vector2 hotspot = new Vector2(0, 0);
    private void Awake()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
    }
    void Start()
    {
        Cursor.SetCursor(cursorNormal, hotspot, CursorMode.Auto);
       
    }

    // Update is called once per frame
    void Update()
    {
       
            Cursormanagers();
    }
    public void Cursormanagers()
    {
        if (!inventoryManager.isCursor)
        {
            Cursor.SetCursor(cursorDefault, hotspot, CursorMode.Auto);
            return;
        }
        else
        {
           
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.SetCursor(cursorshoot, hotspot, CursorMode.Auto);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Cursor.SetCursor(cursorNormal, hotspot, CursorMode.Auto);
            }
            if (Input.GetMouseButtonDown(1))
            {
                Cursor.SetCursor(cursorReload, hotspot, CursorMode.Auto);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                Cursor.SetCursor(cursorNormal, hotspot, CursorMode.Auto);
            }
           

        }



    }
}
