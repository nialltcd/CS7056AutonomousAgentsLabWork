using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Agents
{
    public class PatrolRandomLocation : State<Sheriff>
    {
        public override void Enter(Sheriff sheriff)
        {
        }

        public override void Execute(Sheriff sheriff)
        {
            Debug.Log("Patrolling the "+sheriff.Location.ToString());

            if (!sheriff.OutlawSpotted)
           {
                sheriff.ChangeState(new PatrolRandomLocation());
           }
        }

        public override void Exit(Sheriff sheriff)
        {
            sheriff.MoveAgent(sheriff.PickRandomLocation());
        }
    }

    public class BringMoneyToBank : State<Sheriff>
    {
        public override void Enter(Sheriff agent)
        {
        }

        public override void Execute(Sheriff agent)
        {
        }

        public override void Exit(Sheriff agent)
        {
        }
    }

    public class CelebrateInTheSaloon : State<Sheriff>
    {
        public override void Enter(Sheriff agent)
        {
        }

        public override void Execute(Sheriff agent)
        {
        }

        public override void Exit(Sheriff agent)
        {
        }
    }



    public class GlobalSheriffState : State<Sheriff>
    {
        public override void Enter(Sheriff agent)
        {
        }

        public override void Execute(Sheriff agent)
        {
        }

        public override void Exit(Sheriff agent)
        {
        }
    }
}
