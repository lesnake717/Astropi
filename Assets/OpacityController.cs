using UnityEngine;
using UnityEngine.UI; // N�cessaire pour manipuler les Images UI

public class OpacityController : MonoBehaviour
{
    [SerializeField] private Image firstRect;     // Rectangle Battle Royal
    [SerializeField] private Image secondRect;    // Rectangle Super Battle
    [SerializeField] private Image thirdRect;     // Rectangle Demo

    [Range(0f, 1f)]
    [SerializeField] private float reducedOpacity = 0.5f; // Valeur d'opacit� r�duite

    private void Start()
    {
        // Configuration initiale
        SetDefaultOpacity();
    }

    private void SetDefaultOpacity()
    {
        // Premier rectangle garde son opacit� normale
        SetImageOpacity(firstRect, 1f);
        // Les autres ont une opacit� r�duite
        SetImageOpacity(secondRect, reducedOpacity);
        SetImageOpacity(thirdRect, reducedOpacity);
    }

    public void OnSecondRectHover()
    {
        SetImageOpacity(firstRect, reducedOpacity);
        SetImageOpacity(secondRect, 1f);
        SetImageOpacity(thirdRect, reducedOpacity);
    }

    public void OnThirdRectHover()
    {
        SetImageOpacity(firstRect, reducedOpacity);
        SetImageOpacity(secondRect, reducedOpacity);
        SetImageOpacity(thirdRect, 1f);
    }

    public void OnRectExit()
    {
        SetDefaultOpacity();
    }

    private void SetImageOpacity(Image image, float opacity)
    {
        Color color = image.color;
        color.a = opacity;
        image.color = color;
    }
}