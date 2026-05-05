using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class MobileCameraController : MonoBehaviour
{
    [Header("Настройки камеры")]
    public float moveSpeed = 0.1f;       
    public float minX = -10f, maxX = 10f; // границы по X
    public float minZ = -10f, maxZ = 10f; // границы по Z

    private Camera cam;
    private Vector2 lastPointerPosition;
    private bool dragging;

    private InputAction positionAction;
    private InputAction pressAction;

    private void Awake()
    {
        cam = Camera.main ?? GetComponent<Camera>();
    }

    private void OnEnable()
    {
      // НЕ ТРОГАТЬ !! 
        positionAction = new InputAction("PointerPosition", binding: "<Pointer>/position");
        pressAction = new InputAction("PrimaryPress", binding: "<Pointer>/press");

        pressAction.started += OnPressStarted;
        pressAction.canceled += OnPressCanceled;

        positionAction.Enable();
        pressAction.Enable();
    }

    private void OnDisable()
    {
        if (pressAction != null)
        {
            pressAction.started -= OnPressStarted;
            pressAction.canceled -= OnPressCanceled;
            pressAction.Disable();
            pressAction.Dispose();
            pressAction = null;
        }

        if (positionAction != null)
        {
            positionAction.Disable();
            positionAction.Dispose();
            positionAction = null;
        }
    }

    private void Update()
    {
       
        if (dragging && positionAction != null)
        {
            Vector2 currentPos = positionAction.ReadValue<Vector2>();
            Vector2 delta = currentPos - lastPointerPosition;
            lastPointerPosition = currentPos;

          
            Vector3 move = new Vector3(-delta.x * moveSpeed, 0f, -delta.y * moveSpeed);
            cam.transform.position += move;

            // Ограничиваем по заданным границам
            float clampedX = Mathf.Clamp(cam.transform.position.x, minX, maxX);
            float clampedZ = Mathf.Clamp(cam.transform.position.z, minZ, maxZ);
            cam.transform.position = new Vector3(clampedX, cam.transform.position.y, clampedZ);
        }

        
    }

    private void OnPressStarted(InputAction.CallbackContext ctx)
    {
        
        if (positionAction == null) return;

        lastPointerPosition = positionAction.ReadValue<Vector2>();

        
        if (IsPointerOverUI(lastPointerPosition))
        {
            dragging = false;
        }
        else
        {
            dragging = true;
        }
    }

    private void OnPressCanceled(InputAction.CallbackContext ctx)
    {
        dragging = false;
    }


    private bool IsPointerOverUI(Vector2 screenPosition)
    {
        if (EventSystem.current == null) return false;

        PointerEventData ped = new PointerEventData(EventSystem.current)
        {
            position = screenPosition
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(ped, results);
        return results.Count > 0;
    }
}
