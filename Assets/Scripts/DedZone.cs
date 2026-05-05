using UnityEngine;
using UnityEngine.SceneManagement;

public class DedZone : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Physics.gravity = new Vector3(0,0,0);
        }
    }
}
