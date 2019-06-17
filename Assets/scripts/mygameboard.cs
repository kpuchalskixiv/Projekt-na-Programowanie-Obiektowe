using System.Collections;
using System.Collections.Generic;
//using System.Serializable;
using UnityEngine;

public class mygameboard : MonoBehaviour
{
    private static int height =56;
    private static int width = 40;
    public GameObject[,] board = new GameObject[width+4, height+4];
   // public Dictionary<Pair(float float), GameObject>;
    // Start is called before the first frame update
    void Start()
    {
        object[] objects = GameObject.FindObjectsOfType(typeof(GameObject));
        
        Vector2 pos;
        foreach (GameObject g in objects)
        {
            pos = g.transform.position;
           // Debug.Log(g.name);
            if (g.name != "Pac-man" && g.name != "Canvas" && g.tag!="food" && g.tag!="text" && g.tag!="ghost")
            {
               // Debug.Log(g.name + "x: "+ (int)(pos.x - 86.5) + " y: "+(int)(pos.y - 82.5));
                board[Mathf.RoundToInt(pos.x - 82.5f), Mathf.RoundToInt(pos.y - 78.5f)] = g;
            }

        }
    }
}
