
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class _collision : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 speed;
    private int food_eaten=0;
    private Vector2 stop=new Vector2(0f, 0f);
    public Text level_text, score_text, lifes_text;
    static public int level=1, lifes_left=3, score=0;

    static private int k = 1;
   // stop.x=0f; stop.y=0f;
    void Start()
    {
        level_text.text = "Current level: " + level.ToString();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        speed = rb.velocity;
        score_text.text = "Your score:\n" + score.ToString();//PlayerPrefs.getInt("Current_score").ToString();
        lifes_text.text = "Lifes left:\n" + lifes_left.ToString();
      //  lifes_left.text = "Current level: " + level.ToString();
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "food")
        {
            foood(col.collider.name);
            Destroy(col.gameObject);
            rb.velocity = speed;
        }
        if (col.collider.tag == "telep")
        {
            // Debug.Log("Teleport");
            teleport(col.collider.name);
        }
       // PlayerPrefs.SetInt("Current_score", score);

    }
    void foood(string myname)
    {
        
        //  Debug.Log(col.collider.name);
        if (myname[0] == 'B')
        {
            score += 50;
            GameObject[] t = GameObject.FindGameObjectsWithTag("ghost");
            foreach (GameObject g in t)
            {
               // Debug.Log(g.name);
                if (g.name == "Blinky")
                    g.GetComponent<Blinky_script>().change_to_frightened();
                else if (g.name == "Pinky")
                    g.GetComponent<Pinky_script>().change_to_frightened();
                else if (g.name == "Inky")
                    g.GetComponent<Inky_script>().change_to_frightened();
                else if (g.name == "Clyde")
                    g.GetComponent<Clyde_script>().change_to_frightened();
            }
        }
        else
        {
            score += 10;
            food_eaten++;
        }
        if (score >= k * 10000)
        {
            lifes_left++;
            k++;
        }
        if (food_eaten >= 240)
        {
            Debug.Log("Level passed");
            level++;
            GameObject.Find("Game manager").GetComponent<Manager_script>().Next_Level();
            // Invoke("next_level", 3f);
        }

    }

    void teleport(string myname)
    {
        Vector3 localPos = transform.localPosition;
        if (myname == "Telep1")
            localPos.x = 112.5f;
        else
            localPos.x = 87.5f;
        transform.localPosition = localPos;
    }

    
}
