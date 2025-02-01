using UnityEngine;
using UnityEngine.UI;

public class RadiusManager : MonoBehaviour
{
    public float cornerRadius = 20f;

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // Utiliser un sprite existant avec transparence
            image.type = Image.Type.Sliced;
            image.pixelsPerUnitMultiplier = cornerRadius / 10f;
        }
    }
}