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
        public TextMeshProUGUI priceText;
    }

    public List<CharacterData> characters = new List<CharacterData>();
    public Vector3 characterSpawnPosition = Vector3.zero;
    public Vector3 characterRotation = new Vector3(0, 180, 0);
    public Color priceColor = Color.white;

    private GameObject currentDisplayedCharacter;
    private TextMeshProUGUI currentDisplayedPrice;
    private bool isDefaultCharacter = true;

    void Start()
    {
        SetupEventTriggers();
        HideAllPrices();

        if (characters.Count > 0)
        {
            DisplayDefaultCharacter(characters[0]);
        }
    }

    void HideAllPrices()
    {
        foreach (var character in characters)
        {
            if (character.priceText != null)
            {
                character.priceText.color = priceColor;
                character.priceText.gameObject.SetActive(false);
            }
        }
    }

    void SetupEventTriggers()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            var character = characters[i];
            var characterIndex = i; // Capture l'index pour l'utiliser dans les callbacks

            EventTrigger trigger = character.characterImage.gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
                trigger = character.characterImage.gameObject.AddComponent<EventTrigger>();

            if (characterIndex == 0) // Pour le personnage par défaut (Element 0)
            {
                EventTrigger.Entry entryEnter = new EventTrigger.Entry();
                entryEnter.eventID = EventTriggerType.PointerEnter;
                entryEnter.callback.AddListener((data) => {
                    if (!isDefaultCharacter)
                    {
                        DisplayDefaultCharacter(character);
                    }
                    ShowPrice(character.priceText);
                });
                trigger.triggers.Add(entryEnter);

                EventTrigger.Entry entryExit = new EventTrigger.Entry();
                entryExit.eventID = EventTriggerType.PointerExit;
                entryExit.callback.AddListener((data) => {
                    HidePrice(character.priceText);
                });
                trigger.triggers.Add(entryExit);
            }
            else // Pour les autres personnages
            {
                EventTrigger.Entry entryEnter = new EventTrigger.Entry();
                entryEnter.eventID = EventTriggerType.PointerEnter;
                entryEnter.callback.AddListener((data) => {
                    isDefaultCharacter = false;
                    DisplayCharacterAndPrice(character);
                });
                trigger.triggers.Add(entryEnter);

                EventTrigger.Entry entryExit = new EventTrigger.Entry();
                entryExit.eventID = EventTriggerType.PointerExit;
                entryExit.callback.AddListener((data) => {
                    HidePrice(character.priceText);
                });
                trigger.triggers.Add(entryExit);

                EventTrigger.Entry entryClick = new EventTrigger.Entry();
                entryClick.eventID = EventTriggerType.PointerClick;
                entryClick.callback.AddListener((data) => {
                    isDefaultCharacter = false;
                    DisplayCharacterAndPrice(character);
                });
                trigger.triggers.Add(entryClick);
            }
        }
    }

    void DisplayDefaultCharacter(CharacterData character)
    {
        if (currentDisplayedCharacter != null)
        {
            Destroy(currentDisplayedCharacter);
        }

        Vector3 worldSpawnPosition = transform.position + characterSpawnPosition;
        currentDisplayedCharacter = Instantiate(character.characterPrefab, worldSpawnPosition, Quaternion.Euler(characterRotation));
        currentDisplayedCharacter.transform.SetParent(transform);

        if (currentDisplayedPrice != null)
        {
            currentDisplayedPrice.gameObject.SetActive(false);
            currentDisplayedPrice = null;
        }

        Animator animator = currentDisplayedCharacter.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("Idle");
        }

        isDefaultCharacter = true;
    }

    void ShowPrice(TextMeshProUGUI priceText)
    {
        if (currentDisplayedPrice != null && currentDisplayedPrice != priceText)
        {
            currentDisplayedPrice.gameObject.SetActive(false);
        }

        if (priceText != null)
        {
            priceText.gameObject.SetActive(true);
            currentDisplayedPrice = priceText;
        }
    }

    void DisplayCharacterAndPrice(CharacterData character)
    {
        if (currentDisplayedCharacter != null)
        {
            Destroy(currentDisplayedCharacter);
        }

        Vector3 worldSpawnPosition = transform.position + characterSpawnPosition;
        currentDisplayedCharacter = Instantiate(character.characterPrefab, worldSpawnPosition, Quaternion.Euler(characterRotation));
        currentDisplayedCharacter.transform.SetParent(transform);

        if (currentDisplayedPrice != null)
        {
            currentDisplayedPrice.gameObject.SetActive(false);
        }

        if (character.priceText != null)
        {
            character.priceText.gameObject.SetActive(true);
            currentDisplayedPrice = character.priceText;
        }

        Animator animator = currentDisplayedCharacter.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("Idle");
        }
    }

    void HidePrice(TextMeshProUGUI priceText)
    {
        if (priceText != null)
        {
            priceText.gameObject.SetActive(false);
            if (currentDisplayedPrice == priceText)
            {
                currentDisplayedPrice = null;
            }
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