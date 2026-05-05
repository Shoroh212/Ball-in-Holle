using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingsHouse : MonoBehaviour
{
    private BuildingHosuse flyingBuilding;
    private Camera mainCamera;

    [SerializeField] BoxCollider2D boxCollider;

    private float lastClickTime = 0f;
    private const float doubleClickThreshold = 0.3f;
    private float maxDistance = 100;
 
    //  можно посмотреть, можно ли поставить в текущей позиции
    private bool canPlace = true;

    private void Awake()
    {
        mainCamera = Camera.main;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
    }

    public void StartPlacingBuilding(BuildingHosuse buildingPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }

        flyingBuilding = Instantiate(buildingPrefab);
       
        SetAllCollidersTrigger(flyingBuilding.gameObject, true);
    }

    private void Update()
    {
        if (flyingBuilding == null) return;

        Vector2 screenPos = GetPrimaryPointerScreenPosition();
        if (screenPos == Vector2.zero && Mouse.current == null && Touchscreen.current == null) return;

        if (IsPointerOver(screenPos))
        {
            flyingBuilding.SetTransparent(false);
            return;
        }

        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = mainCamera.ScreenPointToRay(screenPos);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 worldPosition = ray.GetPoint(distance);

          
            flyingBuilding.transform.position = new Vector3(worldPosition.x, 0, worldPosition.z);

            if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
            {
                flyingBuilding.transform.Rotate(0, 0, 90f);
            }

         
            canPlace = !IsOverlapping(flyingBuilding.gameObject);

        
            if (IsPrimaryClick())
            {
                if (canPlace)
                {
                    PlaceFlyingBuilding();
                }
                else
                {
                    Debug.Log("Нельзя поставить здесь  объект пересекается с другим объектом сцены");
                    
                }
            }
        }
    }

    private void PlaceFlyingBuilding()
    {
        if (flyingBuilding == null) return;

        SetAllCollidersTrigger(flyingBuilding.gameObject, false);
        flyingBuilding.SetNormal();
        flyingBuilding = null;
    }

    private Vector2 GetPrimaryPointerScreenPosition()
    {
        if (Touchscreen.current != null)
        {
            foreach (var t in Touchscreen.current.touches)
            {
                if (t.press.isPressed)
                    return t.position.ReadValue();
            }
        }

        if (Mouse.current != null)
            return Mouse.current.position.ReadValue();

        return Vector2.zero;
    }

    private bool IsPrimaryClick()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame) return true;

        if (Touchscreen.current != null)
        {
            foreach (var t in Touchscreen.current.touches)
            {
                if (t.press.wasPressedThisFrame) return true;
            }
        }

        return false;
    }

    private bool IsPointerOver(Vector2 screenPosition)
    {
        if (EventSystem.current == null) return false;

        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = screenPosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        return results.Count > 0;
    }

    private void SetAllCollidersTrigger(GameObject obj, bool isTrigger)
    {
        if (obj == null) return;
        Collider[] colliders = obj.GetComponentsInChildren<Collider>(true);
        foreach (var c in colliders)
            c.isTrigger = isTrigger;
    }

    private bool IsOverlapping(GameObject obj)
    {
        if (obj == null) return false;

        Bounds bounds = GetCombinedBounds(obj);

       
        if (bounds.size == Vector3.zero)
        {
            bounds = new Bounds(obj.transform.position, Vector3.one * 0.5f);
        }

   
        Collider[] hits = Physics.OverlapBox(bounds.center, bounds.extents, obj.transform.rotation, ~0, QueryTriggerInteraction.Collide);

        foreach (var hit in hits)
        {
            if (hit == null) continue;
            // игнорируем колайдеры которые являются частью самого ставимого объекта
            if (hit.transform.IsChildOf(obj.transform)) continue;
            // дополнительная защита если корневой объект совпадает
            if (hit.transform.root == obj.transform.root) continue;

          
            return true;
        }

        return false;
    }

   
    private Bounds GetCombinedBounds(GameObject obj)
    {
        Collider[] cols = obj.GetComponentsInChildren<Collider>(true);
        if (cols != null && cols.Length > 0)
        {
            Bounds b = cols[0].bounds;
            for (int i = 1; i < cols.Length; i++)
            {
                b.Encapsulate(cols[i].bounds);
            }
            return b;
        }

        Renderer[] rends = obj.GetComponentsInChildren<Renderer>(true);
        if (rends != null && rends.Length > 0)
        {
            Bounds b = rends[0].bounds;
            for (int i = 1; i < rends.Length; i++)
            {
                b.Encapsulate(rends[i].bounds);
            }
            return b;
        }

       
        return new Bounds(Vector3.zero, Vector3.zero);
    }
}
