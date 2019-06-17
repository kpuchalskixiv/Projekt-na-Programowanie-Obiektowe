using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky_script : ghostss
{
    

    protected override void Start()
    {
        basic_color = GetComponent<SpriteRenderer>().color;
        current_node = starting_position;
        previous_node = current_node;
        next_node = chose_next();
        current_node = null;     
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
