using UnityEngine;
using UnityEngine.SceneManagement;

public class Stars_System : MonoBehaviour
{
    
    public GameObject sprite1; 
    public GameObject sprite2; 
    public GameObject sprite3;

   
    public float _currentTime; 
    public int _playerDeaths; 
    public int _minDeathsAmongPlayers; 

    void Start()
    {
        ActivateSprite();
    }

    void ActivateSprite()
    {
       
        if (_currentTime <= 20f)
        {
            ActivateOnly(sprite1);
            return;
        }

        if (_playerDeaths == _minDeathsAmongPlayers)
        {
            ActivateOnly(sprite2);
            return;
        }

        int randomIndex = Random.Range(0, 3);
        switch (randomIndex)
        {
            case 0:
                ActivateOnly(sprite1);
                break;
            case 1:
                ActivateOnly(sprite2);
                break;
            case 2:
                ActivateOnly(sprite3);
                break;
        }
    }

    void ActivateOnly(GameObject target)
    {
        sprite1.SetActive(target == sprite1);
        sprite2.SetActive(target == sprite2);
        sprite3.SetActive(target == sprite3);
    }


    public void Startgame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
