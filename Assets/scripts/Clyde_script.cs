using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clyde_script : ghostss

{
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
            if (current_Mode == Mode.Chase)
            {
                Vector2 pacman_position = pacman.transform.position;
                pom = new Vector2(pacman_position.x, pacman_position.y);
                if (distance_between_points(pom, transform.position) < 8)
                    pom = home.transform.position;
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
}
