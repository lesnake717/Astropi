using UnityEngine;
using UnityEngine.Video;

public class GestionGenenerique : MonoBehaviour
{
    public VideoPlayer videoGenerique;
    [SerializeField] private GestionBoutonJouer gestionBoutonJouer;

    void Start()
    {
        videoGenerique.loopPointReached += FinGenerique;
        Debug.Log("Vidéo générique est prête à jouer");
    }

    void FinGenerique(VideoPlayer vp)
    {
        if (gestionBoutonJouer != null)
        {
            Debug.Log("Vidéo générique a fini de jouer, affichage du bouton");
            gestionBoutonJouer.AfficherBouton();
        }
        else
            Debug.LogError("GestionBoutonJouer n'est pas assigné dans l'inspecteur.");
    }
}