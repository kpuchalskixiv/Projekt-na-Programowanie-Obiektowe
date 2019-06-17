using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ghostss : MonoBehaviour
{
    public Rigidbody2D rb;// = GameObject.FindObjectOfType <Rigidbody2D>();
    public float release_time = 0f, normal_speed = 3.8f;
    protected int[] timers = { 7, 20, 7, 20, 5, 20, 5, 20 };
    protected int iteration = 0;
    protected float color_timer=0f, timer_forfrightened = 0f, timer = 0f, frightened_timer=10f, current_speed=3.8f, frightened_speed=2.0f;
    public Node starting_position, home;
    protected Node current_node, next_node, previous_node;
    public GameObject pacman;//=GameObject.Find("Pac-man");
    protected Vector2 direction, next_direction;
    public Text tekst, game_over_tekst;
    protected static int k = 1;
    protected Color basic_color, current_color; 
    
    protected enum Mode
    {
        Chase, Scatter, Frightened
    }
    protected Mode current_Mode = Mode.Scatter, previous_Mode;

   // current_speed=normal_speed; frightened_speed=normal_speed;

    protected float pom_release_time=0f;
    protected virtual void Start() { }
    protected void FixedUpdate() {
        if (caught())
        {
            if (current_Mode != Mode.Frightened)
            {
                Debug.Log(this.name + " caught Pac-man");
                _collision.lifes_left--;

                if(_collision.lifes_left<=0)
                {
                    Debug.Log("Game Over");
                    game_over_tekst.text = "Game Over!\n" + this.name + "\ncaught you!\n"
                        + "Your score is: " + _collision.score.ToString();
                  //  GameObject.Find("your score").text = "Your score is: " + _collision.score.ToString();
                    GameObject.Find("Game manager").GetComponent<Manager_script>().Game_Over();
                    //  SceneManager.LoadScene("Menu");
                }
                reset();
            }
            else
            {
                _collision.score+=k*200;
                k *=2;
                transform.position = starting_position.transform.position;
                rb.velocity = Vector2.zero;
                direction= Vector2.zero;
                pom_release_time = release_time - (frightened_timer - timer_forfrightened);
                current_node = starting_position;
            }
        }
        if (release_time > pom_release_time)
        {
            pom_release_time += Time.deltaTime;
            timer_forfrightened += Time.deltaTime;
        }
        else
        {
            rb.velocity = direction * current_speed;
            //Move();
            Node node = get_node_at_position(transform.position);
            if (node != null)
            {
                if (node != previous_node &&
                    0.2f > distance_between_points(node.transform.position, transform.position))
                {
                    current_node = node;

                    next_node = chose_next();

                    previous_node = current_node;
                    current_node = null;
                    rb.velocity = Vector2.zero;
                }
            }
            ModeUpdate();
        }
    }
    protected virtual Vector2 get_target() { return Vector2.zero; }

    protected bool caught()
    {
        if (distance_between_points(pacman.transform.position, transform.position) < 0.2f)
            return true;
        return false;
    }
    protected void ModeUpdate()
    {
        if (current_Mode != Mode.Frightened)
        {
            if (iteration <= 7)
            {
                timer += Time.deltaTime;
                if (timer > timers[iteration])
                {
                    if (iteration % 2 == 0)
                    {
                        change_mode(Mode.Chase);
                        iteration++;
                    }
                    else
                    {
                        change_mode(Mode.Scatter);
                        iteration++;
                    }
                    timer = 0;
                }
            }
            else
                change_mode(Mode.Chase);
        }
        else
        {

            if (timer_forfrightened > frightened_timer)
            {
                change_mode(previous_Mode);
                timer_forfrightened = 0f;
                color_timer = 0f;
            }
            else
            {
                timer_forfrightened += Time.deltaTime;
                if(timer_forfrightened>=7f+color_timer)
                {
                    current_color= GetComponent<SpriteRenderer>().color;
                    if(current_color==Color.blue)
                    {
                        GetComponent<SpriteRenderer>().color=Color.white;
                        current_color = Color.white;
                    }
                    else
                    {
                        GetComponent<SpriteRenderer>().color = Color.blue;
                        current_color = Color.blue;
                    }
                    color_timer += 0.25f;
                }
            }
        }
    }
    protected void change_mode(Mode m)
    {
        Debug.Log("Mode changed to " + m);
        if (current_Mode != Mode.Frightened || m!=Mode.Frightened)
        {
            if (m == Mode.Frightened)
            {
                current_speed = frightened_speed;
                GetComponent<SpriteRenderer>().color = Color.blue;
            }
            else
            {
                current_speed = normal_speed;
                GetComponent<SpriteRenderer>().color = basic_color;
            }
            previous_Mode = current_Mode;
            current_Mode = m;
            k = 1;
        }
        if (current_Mode == Mode.Frightened)
        {
            timer_forfrightened = 0f;
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
    }
    public void change_to_frightened()
    {
        change_mode(Mode.Frightened);
        Debug.Log("frightened");
    }
    protected Node get_node_at_position(Vector2 pos)
    {
        GameObject tile = GameObject.Find("GAME").GetComponent<mygameboard>().
            board[Mathf.RoundToInt(pos.x - 82.5f), Mathf.RoundToInt(pos.y - 78.5f)];
        if (tile != null)
        {
            if (tile.GetComponent<Node>() != null)
                return tile.GetComponent<Node>();
        }
        return null;
    }
    protected Node chose_next()
    {
        if(current_node.name=="node (40)" && direction.x < 0)
        {
            direction= current_node.legit_movement[0];
            Vector2 p = this.transform.position;
            this.transform.position = new Vector2(p.x + 25.5f, p.y);
            return current_node.neighbours[0];
        }
        if (current_node.name == "node (41)" && direction.x > 0)
        {
            direction = current_node.legit_movement[0];
            Vector2 p = this.transform.position;
            this.transform.position = new Vector2(p.x - 25.5f, p.y);
            return current_node.neighbours[0];
        }
        Vector2 target = Vector2.zero;
        //Vector2 pacman_position = pacman.transform.position;
        // target = new Vector2(pacman_position.x, pacman_position.y);
        target = get_target();
        Node target_node = null;
        Node[] found_nodes = new Node[4];
        Vector2[] directions = new Vector2[4];
        int count = 0;
        for (int i = 0; i < current_node.neighbours.Length; i++)
        {
            if (current_node.legit_movement[i] != -1 * direction)
            {
                found_nodes[count] = current_node.neighbours[i];
                directions[count] = current_node.legit_movement[i];
                count++;
            }
        }
        if (count == 1)
        {
            direction = directions[0];
            return found_nodes[0];
        }
        float dist, least = 100f, most = 0f ;
        for (int i = 0; i < count; i++)
        {
            dist = distance_between_points(target, found_nodes[i].transform.position);
            if (current_Mode != Mode.Frightened)
            {
                if (dist < least)
                {
                    least = dist;
                    target_node = found_nodes[i];
                    direction = directions[i];
                }
            }
            else
            {
                if (dist > most)
                {
                    most = dist;
                    target_node = found_nodes[i];
                    direction = directions[i];
                }
            }
        }
        return target_node;
    }
    protected float distance_between_points(Vector2 A, Vector2 B)
    {
        float x = B.x - A.x, y = B.y - A.y;
        return Mathf.Sqrt(x * x + y * y);
    }

    private void reset()
    {
        GameObject[] t = GameObject.FindGameObjectsWithTag("ghost");
        foreach (GameObject g in t)
        {
            if (g.name == "Blinky")
            {
                g.GetComponent<Blinky_script>().direction = Vector2.zero;
                g.GetComponent<Blinky_script>().rb.velocity = Vector2.zero;
                g.transform.position = g.GetComponent<Blinky_script>().starting_position.transform.position;
                g.GetComponent<Blinky_script>().pom_release_time = 0f;
                g.GetComponent<Blinky_script>().iteration = 0;
                g.GetComponent<Blinky_script>().timer = 0f;
                g.GetComponent<Blinky_script>().Start();
            }
            else if (g.name == "Pinky")
            {
                g.GetComponent<Pinky_script>().rb.velocity = Vector2.zero;
                g.transform.position = g.GetComponent<Pinky_script>().starting_position.transform.position;
                g.GetComponent<Pinky_script>().pom_release_time = 0f;
                g.GetComponent<Pinky_script>().Start();
                g.GetComponent<Pinky_script>().iteration = 0;
                g.GetComponent<Pinky_script>().timer = 0f;
            }
            else if (g.name == "Inky")
            {
                g.GetComponent<Inky_script>().rb.velocity = Vector2.zero;
                g.transform.position = g.GetComponent<Inky_script>().starting_position.transform.position;
                g.GetComponent<Inky_script>().pom_release_time = 0f;
                g.GetComponent<Inky_script>().Start();
                g.GetComponent<Inky_script>().iteration = 0;
                g.GetComponent<Inky_script>().timer = 0f;
            }
            else if (g.name == "Clyde")
            {
                g.GetComponent<Clyde_script>().rb.velocity = Vector2.zero;
                g.transform.position = g.GetComponent<Clyde_script>().starting_position.transform.position;
                g.GetComponent<Clyde_script>().pom_release_time = 0f;
                g.GetComponent<Clyde_script>().Start();
                g.GetComponent<Clyde_script>().iteration = 0;
                g.GetComponent<Clyde_script>().timer = 0f;
            }
        }
        pacman.transform.position = new Vector2(100f, 89.5f);
        pacman.GetComponent<player_movement>().pom = Vector2.zero;
        Invoke("do_nothing", 3f);
    }
    void do_nothing() { }
}
