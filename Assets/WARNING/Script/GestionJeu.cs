using UnityEngine;

public class GestionJeu : MonoBehaviour
{
    public GameObject[] prefabsPersonnages; // Tableau des pr�fabriqu�s de personnages
    public GameObject personnageParDefaut; // Personnage par d�faut
    public Avatar MasculineAvatar; // Avatar pour les personnages masculins
    public Avatar FeminineAvatar;  // Avatar pour les personnages f�minins
    public Avatar DefaultAvatar;   // Avatar par d�faut (optionnel)

    private GameObject instancePersonnage;
    private GameObject objetParent; // GameObject "3"
    private Animator animatorParent; // Animator du GameObject "3"

    private void Start()
    {
        // R�cup�rer l'index du personnage s�lectionn�
        int indexChoisi = PlayerPrefs.GetInt("PersonnageChoisiIndex", -1);
        Debug.Log("Index du personnage s�lectionn� r�cup�r� depuis PlayerPrefs : " + indexChoisi);

        // Trouver le GameObject "3" dans la sc�ne
        objetParent = GameObject.Find("3");

        if (objetParent != null)
        {
            Debug.Log("GameObject '3' trouv� dans la sc�ne.");

            // R�cup�rer l'Animator du GameObject "3"
            animatorParent = objetParent.GetComponent<Animator>();

            if (animatorParent != null)
            {
                Debug.Log("Animator trouv� sur le GameObject '3'.");

                // Supprimer le personnage par d�faut existant
                Transform personnageParDefautTransform = objetParent.transform.Find("Personnage_1");
                if (personnageParDefautTransform != null)
                {
                    DestroyImmediate(personnageParDefautTransform.gameObject);
                }

                // Instancier le personnage choisi ou le personnage par d�faut
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
                    Debug.LogWarning("Index de personnage invalide : " + indexChoisi + ". Le personnage par d�faut a �t� instanci�.");

                    // Assigner un avatar par d�faut
                    if (DefaultAvatar != null)
                    {
                        animatorParent.avatar = DefaultAvatar;
                        Debug.Log("Avatar par d�faut assign� avec succ�s.");
                    }
                    else
                    {
                        Debug.LogError("DefaultAvatar est null. V�rifiez son assignation dans l'Inspecteur.");
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
            Debug.LogError("Le GameObject '3' est introuvable dans la sc�ne.");
        }
    }

    private void RemplacerAvatar(string prefabName)
    {
        // V�rifier que l'Animator du parent est bien r�cup�r�
        if (animatorParent != null)
        {
            Debug.Log("Tentative d'assignation de l'avatar pour le pr�fabriqu� : " + prefabName);

            // Assigner l'avatar en fonction du nom du pr�fabriqu�
            if (prefabName.Contains("Masculin"))
            {
                if (MasculineAvatar != null)
                {
                    animatorParent.avatar = MasculineAvatar;
                    Debug.Log("Avatar masculin assign� avec succ�s.");
                }
                else
                {
                    Debug.LogError("MasculineAvatar est null. V�rifiez son assignation dans l'Inspecteur.");
                }
            }
            else if (prefabName.Contains("Feminin"))
            {
                if (FeminineAvatar != null)
                {
                    animatorParent.avatar = FeminineAvatar;
                    Debug.Log("Avatar f�minin assign� avec succ�s.");
                }
                else
                {
                    Debug.LogError("FeminineAvatar est null. V�rifiez son assignation dans l'Inspecteur.");
                }
            }
            else
            {
                Debug.LogError("Nom du prefab ne contient pas 'Masculin' ou 'Feminin'. Nom du pr�fabriqu� : " + prefabName);
            }

            // V�rifier si l'assignation a bien �t� effectu�e
            if (animatorParent.avatar == null)
            {
                Debug.LogError("L'avatar reste vide apr�s tentative d'assignation.");
            }
        }
        else
        {
            Debug.LogError("L'Animator du parent '3' est null.");
        }
    }
}