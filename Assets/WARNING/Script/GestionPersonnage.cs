// Script GestionPersonnage.cs
using UnityEngine;

public class GestionPersonnage : MonoBehaviour
{
    public static int instancePersonnageChoisi = -1;
    public GameObject[] prefabsPersonnages;

    public static int PersonnageChoisiIndex { get; set; } // Nouvelle propriété static

    public void InstancierPersonnageChoisi()
    {
        if (instancePersonnageChoisi != -1)
        {
            PersonnageChoisiIndex = instancePersonnageChoisi; // Assigner l'index au lieu d'instancier
            // Ne pas instancier ici, on le fera dans la nouvelle scène
        }
    }
}