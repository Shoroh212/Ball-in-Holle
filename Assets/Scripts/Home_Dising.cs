using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Home_Dising : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(100, 100);

    private BuildingHosuse[,] grid;
    private BuildingHosuse flyingBuilding;
    private Camera mainCamera;

    private void Awake()
    {
        grid = new BuildingHosuse[GridSize.x, GridSize.y];
        
       // PlayerPrefs.GetInt("House", grid);
        mainCamera = Camera.main;
    }

    public void StartPlacingBuilding(BuildingHosuse buildingPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }

        //  boxCollider.isTrigger = true;
        flyingBuilding = Instantiate(buildingPrefab);
        SetAllCollidersTrigger(flyingBuilding.gameObject, true);



    }

    private void Update()
    {
        if (flyingBuilding == null) return;

        Vector2 screenPos = GetPrimaryPointerScreenPosition();
        if (screenPos == Vector2.zero && Mouse.current == null && Touchscreen.current == null) return;

        if (IsPointerOverUI(screenPos))
        {
            flyingBuilding.SetTransparent(false);
            return;
        }

        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = mainCamera.ScreenPointToRay(screenPos);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 worldPosition = ray.GetPoint(distance);

            // Ставим в реальную позицию без ограничений по сетке
            flyingBuilding.transform.position = new Vector3(worldPosition.x, 0, worldPosition.z);
            if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
            {
                flyingBuilding.transform.Rotate(0, 90f, 0);
            }
            // При клике — закрепляем
            if (IsPrimaryClick())
            {
                PlaceFlyingBuilding();
            }
        }
    }

    private void PlaceFlyingBuilding()
    {
        SetAllCollidersTrigger(flyingBuilding.gameObject, false);
        flyingBuilding.SetNormal();
        flyingBuilding = null;
        //boxCollider.isTrigger = false;
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

    private bool IsPointerOverUI(Vector2 screenPosition)
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
}


