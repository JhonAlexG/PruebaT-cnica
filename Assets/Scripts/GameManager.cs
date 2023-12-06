using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public ScoreController scoreController;
    public GameObject container;
    public GameObject bombGenerator1;
    public GameObject bombGenerator2;
   
    public List<GameObject> bombsTotal;
    
    public GameObject gameOverCanvas;
    public TMP_Text scoreText;
    public TMP_Text newRecordText;
    public ColliderController colliderControllerRed;
    public ColliderController colliderControllerBlack;
    private bool gameOver = false;
    private int totalPoints = 0;


    void Start()
    {
        newRecordText.gameObject.SetActive(false);
        bombGenerator2.SetActive(false);
    }

    //revisar un componente en todas las bombas
 
    private void Update()
    {
        SeekBombs();
        totalPoints = colliderControllerRed.score + colliderControllerBlack.score;
        if (totalPoints >= 5 && gameOver == false)
        {
            bombGenerator2.SetActive(true);
        }
        if (totalPoints >= 15)
        {
            bombGenerator1.GetComponent<BombGenerator>().minimum = 0.5f;
            bombGenerator1.GetComponent<BombGenerator>().maximum = 2f;
            bombGenerator2.GetComponent<BombGenerator>().minimum = 0.5f;
            bombGenerator2.GetComponent<BombGenerator>().maximum = 2f;
        }
        BombExploded();
    }

    public void ClickRestart()
    {
        if (totalPoints > 0)
        {
            scoreController.AddScore(new Score(totalPoints));
        }
        Debug.Log("Puntos totales: " + totalPoints);
        SceneManager.LoadScene("GameMain");
    } 
 
    public void ClickBack()
    {
        if (totalPoints > 0)
        {
            scoreController.AddScore(new Score(totalPoints));
        }
        Debug.Log("Puntos totales: " + totalPoints);
        SceneManager.LoadScene("Menu");
    }

    public void SeekBombs()
    {
        bombsTotal.Clear();
        foreach (Transform child in container.transform)
        {
            bombsTotal.Add(child.gameObject);
        } 
    }
    
    private void BombExploded()
    {
        foreach (GameObject bomb in bombsTotal)
        {
            if (bomb.GetComponent<BombBehavior>().explode == true)
            {
                scoreText.text = "Score: " + totalPoints;
                gameOverCanvas.SetActive(true);
                if (scoreController.IsNewRecord(totalPoints))
                {
                    newRecordText.gameObject.SetActive(true);
                }
                bombGenerator1.SetActive(false);
                bombGenerator2.SetActive(false);
                if (gameOver == false)
                {
                    bomb.GetComponent<BombBehavior>().audioSource.PlayOneShot(bomb.GetComponent<BombBehavior>().explosionSound);
                    gameOver = true;
                }
                ExplodeUnusedBombs();
            }
        }
    }

    private void ExplodeUnusedBombs()
    {
        foreach (GameObject bomb in bombsTotal)
        {
            bomb.GetComponent<BombBehavior>().animator.SetBool("GameOver", true);
            bomb.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            if (bomb.GetComponent<BombBehavior>().isDraggable == true)
            {
                bomb.GetComponent<BombBehavior>().animator.SetBool("Lose", true);
                if (bomb.GetComponent<BombBehavior>().explode == false)
                {
                    bomb.GetComponent<BombBehavior>().audioSource.PlayOneShot(bomb.GetComponent<BombBehavior>().explosionSound);
                    bomb.GetComponent<BombBehavior>().explode = true;
                }
                Destroy(bomb, 1.5f);
            }
        }
    }

    private void SaveScore()
    {
        scoreController.AddScore(new Score(totalPoints));
        scoreController.SaveScores();
    }
}
