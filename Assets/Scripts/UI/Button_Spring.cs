using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Button_Spring : MonoBehaviour,Icurrent
{
    [SerializeField] public int Spring;
    [SerializeField] private TextMeshProUGUI textMeshPro;

    private Button button;

    public int current { get; set; }

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButton);
        UpdateText();
    }

  private void OnButton()
    {
        Spring--;

        current++;

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
