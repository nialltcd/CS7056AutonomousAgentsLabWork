using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts
{
    public class BoardManager : MonoBehaviour
    {
        [Serializable]
        public class Count
        {
            public int minimum;
            public int maximum;
            public Count(int min, int max)
            {
                minimum = min;
                maximum = max;
            }
        }

        public int columns = 10;
        public int rows = 10;
        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public Count wallCount = new Count(5,11);
        public Count mountainCount = new Count(2, 5);
        public GameObject[] mountainTiles;
        public GameObject[] locationTiles;
        public GameObject[] agents;
        private Transform boardHolder;
        private List<Vector3> gridPositions = new List<Vector3>();

        void InitialiseList()
        {
            gridPositions.Clear();
            for(int x=1;x<columns-1;x++)
            {
                for(int y=1;y<rows-1;y++)
                {
                    gridPositions.Add(new Vector3(x, y, 0f));
                }
            }
        }

        void BoardSetup()
        {
            boardHolder = new GameObject("Board").transform;
            for (int x = -1; x < columns+1; x++)
            {
                for (int y = -1; y < rows+1; y++)
                {
                    gridPositions.Add(new Vector3(x, y, 0f));
                    GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                    if (x == -1 || x == columns || y == -1 || y == rows)
                        toInstantiate = wallTiles[Random.Range(0, wallTiles.Length)];
                    GameObject instance = Instantiate(toInstantiate,new Vector3(x,y,0f),Quaternion.identity) as GameObject;
                    instance.transform.SetParent(boardHolder);
                }
           }
        }

        Vector3 RandomPosition()
        {
            int randomIndex = Random.Range(0, gridPositions.Count);
            Vector3 randomPosition = gridPositions[randomIndex];
            gridPositions.RemoveAt(randomIndex);
            return randomPosition;
        }

        void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum, bool chooseRandomSprite)
        {
            int objectCount = Random.Range(minimum, maximum + 1);
            for(int i=0;i<objectCount;i++)
            {
                Vector3 randomPosition = RandomPosition();
                GameObject tileChoice = new GameObject();
                if (chooseRandomSprite)
                    tileChoice = tileArray[Random.Range(0, tileArray.Length)];
                else
                    tileChoice = tileArray[i];
                Instantiate(tileChoice, randomPosition, Quaternion.identity);
            }
        }


        void LayoutLocationsAtRandom(GameObject[] locationArray)
        {
            for (int i = 0; i < locationArray.Length; i++)
            {
                Vector3 randomPosition = RandomPosition();
                GameObject tileChoice = locationArray[i];
                LocationProperties.LocationCoords[i] = randomPosition;
                Instantiate(tileChoice, randomPosition, Quaternion.identity);
                if(i<agents.Length)
                {
                    GameObject agentChoice = agents[i];
                    Instantiate(agentChoice, randomPosition, Quaternion.identity);
                }
            }
        }

        public void SetupScene()
        {
            BoardSetup();
            InitialiseList();
            LayoutObjectAtRandom(mountainTiles, mountainCount.minimum, mountainCount.maximum, true);
            LayoutLocationsAtRandom(locationTiles);
        }
    }
}
