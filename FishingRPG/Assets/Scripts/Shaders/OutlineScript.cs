using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class OutlineScript : MonoBehaviour
{
    [SerializeField] private Material outlineMat;
    [SerializeField] private float outlineScaleFact;
    [SerializeField] private Color outlineColor;
    private Renderer outlineRenderer;

    private void Start()
    {
        outlineRenderer = CreateOutline(outlineMat, outlineScaleFact, outlineColor);
        outlineRenderer.enabled = true;
    }

    Renderer CreateOutline(Material _mat, float _scale, Color _color)
    {
        GameObject outlineObj = Instantiate(gameObject, transform.position, transform.rotation, transform);
        Renderer rend = outlineObj.GetComponent<Renderer>();

        rend.material = outlineMat;
        rend.material.SetColor("_OutlineColor", _color);
        rend.material.SetFloat("_Scale", _scale);
        rend.shadowCastingMode = ShadowCastingMode.Off;

        outlineObj.GetComponent<OutlineScript>().enabled = false;
        outlineObj.GetComponent<Collider>().enabled = false;

        rend.enabled = false;

        return rend;
    }
}
