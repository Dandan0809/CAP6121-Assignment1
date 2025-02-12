using UnityEngine;
using TMPro;

public class UpdateMissCount : MonoBehaviour
{
    public TextMeshProUGUI missText;  
    public SaberDeflect saberDeflect;

    private void Update()
    {
        if (missText != null && saberDeflect != null)
        {
            int missCount = saberDeflect.shotCount - saberDeflect.deflectCount;
            missText.text = "Misses: " + missCount;
        }
    }
}
