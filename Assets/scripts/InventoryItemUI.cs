using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    private Image image;
    private Color originalColor;

    void Awake()
    {
        image = GetComponent<Image>();
        originalColor = image.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Se selecciono el item");
        image.color = Color.yellow; // 👈 hover
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Se dejo de seleccionar el item");
        image.color = originalColor; // 👈 vuelve al original
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click en el item");
    }
}