using UnityEngine;

public class GestionJeu : MonoBehaviour
{
    public GameObject[] prefabsPersonnages;
    public GameObject personnageParDefaut;
    public Avatar MasculineAvatar; // Référence à l'avatar à utiliser

    private GameObject instancePersonnage;
    private GameObject objetParent;
    private Animator animatorPersonnage;

    private void Start()
    {
        int indexChoisi = PlayerPrefs.GetInt("PersonnageChoisiIndex", -1);
        Debug.Log("Index du personnage sélectionné récupéré depuis PlayerPrefs : " + indexChoisi);

        objetParent = GameObject.Find("3");

        if (objetParent != null)
        {
            Transform personnageParDefautTransform = objetParent.transform.Find("Personnage_1");
            if (personnageParDefautTransform != null)
            {
                DestroyImmediate(personnageParDefautTransform.gameObject);
            }

            if (indexChoisi >= 0 && indexChoisi < prefabsPersonnages.Length)
            {
                GameObject prefabPersonnageChoisi = prefabsPersonnages[indexChoisi];
                instancePersonnage = Instantiate(prefabPersonnageChoisi, objetParent.transform);
                instancePersonnage.transform.parent = objetParent.transform;
                animatorPersonnage = instancePersonnage.GetComponent<Animator>();

                if (animatorPersonnage != null)
                {
                    // Attribuer le nouveau contrôleur d'animation vide
                    animatorPersonnage.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("NomDuControlleurVide");

                    // Changer l'avatar du GameObject "3" à "MasculineAvatar"
                    Animator objetParentAnimator = objetParent.GetComponent<Animator>();
                    if (objetParentAnimator != null && MasculineAvatar != null)
                    {
                        objetParentAnimator.avatar = MasculineAvatar;
                    }
                    else
                    {
                        Debug.LogError("Le GameObject '3' n'a pas de composant Animator ou 'MasculineAvatar' est null.");
                    }
                }
                else
                {
                    Debug.LogError("Le GameObject instancié n'a pas de composant Animator.");
                }
            }
            else
            {
                instancePersonnage = Instantiate(personnageParDefaut, objetParent.transform);
                instancePersonnage.transform.parent = objetParent.transform;
                animatorPersonnage = instancePersonnage.GetComponent<Animator>();
                Debug.LogWarning("Index de personnage invalide : " + indexChoisi + ". Le personnage par défaut a été instancié.");
            }

            VerifierAnimationsPersonnage();
        }
        else
        {
            Debug.LogError("Le parent '3' est introuvable dans la scène.");
        }
    }

    private void VerifierAnimationsPersonnage()
    {
        if (animatorPersonnage != null)
        {
            AnimatorStateInfo stateInfo = animatorPersonnage.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.shortNameHash != 0)
            {
                animatorPersonnage.applyRootMotion = true;
            }
            else
            {
                animatorPersonnage.applyRootMotion = false;
            }
        }
        else
        {
            Debug.LogError("Animator du personnage instancié est null.");
        }
    }
}
