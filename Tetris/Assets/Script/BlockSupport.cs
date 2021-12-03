using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockSupport : MonoBehaviour
{
    public static BlockSupport S;
    public bool Game_Stop = false;
    public GameObject[] Blocks;

    // Start is called before the first frame update
    void Start()
    {
        S = this;
        Block_support();   
    }

    // Update is called once per frame
    public void Block_support()
    {
        if (Game_Stop == false)
        {
            Instantiate<GameObject>(Blocks[Random.Range(0, Blocks.Length)], transform.position, Quaternion.identity);
        }
    }
}
