using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class countdown : MonoBehaviour
{
    public Text tekst;
    private int i = 3;
    public void count_down()
    {
        if (i > 0)
        {
            tekst.text = "Next level in: " + i.ToString();
            i--;
        }
        else
            SceneManager.LoadScene(((_collision.level - 1) % 2) + 1);
    }
}
