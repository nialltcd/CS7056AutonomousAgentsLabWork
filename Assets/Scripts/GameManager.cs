using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance = null;
        public BoardManager boardScript;
        private List<Agent> agents;
        public float turnDelay = 0.1f;							//Delay between each Player turn.

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if(instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
            boardScript = GetComponent<BoardManager>();
            agents = new List<Agent>();

            InitGame();
        }

        public void AddAgentToList(Agent script)
        {
            //Add Agent to List agents.
            agents.Add(script);
        }

        void InitGame()
        {
            agents.Clear();
            boardScript.SetupScene();
        }

        void Update()
        {
            //Start moving enemies.
            StartCoroutine(MoveAgents());
        }

        IEnumerator MoveAgents()
        {
            //Wait for turnDelay seconds, defaults to .1 (100 ms).
            yield return new WaitForSeconds(turnDelay);

            //If there are no enemies spawned (IE in first level):
            if (agents.Count == 0)
            {
                //Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
                yield return new WaitForSeconds(turnDelay);
            }

            //Loop through List of Enemy objects.
            for (int i = 0; i < agents.Count; i++)
            {
                //Call the MoveEnemy function of Enemy at index i in the enemies List.
                //agents[i].MoveAgent();

                //Wait for Enemy's moveTime before moving next Enemy, 
                yield return new WaitForSeconds(agents[i].moveTime);
            }
        }

    }
}
