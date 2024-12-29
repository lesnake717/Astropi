using UnityEngine;
using UnityEngine.UI;

public class BoutonSelectionner : MonoBehaviour
{
    private Button boutonSelectionner;
    private GestionPersonnage gestionPersonnage;

    private void Start()
    {
        boutonSelectionner = GetComponent<Button>();
        gestionPersonnage = Object.FindFirstObjectByType<GestionPersonnage>(); // Utilisation de la méthode recommandée
        boutonSelectionner.onClick.AddListener(InstancierPersonnageChoisi);
    }

    private void InstancierPersonnageChoisi()
    {
        gestionPersonnage.InstancierPersonnageChoisi();
    }
}


