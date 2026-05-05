using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Edit_Script : MonoBehaviour
{
    public  TMP_Dropdown qualityDropdown;
    public Button Play;
    public Button Main;
    public GameObject panel;
    void Start()
    {
       

        // Если не назначили слайдер вручную — найдём его
        if (volumeSlider == null)
            volumeSlider = GetComponent<Slider>();

        // Устанавливаем начальное значение (можно запомнить через PlayerPrefs)
        volumeSlider.value = musicSource.volume;

        // Подписываем метод на изменение слайдера
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void Hight()
    {

        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);

    }

    public void Medium()
    {

        Screen.SetResolution(1440, 900, FullScreenMode.FullScreenWindow);

    }

  
            
    public void Low()
    {

        Screen.SetResolution(1920, 1080, FullScreenMode.FullScreenWindow);

    }




    [SerializeField] private AudioSource musicSource; // источник звука
    [SerializeField] private Slider volumeSlider;    

  

    private void ChangeVolume(float value)
    {
        musicSource.volume = value; // громкость 
    }


    public void exit()
    {
        panel.SetActive(false);
    }

    public void input()
    {
        panel.SetActive(true);
    }

    public void main()
    {
        SceneManager.LoadScene(0);
    }
}



