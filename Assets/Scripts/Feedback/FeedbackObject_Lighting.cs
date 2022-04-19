using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FeedbackObject_Lighting : FeedbackObject
{
    private Light _light;

    public float intensity;
    private Renderer _renderer;
    private MaterialPropertyBlock _propertyBlock ;
    private Material _lightMaterial;
    private int _materialIndex;

    void Start()
    {
        _light = GetComponent<Light>();
        _renderer = GetComponentInParent<Renderer>();

        _propertyBlock = new MaterialPropertyBlock();

        if (_renderer != null)
        {
            
            for(int i=0;i< _renderer.sharedMaterials.Length;i++)
            {
                Material material = _renderer.sharedMaterials[i];
                Debug.Log(material.name);
                // Checks if material is a light material. If not then return either the previous light material or null
                _lightMaterial = material.name.Contains("Light") ? material : (_lightMaterial != null) ? _lightMaterial : null;
                _materialIndex = material.name.Contains("Light") ? i : (_lightMaterial != null) ? _materialIndex : -1;
            }
        }
    }

    public override void AdjustFeedback(Color value)
    {
        Debug.Log($" Adjusting the Lighting to: {value}");
        _light.color = value;
        if (!ReferenceEquals(_renderer, null))
        {
            _renderer.GetPropertyBlock(_propertyBlock, _materialIndex);

            _propertyBlock.SetColor("_EmissionColor", value * intensity);

            _renderer.SetPropertyBlock(_propertyBlock, _materialIndex);
        }
    }

}
