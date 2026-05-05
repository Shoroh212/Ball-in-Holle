using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Button_Cube : MonoBehaviour,Icurrent
{
    [SerializeField] public int cube ;
    [SerializeField] private TextMeshProUGUI textMeshPro;

    private Button button;

    public int current {  get; set; }
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButton);
        Updatetext();
    }

  public void OnButton()
    {
        cube--;

        current++;

        if (cube <= 0)
        {

            cube = 0; 
            button.interactable = false; 


        }

        Updatetext();
    }

    private void Updatetext()
    {
        textMeshPro.text = cube.ToString();
    }
}
