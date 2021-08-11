using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    public int width;
    public int height;

    public float size = 1f;
    public float keySize = 1f;
    public Transform keyPrefab = null;
    public Transform wallPrefab = null;
    // Start is called before the first frame update
    void Start()
    {
        var maze = MazeGenerator.Generate(width, height);
        Draw(maze);
    }
    private void Draw(WallState[,] maze)
    {
        for(int i=0; i < width; i++)
        {
            for(int j=0; j<height; j++){
                var cell = maze[i, j];
                var position = new Vector3(-width / 2 + i, -height / 2 + j, 0);
                if (cell.HasFlag(WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, size/2, 0);
                    topWall.localScale = new Vector3(size, topWall.localScale.y, topWall.localScale.z);
                }
                if (cell.HasFlag(WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size / 2, 0, 0);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.y, leftWall.localScale.z);
                    leftWall.eulerAngles = new Vector3(0, 0, 90);
                }
                if(i == width - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(size / 2, 0, 0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.y, rightWall.localScale.z);
                        rightWall.eulerAngles = new Vector3(0, 0, 90);

                    }
                }
                if(j == 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0, -size / 2, 0);
                        bottomWall.localScale = new Vector3(size, bottomWall.localScale.y, bottomWall.localScale.z);
                    }
                }
            }
        }

        // add random key

        int randX = -(Random.Range(0, height));
        int randY = -(Random.Range(0, (width/2) -2));
        var key = Instantiate(keyPrefab, transform) as Transform;
        key.position = new Vector3(randX, randY, 0);
        //key.localScale = new Vector3(keySize, key.localScale.y, key.localScale.z);
    }

    
}
