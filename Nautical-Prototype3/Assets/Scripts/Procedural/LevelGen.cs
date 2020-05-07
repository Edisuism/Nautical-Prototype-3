    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//////////////////////////////////////////////////////Script generates a path to the end//////////////////////////////////////////////////
public class LevelGen : MonoBehaviour
{
    public Transform[] startingPositions;
    //Potential areas include: i0 = LR, i1 = LRB, i2 = LRT, i3 = Open
    public GameObject[] areas;
    public GameObject[] start;
    public GameObject player;
    public GameObject treasure;
    //Adjust according to tile size
    public float moveAmount;
    public float startTimeBetweenArea = 0.25f;
    //Adjust according to map size
    public float minX;
    public float maxX;
    public float minY;
    public bool stopGen = false;
    public LayerMask room;

    private int direction;
    private float timeBetweenArea;
    private int downCounter;


    void Start()
    {
        int randomStartingPoint = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randomStartingPoint].position;
        Instantiate(start[0], transform.position, Quaternion.identity);
        Instantiate(player, transform.position, Quaternion.identity);
        //could edit so instead of areas[0] it calls from a list of potential starting areas

        direction = Random.Range(1, 6);
    }

    void Update()
    {
        /////////////////////////////////////////////////Generation Conditions/////////////////////////////////////////////////
        if (timeBetweenArea <= 0 && !stopGen)
        {
            Move();
            timeBetweenArea = startTimeBetweenArea;
        }
        else
        {
            timeBetweenArea -= Time.deltaTime;
        }
    }

    private void Move()
    {
        ///////////////////////////////////////////////////////Move Right///////////////////////////////////////////////////////
        if (direction == 1 || direction == 2)
        {
            if (transform.position.x < maxX)
            {
                //Position did not go down
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                //Instantiate a tile from areas array
                int rand = Random.Range(0, areas.Length);
                Instantiate(areas[rand], transform.position, Quaternion.identity);

                //Prevent backtracking and overlapping
                direction = Random.Range(1, 6);
                if (direction == 3)
                {
                    direction = 1;
                }
                else if (direction == 4)
                {
                    direction = 5;
                }
            }
            else
            {
                direction = 5;
            }
        ///////////////////////////////////////////////////////Move Left///////////////////////////////////////////////////////    
        }
        else if (direction == 3 || direction == 4)
        {
            if (transform.position.x > minX)
            {
                //Position did not go down
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                //Instantiate a tile from areas array
                int rand = Random.Range(0, areas.Length);
                Instantiate(areas[rand], transform.position, Quaternion.identity);

                //Prevent backtracking and overlapping
                direction = Random.Range(3, 6);
            }
            else
            {
                direction = 5;
            }
        }
        ///////////////////////////////////////////////////////Move Down/////////////////////////////////////////////////////// 
        else if (direction == 5)
        {
            //Position went down
            downCounter++;

            if (transform.position.y > minY)
            {
                //Check previous area if it has a bottom opening, otherwise...
                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomDetection.GetComponent<AreaType>().type != 1 && roomDetection.GetComponent<AreaType>().type != 3)
                {
                    //if generator has gone down twice in a row, replace previous with open tile
                    if (downCounter >= 2)
                    {
                        roomDetection.GetComponent<AreaType>().areaDestroy();
                        Instantiate(areas[3], transform.position, Quaternion.identity);
                    }
                    //if generator has gone down once, replace previous tile with a bottom open tile
                    else
                    {
                        roomDetection.GetComponent<AreaType>().areaDestroy();
                        int randBottomRoom = Random.Range(1, 4);
                        if (randBottomRoom == 2)
                        {
                            randBottomRoom = 1;
                        }
                        Instantiate(areas[randBottomRoom], transform.position, Quaternion.identity);
                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                //Instantiate a tile from areas array
                int rand = Random.Range(2, 4);
                Instantiate(areas[rand], transform.position, Quaternion.identity);

                //Move a random direction
                direction = Random.Range(1, 6);
            }
            //If reached the bottom then stop generating
            else
            {
                //could add so it destroys the room it is currently on and replace it with a suitable end tile
                Instantiate(treasure, transform.position, Quaternion.identity);
                stopGen = true;
            }
        }
    }
}
