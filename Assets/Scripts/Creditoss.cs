using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Creditoss : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        Invoke("WaitToEnd", 30);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu Inicial");
        }
    }

    public void WaitToEnd()

    {
        SceneManager.LoadScene("Menu Inicial");
    }
}

