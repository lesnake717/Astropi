using UnityEngine;
using UnityEngine.UI;

public class GestionAchatAutreBis_2 : MonoBehaviour
{
    public Text quantiteText;
    public Text prixText;
    public Text xpText;
    public Button boutonMoins;
    public Button boutonPlus;
    public Dropdown deviseDropdown;

    private int quantite = 2;
    private int quantiteMaximale = 9;
    private float prixInitial = 750000f;
    private int xpInitial = 10000000;
    private float tauxConversionEUR = 0.92f;
    private float tauxConversionGBP = 0.81f;

    private void Start()
    {
        boutonMoins.onClick.AddListener(DiminuerQuantite);
        boutonPlus.onClick.AddListener(AugmenterQuantite);
        deviseDropdown.onValueChanged.AddListener(ChangerDevise);
        MettreAJourPrix("USD");
        MettreAJourXP();
    }

    public void DiminuerQuantite()
    {
        if (quantite > 1)
        {
            quantite--;
            MettreAJourTexteQuantite();
            MettreAJourPrix(deviseDropdown.options[deviseDropdown.value].text);
            MettreAJourXP();
        }
    }

    public void AugmenterQuantite()
    {
        if (quantite < quantiteMaximale)
        {
            quantite++;
            MettreAJourTexteQuantite();
            MettreAJourPrix(deviseDropdown.options[deviseDropdown.value].text);
            MettreAJourXP();
        }
    }

    private void MettreAJourTexteQuantite()
    {
        quantiteText.text = quantite.ToString();
    }

    private void MettreAJourPrix(string devise)
    {
        float prixActuel = prixInitial * quantite;
        switch (devise)
        {
            case "EUR":
                prixActuel *= tauxConversionEUR;
                prixText.text = "€" + prixActuel.ToString("F2");
                break;
            case "GBP":
                prixActuel *= tauxConversionGBP;
                prixText.text = "£" + prixActuel.ToString("F2");
                break;
            default:
                prixText.text = "$" + prixActuel.ToString("F2");
                break;
        }
    }

    private void MettreAJourXP()
    {
        int xpActuel = xpInitial * quantite;
        xpText.text = xpActuel.ToString("N0") + " XP";
    }

    private void ChangerDevise(int indexDevise)
    {
        MettreAJourPrix(deviseDropdown.options[indexDevise].text);
    }
}