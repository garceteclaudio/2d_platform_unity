using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryUI;
    private bool isOpen = false;

    public Image espadaImage;
    public Image pistolaImage;
    public Image lifePointsImage;

    void Start()
    {
        inventoryUI.SetActive(false);
        espadaImage.enabled = false;
        pistolaImage.enabled = false;
        lifePointsImage.enabled = false;
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

    public void AddEspada()
    {
        espadaImage.enabled = true;
    }

    public void AddPistola()
    {
        pistolaImage.enabled = true;
    }

    public void AddLifePoints()
    {
        lifePointsImage.enabled = true;
    }
}