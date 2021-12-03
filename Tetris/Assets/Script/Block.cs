using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour
{
    private float preTime = 0;
    public float fallTime = 0.8f;
    public static int height = 8;
    public static int width = 4;
    public Vector3 rotationCenterPoint;
    private static Transform[,] grid = new Transform[9, 17];
    public GameObject option;

    // Update is called once per frame
    void Update()
    {
        if(option == null)
        {
            this.GetComponent<Block>().option = GameObject.Find("Canvas/Op_window");
        }
        else if (option != null)
        {
            if (!option.activeSelf)
            {
                BlockSupport.S.Game_Stop = false;
            }
            else if (option.activeSelf)
            {
                BlockSupport.S.Game_Stop = true;
            }
        }

        if (!BlockSupport.S.Game_Stop)
        {
            if (Control_true())
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    this.transform.position += new Vector3(-1, 0, 0);
                    if (!ValidMove())
                    {
                        transform.position += new Vector3(1, 0, 0);
                    }
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    this.transform.position += new Vector3(1, 0, 0);
                    if (!ValidMove())
                    {
                        transform.position += new Vector3(-1, 0, 0);
                    }
                }
                if (Input.GetButtonDown("Jump"))
                {
                    transform.RotateAround(transform.TransformPoint(rotationCenterPoint), Vector3.back, 90);
                    if (!ValidMove())
                    {
                        transform.RotateAround(transform.TransformPoint(rotationCenterPoint), Vector3.back, -90);
                    }
                }
            }
            if (Time.time - preTime > ((Control_true()) ? (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime) : fallTime) ||
                Time.time - preTime > ((Control_true()) ? (Input.GetKey(KeyCode.UpArrow) ? 0 : fallTime) : fallTime))
            {
                this.transform.position += new Vector3(0, -1, 0);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(0, -1, 0);
                    AddGrid();
                    CheckForLines();
                    this.enabled = false;
                    FindObjectOfType<BlockSupport>().Block_support();
                }
                preTime = Time.time;
            }
        }
    }
    void CheckForLines()
    {
        for (int i = 16; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }
    bool HasLine(int i)
    {
        for (int j = 0; j < 9; j++)
        {
            if (grid[j, i] == null)
            {
                return false;
            }
        }
        return true;
    }
    void DeleteLine(int i)
    {
        for (int j = 0; j < 9; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
        Add_Score(100);
    }
    void RowDown(int i)
    {
        for (int y = i; y < 17; y++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }
    void AddGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedY + height > 16)
            {
                GameOver();
            }
            else
            {
                grid[roundedX + width, roundedY + height] = children.transform;
            }
        }
        Add_Score(30);
    }
    void GameOver()
    {
        if (!BlockSupport.S.Game_Stop)
        {
            BlockSupport.S.Game_Stop = true;
            UI_Manager.S.Show_GameOver();
        }
    }
    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < -width || roundedX > width || roundedY < -height)
            {
                return false;
            }
            if (roundedY + 8 < 17)
            {
                if (grid[roundedX + width, roundedY + height] != null)
                {
                    return false;
                }
            }
        }
        return true;
    }
    bool Control_true()
    {
        foreach (Transform children in transform)
        {
            int roundedY = Mathf.RoundToInt(children.transform.position.y);
            if (roundedY > height)
            {
                return false;
            }

        }
        return true;
    }
    void Add_Score(int score)
    {
        Score_Board.S.score_ += score;
    }
}
