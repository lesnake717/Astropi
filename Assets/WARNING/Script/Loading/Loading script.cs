using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InfiniteRadialLoading : MonoBehaviour
{
    [SerializeField] private Image radialIndicatorUI = null;
    [SerializeField] private float rotationSpeed = 90.0f; // Vitesse de rotation en degrés par seconde

    void Start()
    {
        if (radialIndicatorUI == null)
        {
            Debug.LogError("Radial Indicator UI is not assigned!");
            enabled = false;
            return;
        }

        StartCoroutine(LoadingCoroutine());
    }

    private IEnumerator LoadingCoroutine()
    {
        while (true) // Boucle infinie pour une rotation continue
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            radialIndicatorUI.fillAmount += rotationAmount / 360.0f; // Mise à jour du fillAmount pour simuler la rotation
            radialIndicatorUI.fillAmount %= 1.0f; // Assurez-vous que fillAmount reste entre 0 et 1

            yield return null;
        }
    }
}











