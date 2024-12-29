using UnityEngine;

public class PauseDebugger : MonoBehaviour
{
    private float previousTimeScale;

    void Start()
    {
        previousTimeScale = Time.timeScale; // Enregistre l'état initial
    }

    void Update()
    {
        if (Time.timeScale != previousTimeScale)
        {
            Debug.LogWarning($"Time.timeScale modifié : Ancien = {previousTimeScale}, Nouveau = {Time.timeScale}. StackTrace : {System.Environment.StackTrace}");
            previousTimeScale = Time.timeScale;
        }
    }
}
