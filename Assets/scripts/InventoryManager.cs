using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryUI;
    private bool isOpen = false;

    void Start()
    {
        inventoryUI.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            ToggleInventory();
        }
    }

    void ToggleInventory()
    {
        //la variable pasa a ser lo contrario de lo que es actualmente
        isOpen = !isOpen;
        inventoryUI.SetActive(isOpen);
    }
}