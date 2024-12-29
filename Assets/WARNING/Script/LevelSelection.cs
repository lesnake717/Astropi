using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButtons;
    private Color disabledColor = new Color(0.8f, 0.8f, 0.8f); // Couleur légèrement grise pour les niveaux désactivés

    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 2);

        // Désactiver tous les boutons de niveau au départ et les grisers légèrement
        for (int i = 0; i < lvlButtons.Length; i++)
        {
            lvlButtons[i].interactable = false;
            ColorBlock colorBlock = lvlButtons[i].colors;
            colorBlock.disabledColor = disabledColor;
            lvlButtons[i].colors = colorBlock;
        }

        // Activer uniquement le bouton correspondant au niveau de départ
        if (levelAt >= 2 && levelAt <= lvlButtons.Length + 1)
        {
            int levelIndex = levelAt - 2;
            lvlButtons[levelIndex].interactable = true;
            ColorBlock colorBlock = lvlButtons[levelIndex].colors;
            colorBlock.disabledColor = Color.white; // Rétablir la couleur normale pour le niveau activé
            lvlButtons[levelIndex].colors = colorBlock;
        }
    }
}