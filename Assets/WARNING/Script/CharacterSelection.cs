using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject redOutline;
    public GameObject yellowOutline;

    private static CharacterSelection previousSelection; // Référence vers la dernière sélection

    private void Start()
    {
        redOutline.SetActive(false);
        yellowOutline.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        redOutline.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        redOutline.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Désactiver le cadre jaune de la sélection précédente, s'il y en a une
        if (previousSelection != null && previousSelection != this)
        {
            previousSelection.yellowOutline.SetActive(false);
        }

        yellowOutline.SetActive(true);
        previousSelection = this; // Stocker la nouvelle sélection
    }
}