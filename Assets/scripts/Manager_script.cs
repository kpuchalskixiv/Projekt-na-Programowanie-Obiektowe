using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager_script : MonoBehaviour
{
    public GameObject next_level_UI;
    public GameObject game_over_UI;
    // public Text next_lvl_text;
    //private int i = 3;
    public void Next_Level()
    {
        next_level_UI.SetActive(true);
    }
    public void Game_Over()
    {
        game_over_UI.SetActive(true);
    }
    /*public void count()
    {
        if (i >= 0)
        {
            next_lvl_text.text = "Next level in: " + i.ToString();
            i--;
        }
    }*/
}
