using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Button_Furniture : MonoBehaviour
{
    [SerializeField] private int Spring;
    [SerializeField] private TextMeshProUGUI textMeshPro;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButton);
        UpdateText();
    }

    private void OnButton()
    {
        Spring--; 



        if (Spring <= 0)
        {
            Spring = 0; 
            button.interactable = false; 
        }

        UpdateText(); 
    }

    private void UpdateText()
    {
        textMeshPro.text = Spring.ToString();
    }
}
