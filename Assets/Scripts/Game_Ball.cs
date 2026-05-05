
using TMPro;
using UnityEngine;


public class Game_Ball : MonoBehaviour
{
   
    

    [SerializeField] protected TextMeshProUGUI textTime;

 


    public static float time;
   public static int Money_amount = 0;
    [SerializeField] TextMeshProUGUI starstext;



    void Start()
    {
       

         Physics.gravity = new Vector3(0,0, -9.81f);
        starstext.text = Money_amount.ToString();

        time = 0f; // начальное значение времени
    }

    private void Update()
    {
        time += Time.deltaTime;
        textTime.text = time.ToString("F1");
        if (time > 50)
        {
            Money_amount -= 1; 
        }
    }


  








}
