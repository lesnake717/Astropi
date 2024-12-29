using UnityEngine;
using UnityEngine.EventSystems;

public class SelectPersonnage : MonoBehaviour, IPointerClickHandler
{
    public int numeroPersonnage;

    public void OnPointerClick(PointerEventData evenementData)
    {
        // Enregistrer le numéro du personnage sélectionné dans PlayerPrefs avec la clé "PersonnageChoisiIndex"
        PlayerPrefs.SetInt("PersonnageChoisiIndex", numeroPersonnage);
    }
}