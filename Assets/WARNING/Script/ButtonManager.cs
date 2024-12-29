using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    public Button worldCupButton;
    public Button superCupButton;
    public Text worldCupCountdownText;
    public Text superCupCountdownText;

    private float worldCupCountdownStart = 8640000f; // 8 jours en secondes (8 * 24 * 60 * 60)
    private float superCupCountdownStart = 4320000f; // 5 jours en secondes (5 * 24 * 60 * 60)
    private float worldCupCountdownRemaining;
    private float superCupCountdownRemaining;
    private float savedWorldCupCountdownRemaining; // Variable pour stocker la valeur restante
    private float savedSuperCupCountdownRemaining; // Variable pour stocker la valeur restante

    private Coroutine worldCupCoroutine;
    private Coroutine superCupCoroutine;

    void Start()
    {
        // Initialiser les compteurs à rebours restants
        worldCupCountdownRemaining = savedWorldCupCountdownRemaining > 0 ? savedWorldCupCountdownRemaining : worldCupCountdownStart;
        superCupCountdownRemaining = savedSuperCupCountdownRemaining > 0 ? savedSuperCupCountdownRemaining : superCupCountdownStart;

        // Désactiver les boutons et ajuster la couleur pour les griser
        SetButtonInteractable(worldCupButton, false);
        SetButtonInteractable(superCupButton, false);

        // Lancer les compteurs à rebours
        worldCupCoroutine = StartCoroutine(CountdownRoutine(worldCupCountdownRemaining, worldCupCountdownText, worldCupButton));
        superCupCoroutine = StartCoroutine(CountdownRoutine(superCupCountdownRemaining, superCupCountdownText, superCupButton));
    }

    void OnEnable()
    {
        Debug.Log("OnEnable called.");

        // Récupérer les valeurs sauvegardées des compteurs à rebours
        savedWorldCupCountdownRemaining = PlayerPrefs.GetFloat("SavedWorldCupCountdownRemaining", 0);
        savedSuperCupCountdownRemaining = PlayerPrefs.GetFloat("SavedSuperCupCountdownRemaining", 0);

        Debug.Log("Saved World Cup Countdown Remaining: " + savedWorldCupCountdownRemaining);
        Debug.Log("Saved Super Cup Countdown Remaining: " + savedSuperCupCountdownRemaining);

        // Relancer les compteurs à rebours avec le temps restant
        if (savedWorldCupCountdownRemaining > 0)
            worldCupCoroutine = StartCoroutine(CountdownRoutine(savedWorldCupCountdownRemaining, worldCupCountdownText, worldCupButton));
        if (savedSuperCupCountdownRemaining > 0)
            superCupCoroutine = StartCoroutine(CountdownRoutine(savedSuperCupCountdownRemaining, superCupCountdownText, superCupButton));
    }

    void OnDisable()
    {
        Debug.Log("OnDisable called.");

        // Sauvegarder les valeurs restantes des compteurs à rebours
        PlayerPrefs.SetFloat("SavedWorldCupCountdownRemaining", worldCupCountdownRemaining);
        PlayerPrefs.SetFloat("SavedSuperCupCountdownRemaining", superCupCountdownRemaining);
        PlayerPrefs.Save(); // Assure que les valeurs sont bien sauvegardées

        Debug.Log("World Cup Countdown Remaining saved: " + worldCupCountdownRemaining);
        Debug.Log("Super Cup Countdown Remaining saved: " + superCupCountdownRemaining);

        // Arrêter les coroutines en cours
        StopCoroutines();
    }

    void StopCoroutines()
    {
        if (worldCupCoroutine != null)
        {
            StopCoroutine(worldCupCoroutine);
        }

        if (superCupCoroutine != null)
        {
            StopCoroutine(superCupCoroutine);
        }
    }

    void SetButtonInteractable(Button button, bool interactable)
    {
        button.interactable = interactable;
        ColorBlock colorBlock = button.colors;
        colorBlock.disabledColor = interactable ? Color.white : new Color(0.8f, 0.8f, 0.8f); // Blanc si activé, gris sinon
        button.colors = colorBlock;
    }

    IEnumerator CountdownRoutine(float countdown, Text countdownText, Button button)
    {
        button.interactable = false; // Désactiver le bouton au début du compte à rebours

        while (countdown > 0)
        {
            int days = Mathf.FloorToInt(countdown / (24f * 60f * 60f));
            int hours = Mathf.FloorToInt((countdown % (24f * 60f * 60f)) / (60f * 60f));
            int minutes = Mathf.FloorToInt((countdown % (60f * 60f)) / 60f);
            int seconds = Mathf.FloorToInt(countdown % 60f);

            countdownText.text = string.Format("{0:D2}j {1:D2}h {2:D2}m {3:D2}s", days, hours, minutes, seconds);

            countdown -= 1;
            yield return new WaitForSeconds(1);
        }

        // Compte à rebours terminé, activer le bouton
        button.interactable = true;
        ColorBlock colorBlock = button.colors;
        colorBlock.disabledColor = Color.white; // Rétablir la couleur du bouton à sa valeur par défaut
        button.colors = colorBlock;
        countdownText.text = "Prêt à jouer !";
    }
}
