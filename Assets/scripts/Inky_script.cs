using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inky_script : ghostss

{
    public Blinky_script Blinky;
    protected override void Start()
    {
        basic_color = GetComponent<SpriteRenderer>().color;
        current_node = starting_position;
        previous_node = current_node;
        direction = Vector2.up;
    }

    protected override Vector2 get_target()
    {

        Vector2 pom = Vector2.zero;
        if (current_Mode != Mode.Frightened)
        {
            float[] t;
            if (current_Mode == Mode.Chase)
            {
                Vector2 pacman_position = pacman.transform.position;
                Vector2 blinky_position = Blinky.transform.position;
                t = vector(blinky_position, pacman_position);
                return new Vector2 (2*t[0]+blinky_position.x, 2*t[1]+blinky_position.y);
            }
            else
                pom = home.transform.position;
        }
        else
        {
            Vector2 pacman_position = pacman.transform.position;
            pom = new Vector2(pacman_position.x, pacman_position.y);
        }
        return pom;
    }
    float[] vector (Vector2 A, Vector2 B)
    {
        float[] tab = new float[3];
        tab[0] = B.x - A.x; tab[1] = B.y - A.y;
        return tab;
    }
}
