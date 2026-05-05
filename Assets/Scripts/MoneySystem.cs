using TMPro;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    Transform transform1;

   int Money = 0;

    [SerializeField] TextMeshProUGUI textMeshPro; 
    
    void Start()
    {
        transform1 = GetComponent<Transform>();
        textMeshPro.text = Money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 90 * Time.deltaTime);


       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Money++;
        }
    }

}


