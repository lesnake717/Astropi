using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

public class CharacterPreviewManager : MonoBehaviour
{
    [System.Serializable]
    public class CharacterData
    {
        public Image characterImage;
        public GameObject characterPrefab;
        public string price;
        public TextMeshProUGUI priceText;
    }

    public List<CharacterData> characters = new List<CharacterData>();
    public Vector3 characterSpawnPosition = Vector3.zero;
    public Vector3 characterRotation = new Vector3(0, 180, 0);
    public Color priceColor = Color.white;

    private GameObject currentDisplayedCharacter;
    private TextMeshProUGUI currentDisplayedPrice;

    void Start()
    {
        SetupEventTriggers();
        HideAllPrices(); // Cache tous les prix au démarrage
    }

    void HideAllPrices()
    {
        foreach (var character in characters)
        {
            if (character.priceText != null)
            {
                character.priceText.text = character.price;
                character.priceText.color = priceColor;
                character.priceText.gameObject.SetActive(false); // Cache le texte
            }
        }
    }

    void SetupEventTriggers()
    {
        foreach (var character in characters)
        {
            EventTrigger trigger = character.characterImage.gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = character.characterImage.gameObject.AddComponent<EventTrigger>();

            // Configuration du hover (PointerEnter)
            EventTrigger.Entry entryEnter = new EventTrigger.Entry();
            entryEnter.eventID = EventTriggerType.PointerEnter;
            entryEnter.callback.AddListener((data) => {
                DisplayCharacterAndPrice(character);
            });
            trigger.triggers.Add(entryEnter);

            // Configuration du hover exit (PointerExit)
            EventTrigger.Entry entryExit = new EventTrigger.Entry();
            entryExit.eventID = EventTriggerType.PointerExit;
            entryExit.callback.AddListener((data) => {
                // Ne cache le prix que si aucun personnage n'est actuellement sélectionné par clic
                if (currentDisplayedPrice != character.priceText)
                {
                    HidePrice(character.priceText);
                }
            });
            trigger.triggers.Add(entryExit);

            // Configuration du click
            EventTrigger.Entry entryClick = new EventTrigger.Entry();
            entryClick.eventID = EventTriggerType.PointerClick;
            entryClick.callback.AddListener((data) => {
                DisplayCharacterAndPrice(character);
            });
            trigger.triggers.Add(entryClick);
        }
    }

    void DisplayCharacterAndPrice(CharacterData character)
    {
        // Gérer l'affichage du personnage
        if (currentDisplayedCharacter != null)
        {
            Destroy(currentDisplayedCharacter);
        }

        Vector3 worldSpawnPosition = transform.position + characterSpawnPosition;
        currentDisplayedCharacter = Instantiate(character.characterPrefab, worldSpawnPosition, Quaternion.Euler(characterRotation));
        currentDisplayedCharacter.transform.SetParent(transform);

        // Gérer l'animation si elle existe
        Animator animator = currentDisplayedCharacter.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("Idle");
        }

        // Gérer l'affichage du prix
        if (currentDisplayedPrice != null)
        {
            currentDisplayedPrice.gameObject.SetActive(false);
        }

        if (character.priceText != null)
        {
            character.priceText.gameObject.SetActive(true);
            currentDisplayedPrice = character.priceText;
        }
    }

    void HidePrice(TextMeshProUGUI priceText)
    {
        if (priceText != null)
        {
            priceText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (currentDisplayedCharacter != null && Input.GetKey(KeyCode.Mouse1))
        {
            float rotationSpeed = 100f;
            float mouseX = Input.GetAxis("Mouse X");
            currentDisplayedCharacter.transform.Rotate(Vector3.up, -mouseX * rotationSpeed * Time.deltaTime);
        }
    }
}