using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGenerator : MonoBehaviour
{
    //public GameManager gameManager;
    public List<GameObject> bombsList;

    public GameObject bomsContainer;
    public GameObject blackBombPrefab;
    public GameObject redBombPrefab;
    private float cooldown = 1.5f;
    public float minimum = 1.5f;
    public float maximum = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown <= 0)
        {
            GenerateBomb();
            cooldown = Random.Range(minimum, maximum);
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }
    public void GenerateBomb()
    {
        int random = Random.Range(0, 2);
        GameObject bombPrefab;
        if (random == 0)
        {
            bombPrefab = blackBombPrefab;
        }
        else
        {
            bombPrefab = redBombPrefab;
        }   
        Instantiate(bombPrefab, transform.position , bomsContainer.transform.rotation, bomsContainer.transform);
    } 
    //Detectar si explotó una bomba

}
