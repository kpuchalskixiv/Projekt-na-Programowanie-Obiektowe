using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu_script : MonoBehaviour
{
    public void  Play_game()
    {
        _collision.score = 0;
        _collision.lifes_left = 3;
        _collision.level = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Quit_game()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
