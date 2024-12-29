using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    // M�thode pour charger une sc�ne
    public void LoadScene(string sceneName)
    {
        // V�rification si la sc�ne demand�e existe dans les Build Settings
        if (IsSceneInBuildSettings(sceneName))
        {
            Debug.Log($"Tentative de chargement de la sc�ne : {sceneName}");
            SceneManager.LoadScene(sceneName); // Charger la sc�ne
        }
        else
        {
            Debug.LogError($"Erreur : La sc�ne '{sceneName}' n'existe pas dans les Build Settings. V�rifiez le nom ou ajoutez la sc�ne dans File > Build Settings.");
        }
    }

    // M�thode pour v�rifier si la sc�ne est pr�sente dans les Build Settings
    private bool IsSceneInBuildSettings(string sceneName)
    {
        // Obtenir toutes les sc�nes d�finies dans les Build Settings
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneNameFromPath = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneNameFromPath == sceneName)
            {
                return true; // Sc�ne trouv�e
            }
        }
        return false; // Sc�ne non trouv�e
    }
}
