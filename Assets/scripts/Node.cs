using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Node[] neighbours;
    public Vector2[] legit_movement;
    // Start is called before the first frame update
    void Start()
    {
        int k = neighbours.Length;
        legit_movement = new Vector2[k];
        Node neighb; Vector2 pom;
        for(int i=0; i<k; i++)
        {
            neighb = neighbours[i];
            pom = neighb.transform.position - transform.position;
            legit_movement[i] = pom.normalized;
        }
        if (this.name == "node (40)")
            legit_movement[0] = new Vector2(-1, 0);
        if (this.name == "node (41)")
            legit_movement[0] = new Vector2(1, 0);

    }


}
