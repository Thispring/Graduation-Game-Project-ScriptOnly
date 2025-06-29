using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomerUnit : MonoBehaviour
{
    private static ZoomerUnit instance;
    private new static Renderer renderer;
    private SpriteRenderer spriteRenderer;
    

    private void Awake() 
    {
        instance = this;
        spriteRenderer = GetComponent<SpriteRenderer>();
        renderer = GetComponent<Renderer>();
    }

    public static void SetSprite(Sprite cardSprite)  
    {
        instance.spriteRenderer.sprite = cardSprite;    
    }

    public static void SetMaterial(Material material)
    {
        if (renderer != null)
        {
            renderer.material = material;
        }
        else
        {
            Debug.LogError("Renderer component not found on Zoomer.");
        }
    }
}