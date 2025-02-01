using UnityEngine;

public class GestionJeu : MonoBehaviour
{
    public GameObject[] prefabsPersonnages; // Tableau des préfabriqués de personnages
    public GameObject personnageParDefaut; // Personnage par défaut
    public Avatar MasculineAvatar; // Avatar pour les personnages masculins
    public Avatar FeminineAvatar;  // Avatar pour les personnages féminins
    public Avatar DefaultAvatar;   // Avatar par défaut (optionnel)

    private GameObject instancePersonnage;
    private GameObject objetParent; // GameObject "3"
    private Animator animatorParent; // Animator du GameObject "3"

    private void Start()
    {
        // Récupérer l'index du personnage sélectionné
        int indexChoisi = PlayerPrefs.GetInt("PersonnageChoisiIndex", -1);
        Debug.Log("Index du personnage sélectionné récupéré depuis PlayerPrefs : " + indexChoisi);

        // Trouver le GameObject "3" dans la scène
        objetParent = GameObject.Find("3");

        if (objetParent != null)
        {
            Debug.Log("GameObject '3' trouvé dans la scène.");

            // Récupérer l'Animator du GameObject "3"
            animatorParent = objetParent.GetComponent<Animator>();

            if (animatorParent != null)
            {
                Debug.Log("Animator trouvé sur le GameObject '3'.");

                // Supprimer le personnage par défaut existant
                Transform personnageParDefautTransform = objetParent.transform.Find("Personnage_1");
                if (personnageParDefautTransform != null)
                {
                    DestroyImmediate(personnageParDefautTransform.gameObject);
                }

                // Instancier le personnage choisi ou le personnage par défaut
                if (indexChoisi >= 0 && indexChoisi < prefabsPersonnages.Length)
                {
                    GameObject prefabPersonnageChoisi = prefabsPersonnages[indexChoisi];
                    instancePersonnage = Instantiate(prefabPersonnageChoisi, objetParent.transform);

                    // Remplacer l'avatar selon le personnage choisi
                    RemplacerAvatar(prefabPersonnageChoisi.name);
                }
                else
                {
                    instancePersonnage = Instantiate(personnageParDefaut, objetParent.transform);
                    Debug.LogWarning("Index de personnage invalide : " + indexChoisi + ". Le personnage par défaut a été instancié.");

                    // Assigner un avatar par défaut
                    if (DefaultAvatar != null)
                    {
                        animatorParent.avatar = DefaultAvatar;
                        Debug.Log("Avatar par défaut assigné avec succès.");
                    }
                    else
                    {
                        Debug.LogError("DefaultAvatar est null. Vérifiez son assignation dans l'Inspecteur.");
                    }
                }
            }
            else
            {
                Debug.LogError("Le GameObject '3' n'a pas de composant Animator.");
            }
        }
        else
        {
            Debug.LogError("Le GameObject '3' est introuvable dans la scène.");
        }
    }

    private void RemplacerAvatar(string prefabName)
    {
        // Vérifier que l'Animator du parent est bien récupéré
        if (animatorParent != null)
        {
            Debug.Log("Tentative d'assignation de l'avatar pour le préfabriqué : " + prefabName);

            // Assigner l'avatar en fonction du nom du préfabriqué
            if (prefabName.Contains("Masculin"))
            {
                if (MasculineAvatar != null)
                {
                    animatorParent.avatar = MasculineAvatar;
                    Debug.Log("Avatar masculin assigné avec succès.");
                }
                else
                {
                    Debug.LogError("MasculineAvatar est null. Vérifiez son assignation dans l'Inspecteur.");
                }
            }
            else if (prefabName.Contains("Feminin"))
            {
                if (FeminineAvatar != null)
                {
                    animatorParent.avatar = FeminineAvatar;
                    Debug.Log("Avatar féminin assigné avec succès.");
                }
                else
                {
                    Debug.LogError("FeminineAvatar est null. Vérifiez son assignation dans l'Inspecteur.");
                }
            }
            else
            {
                Debug.LogError("Nom du prefab ne contient pas 'Masculin' ou 'Feminin'. Nom du préfabriqué : " + prefabName);
            }

            // Vérifier si l'assignation a bien été effectuée
            if (animatorParent.avatar == null)
            {
                Debug.LogError("L'avatar reste vide après tentative d'assignation.");
            }
        }
        else
        {
            Debug.LogError("L'Animator du parent '3' est null.");
        }
    }
}