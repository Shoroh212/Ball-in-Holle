using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win_Script : MonoBehaviour
{
    [SerializeField] GameObject PanelWin;
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }


      private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            StartCoroutine(WaitAndPrint());

        }
        
    }
    IEnumerator WaitAndPrint()
    {



        yield return new WaitForSeconds(3f);
        PanelWin.SetActive(true);
    }

}
