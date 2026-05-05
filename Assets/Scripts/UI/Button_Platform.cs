using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Button_Platform : MonoBehaviour, Icurrent
{
    [SerializeField] public int platform; 
    [SerializeField] private TextMeshProUGUI textMeshPro;

    private Button button;

    public int current {  get;  set; }

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButton);
        UpdateText();
    }

    private void OnButton()
    {
        platform--;

        current++;

        if (platform <= 0)
        {
            platform = 0;   

            button.interactable = false;  
        }

        UpdateText(); 


    }

    private void UpdateText()
    {
        textMeshPro.text = platform.ToString();
    }
}
