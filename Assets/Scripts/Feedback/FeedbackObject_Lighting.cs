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
    private Color resultantColour;
    public ColourLerping colourLerping;


    
    void Start()
    {
        _light = GetComponent<Light>();
        _renderer = GetComponentInParent<Renderer>();

        intensity = _light.intensity;

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

    public override void AdjustFeedback(float value, int index)
    {
        
        evaluatedControllers[index] = value;

        float sum = 0f;
        for (int i = 0; i < evaluatedControllers.Count; i++)
        {
            sum += evaluatedControllers[i];
        }

        DetermineFeedback(colourLerping.SetLerpValue(sum));
    }

    
    public void DetermineFeedback(Color value)
    {
        resultantColour = value;
        Debug.Log($" Adjusting the Lighting to: {resultantColour}");
        _light.color = resultantColour * intensity;
        if (!ReferenceEquals(_renderer, null))
        {
            _renderer.GetPropertyBlock(_propertyBlock, _materialIndex);

            _propertyBlock.SetColor("_EmissionColor", resultantColour * intensity);

            _renderer.SetPropertyBlock(_propertyBlock, _materialIndex);
        }
    }
    
}
