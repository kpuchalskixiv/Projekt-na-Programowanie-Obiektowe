using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
   /* public Rigidbody2D rb;
    public float speed = 4.8f;
    private int[] timers = { 7, 20, 7, 20, 5, 20, 5, 20 };
    private int iteration = 0;
    private float timer = 0f;
    public Node starting_position;
    private Node current_node, next_node, previous_node;
    public GameObject pacman;
    private Vector2 direction, next_direction;
    private enum Mode
    {
        Chase, Scatter, Frightened
    }
    Mode current_Mode = Mode.Scatter, previous_Mode;


    void Start()
    {
        //Node node = get_node_at_position(transform.position);
        current_node = starting_position;
        previous_node = current_node;
        next_node = chose_next();
        current_node = null;
        //previous_node = current_node;

    }
    void FixedUpdate()
    {
        rb.velocity = direction * speed;
        //Move();
        Node node = get_node_at_position(transform.position);
        if (node != null)
        {
            if (node != previous_node &&
                0.2 > distance_between_points(node.transform.position, transform.position))
            {
                current_node = node;

                next_node = chose_next();
                Debug.Log(next_node.name);

                previous_node = current_node;
                current_node = null;
                rb.velocity = Vector2.zero;
            }
        }
        //  Move();
    }

    void ModeUpdate()
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
        }
    }
    void change_mode(Mode m)
    {
        previous_Mode = current_Mode;
        current_Mode = m;
    }
    Node get_node_at_position(Vector2 pos)
    {
        GameObject tile = GameObject.Find("GAME").GetComponent<mygameboard>().
            board[Mathf.RoundToInt(pos.x - 86.5f), Mathf.RoundToInt(pos.y - 82.5f)];
        if (tile != null)
        {
            if (tile.GetComponent<Node>() != null)
                return tile.GetComponent<Node>();
        }
        return null;
    }
    /*void Move()
    {
        /* if(next_node!=current_node)
         {
             if (Overshot())
             {
                 Debug.Log("Overshot");
                 current_node = next_node;
                // transform.position = current_node.transform.position;
                 next_node = chose_next();
                 rb.velocity = direction * speed;

                 previous_node = current_node;
                 current_node = null;
             }
             else
        Debug.Log(direction);
                rb.velocity = direction * speed;
        //}
    }
    Node chose_next()
    {
        Vector2 target = Vector2.zero;
        Vector2 pacman_position = pacman.transform.position;
        target = new Vector2(pacman_position.x, pacman_position.y);
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
        float dist, least = 100f;
        for (int i = 0; i < count; i++)
        {
            dist = distance_between_points(target, found_nodes[i].transform.position);
            if (dist < least)
            {
                least = dist;
                target_node = found_nodes[i];
                direction = directions[i];
            }
        }
        return target_node;
    }
    /* float Length_from_previous_Node (Vector2 target_pos)
     {
         Vector2 vector = target_pos - (Vector2)previous_node.transform.position;
         return vector.sqrMagnitude;
     }
     bool Overshot()
     {
         float to_next= Length_from_previous_Node(next_node.transform.position);
         float to_position = Length_from_previous_Node(transform.position);
         return to_position>to_next;
     }
    float distance_between_points(Vector2 A, Vector2 B)
    {
        float x = B.x - A.x, y = B.y - A.y;
        return Mathf.Sqrt(x * x + y * y);
    }*/
}
