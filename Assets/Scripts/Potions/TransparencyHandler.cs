
using System.Collections;
using UnityEngine;
public class TransparencyHandler : MonoBehaviour
{
    public static void SetObjectTransparency(GameObject go, float transparency)
    {
        //Debug.Log("Transparency: " + transparency);
        Renderer[] renderers = go.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            //Debug.Log("Has color property: " + renderer.material.HasProperty("_Color"));
            Color color = renderer.material.color;
            color.a = transparency;
            renderer.material.color = color;
        }
    }
    
    public static IEnumerator FadeIn(GameObject go, float duration)
    {
        //Debug.Log("In Fade In");
        float elapsed = 0f;
        while (elapsed < duration)
        {
            SetObjectTransparency(go, Mathf.Lerp(0f, 1f, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
    
    public static IEnumerator FadeOut(GameObject go, float duration)
    {
        //Debug.Log("In Fade Out");
        float elapsed = 0f;
        while (elapsed < duration)
        {
            SetObjectTransparency(go, Mathf.Lerp(1f, 0f, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}