
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XInput;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class Finish : Stars_System, Icurrent
{
   public int current { get; set; }
    private int  sum_max;
    public static int HP = 3;
    [SerializeField] private TextMeshProUGUI one;
    [SerializeField] private float fadeDuration = 2f;
    /*public int _hp
    {
        get { return HP; }
        set { HP = value; }
    }
    */
    Button_Spring button_Spring;
    Button_Cube button_Cube;
    Button_Platform button_Platform;

    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private SpriteRenderer Animationmesh;
    [SerializeField] private SpriteRenderer player;
    public Game_Ball game_Ball;

    Game_Ball gamescript;
    public Button butt1;
    public Button butt2;
    public Button butt3;
    public Button butt4;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource mus;



    [SerializeField] private GameObject PanelLose;
    [SerializeField] private GameObject PanelCore;

    void Start()
    {
        //StartCoroutine(WaitAndPrint());
        game_Ball.enabled = false;
        rb.constraints = RigidbodyConstraints.FreezePosition;

        sum_max = button_Platform.platform + button_Cube.cube + button_Spring.Spring;

        

    }
    void Update()
    {
        if (current >= sum_max * 0.5f)
        {
           
        }
        else
        {

            
        }

        if (HP <= 0 )
        {
            PanelCore.SetActive(false);
            PanelLose.SetActive(true);
        }
    }

   public void StartGame()
    {
        game_Ball.enabled = true;
        // Game_Ball.GetComponent<Rigidbody>().isKinematic = false;
        audioSource.enabled = true;
        mus.enabled = false;
        butt1.enabled = false; butt2.enabled = false; butt3.enabled = false; butt4.enabled = false;
        //Platform.enabled = true;
        // StartCoroutine(WaitAndPrint());
        rb.constraints = RigidbodyConstraints.None;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mesh.enabled = false;
            player.enabled = false;
            Animationmesh.enabled = true;
            //audioSource.Play();
            // SceneManager.LoadScene(2);
             game_Ball.enabled = false;
            StartCoroutine(WaitAndPrint());
            WaitAndPrint();
  
        }
    }

    IEnumerator WaitAndPrint()
    {
        
        Debug.Log("Победа");

        yield return new WaitForSeconds(3f);

        
        Scene currentScene = SceneManager.GetActiveScene();

        
        if (currentScene.name != "lvl2")
        {
            int nextSceneIndex = currentScene.buildIndex + 2;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
           
        }

        Physics.gravity = new Vector3(0, 0, 0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Physics.gravity = new Vector3(0, 0, 0);
        HP--;
       

    }


    public void RestartLVL()
    {

        Time.timeScale = 1f;          
        PlayerPrefs.DeleteAll();  
       
        SceneManager.LoadScene(1);



    }

    public void Scenelvl2()
    {
        SceneManager.LoadScene(6);
    }

}



interface Icurrent
{
   public int current { get; set; }
}
