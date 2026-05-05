using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; 

public class NailScript : MonoBehaviour
{

    public Button button;
    public Camera mainCamera;
    [SerializeField] private GameObject Nailobject;

    private bool isModeActive = false;
    private Vector2 pointerPosition;     

    void Start()
    {
        button.onClick.AddListener(() =>
        {
            isModeActive = true;
        });

        if (mainCamera == null)
            mainCamera = Camera.main;
    }


    public void OnPoint(InputAction.CallbackContext context)
    {
        pointerPosition = context.ReadValue<Vector2>();
    }

    
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!isModeActive) return;

       
        if (context.performed)
        {
            Ray ray = mainCamera.ScreenPointToRay(pointerPosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
               
                Nailobject.transform.position = hit.point;

                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }

                isModeActive = false;
            }
        }
    }
}
