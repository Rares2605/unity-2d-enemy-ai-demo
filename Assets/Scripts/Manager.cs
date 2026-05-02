using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Manager : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public EnemyAI realHp;
    public GameObject gameOver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        healthText.text = "HP: " + realHp.health.ToString();
        if(realHp.health ==0) { gameOver.SetActive(true); Time.timeScale = 0;
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

}
