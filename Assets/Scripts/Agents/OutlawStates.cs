using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Agents
{
    public class LurkInOutlawCamp : State<Outlaw>
    {

        public override void Enter(Outlaw outlaw)
        {
            System.Random randomGenerator = new System.Random();
            outlaw.SetTimeToWait(randomGenerator.Next(1, 10));
            //Debug.Log("I love lurking in my Outlaw Camp for "+ outlaw.ChangeStateCountdown+" cycles");
        }

        public override void Execute(Outlaw outlaw)
        {
            //Debug.Log("Outlaw camp is the best!");
            if(outlaw.WaitedLongEnough())
                outlaw.ChangeState(new LurkInCemetary());
        }

        public override void Exit(Outlaw outlaw)
        {//I'm going on an adventure
            //Debug.Log("I'm going on an adventure");
            outlaw.MoveAgent(Location.Cemetery);
        }
    }

    public class LurkInCemetary : State<Outlaw>
    {
        private System.Random randomGenerator = new System.Random();

        public override void Enter(Outlaw outlaw)
        {//Ooooooh a cemetary!
            System.Random randomGenerator = new System.Random();
            outlaw.SetTimeToWait(randomGenerator.Next(1, 10));
            //Debug.Log("Ooooooh a cemetary!");
        }

        public override void Execute(Outlaw outlaw)
        {//
            //Debug.Log("This cemetary is spooky");
            if(outlaw.WaitedLongEnough())
                outlaw.ChangeState(new LurkInOutlawCamp());
        }

        public override void Exit(Outlaw outlaw)
        {//Time to leave this cemetary
            //Debug.Log("Time to leave this cemetary");
            outlaw.MoveAgent(Location.OutlawCamp);
            while (outlaw.IsTravelling) { }
        }
    }

    public class RobBank : State<Outlaw>
    {
        private System.Random randomGenerator = new System.Random();
        public override void Enter(Outlaw outlaw)
        {
            Debug.Log("Time to rob me some money");
        }

        public override void Execute(Outlaw outlaw)
        {
            outlaw.Gold += randomGenerator.Next(1, 11);
        }

        public override void Exit(Outlaw outlaw)
        {//Making my getaway
            Debug.Log("Making my getaway");
        }
    }

    public class GlobalOutlawState : State<Outlaw>
    {
        public override void Enter(Outlaw outlaw)
        {
        }

        public override void Execute(Outlaw outlaw)
        {
        }

        public override void Exit(Outlaw outlaw)
        {
        }
    }

    public class OutlawKilled : State<Outlaw>
    {

        public override void Enter(Outlaw outlaw)
        {
        }

        public override void Execute(Outlaw outlaw)
        {
            outlaw.Location = Location.OutlawCamp;
        }

        public override void Exit(Outlaw outlaw)
        {
        }
    }

}
