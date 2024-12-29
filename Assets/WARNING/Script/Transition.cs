using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    // Méthode pour charger une scène
    public void LoadScene(string sceneName)
    {
        // Vérification si la scène demandée existe dans les Build Settings
        if (IsSceneInBuildSettings(sceneName))
        {
            Debug.Log($"Tentative de chargement de la scène : {sceneName}");
            SceneManager.LoadScene(sceneName); // Charger la scène
        }
        else
        {
            Debug.LogError($"Erreur : La scène '{sceneName}' n'existe pas dans les Build Settings. Vérifiez le nom ou ajoutez la scène dans File > Build Settings.");
        }
    }

    // Méthode pour vérifier si la scène est présente dans les Build Settings
    private bool IsSceneInBuildSettings(string sceneName)
    {
        // Obtenir toutes les scènes définies dans les Build Settings
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameFromPath = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneNameFromPath == sceneName)
            {
                return true; // Scène trouvée
            }
        }
        return false; // Scène non trouvée
    }
}
