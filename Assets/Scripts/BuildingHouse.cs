using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHosuse : MonoBehaviour
{
    public Renderer MainRenderer;
    public Vector2Int Size = Vector2Int.one;

    [Header("Materials")]
    public Material alternateMaterial; // назначить в инспекторе Ч материал, на который будем переключатьс€

    // сохран€ем оригинал, чтобы можно было вернуть
    private Material originalMaterial;



   


    private void Awake()
    {
        if (MainRenderer == null)
            MainRenderer = GetComponent<Renderer>();
        if (MainRenderer != null)
            originalMaterial = MainRenderer.material; 
    }

     void Start()
    {
    

    }

   void List()
    {
       // foreach (Rigidbody rb in bodyList)
        {
            //rb.isKinematic = true;
        }
    }
    void ListKinimatic()
    {
        //foreach (Rigidbody rb in bodyList)
        {
            //rb.isKinematic = false;
        }
    }

    public void SetAlternateMaterial(bool useAlternate)
    {
        if (MainRenderer == null) return;

        if (useAlternate)
        {
            if (alternateMaterial != null)
                MainRenderer.material = alternateMaterial;
            else
                Debug.LogWarning("alternateMaterial не назначен в инспекторе.");
        }
        else
        {
            if (originalMaterial != null)
                MainRenderer.material = originalMaterial;
        }
    }

    // ”станавливает прозрачность текущего материала (мен€ет альфу цвета)
    public void SetTransparent(bool available)
    {
        if (MainRenderer == null) return;

        Color color = MainRenderer.material.color;

        if (available)
        {
            // альфа = 0.3 Ч полупрозрачный
            color.a = 0.3f;
        }
        else
        {
           
            color.a = 1f;
        }

        MainRenderer.material.color = color;
    }

    // ¬озвращает материал и ставит белый цвет
    public void SetNormal()
    {
        if (MainRenderer == null) return;

        if (originalMaterial != null)
            MainRenderer.material = originalMaterial;

        MainRenderer.material.color = Color.white;
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                if ((x + y) % 2 == 0) Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
                else Gizmos.color = new Color(1f, 0.68f, 0f, 0.3f);

                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, .1f, 1));
            }
        }
    }
}
