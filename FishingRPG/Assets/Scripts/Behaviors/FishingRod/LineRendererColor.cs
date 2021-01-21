﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererColor : MonoBehaviour
{
    public MeshRenderer sphere;
    public Material mat;
    private ColorHSV matCol;

    /*public Gradient gr;
    GradientColorKey[] colorKeys;
    GradientAlphaKey[] alphaKeys;*/

    private void Start()
    {
        /*gr = new Gradient();

        matCol = mat.color;
        matCol.a = 255;

        colorKeys = new GradientColorKey[5];
        colorKeys[0].color = new Color(0, 255, 97);
        colorKeys[0].time = 0;
        colorKeys[0].color = new Color(255, 166, 69);
        colorKeys[0].time = 40;
        colorKeys[0].color = new Color(255, 166, 69);
        colorKeys[0].time = 50;
        colorKeys[0].color = new Color(255, 166, 69);
        colorKeys[0].time = 60;
        colorKeys[0].color = new Color(255, 25, 46);
        colorKeys[0].time = 100;

        alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 1;
        alphaKeys[0].time = 0;
        alphaKeys[1].alpha = 1;
        alphaKeys[1].time = 1;

        gr.SetKeys(colorKeys, alphaKeys);*/
        //matCol = new Color(0, 5, 2, 1);

        //sphere.material.SetColor("Color", currentCol);
        //sphere.material.SetColor("_Color", Random.ColorHSV());
        StartCoroutine(UpdateColor());
    }

   IEnumerator UpdateColor()
    {
        matCol = Random.ColorHSV();

        //sphere.material.SetColor("Color", currentCol);
        sphere.material.SetColor("_Color", matCol);

        Debug.Log("Sphere " + sphere.material.color);
        Debug.Log("Random " + matCol);

        yield return new WaitForSeconds(1);

        StartCoroutine(UpdateColor());
    }


}
