
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    float appleTimer;
    float doubleJumpTimer;
    public float interval = 3;
    public float DJumpInterval = 30;
    public GameObject apple;
    public GameObject doubleJump;
    public static int points;
    public Text playerScoreText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Score.totalScore = points;
        playerScoreText.text = "Score: " + points;

        appleTimer += Time.deltaTime;
        if(appleTimer >= interval){
            spawnApple();
            appleTimer -= interval;
        }

        doubleJumpTimer += Time.deltaTime;
        if(doubleJumpTimer >= DJumpInterval){
            spawnDoubleJump();
            doubleJumpTimer -= DJumpInterval;
        }
    }

    public void spawnApple(){
        Vector3 spawnPoint = Vector3.zero;
        spawnPoint.x = Random.Range(-10, 10);
        spawnPoint.y = 3.8f;
        spawnPoint.z = Random.Range(-10,10);
        GameObject spawnedApple = Instantiate(apple, spawnPoint, Quaternion.identity);
        Destroy(spawnedApple, 5);
    }

    public void spawnDoubleJump(){
        Vector3 spawnPoint = Vector3.zero;
        spawnPoint.x = Random.Range(-10, 10);
        spawnPoint.y = 3.8f;
        spawnPoint.z = Random.Range(-10,10);
        GameObject spawnedDoubleJump = Instantiate(doubleJump, spawnPoint, Quaternion.identity);
        Destroy(spawnedDoubleJump, 5);
    }
}
