using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Agents
{
    public class HoverInUndertakersOffice : State<Undertaker>
    {
        public override void Enter(Undertaker agent)
        {
            //Debug.Log("Oh I just arrived to my office Pilgrim");
        }

        public override void Execute(Undertaker agent)
        {
            //Debug.Log("Hovering in my office Pilgrim");
        }

        public override void Exit(Undertaker agent)
        {
            //Debug.Log("Leaving my office to find some dead guys Pilgrim");
        }
    }

    public class LookForDeadBodies : State<Undertaker>
    {
        public override void Enter(Undertaker agent)
        {
            Debug.Log("Gonna start looking for some dead guys Pilgrim");
        }

        public override void Execute(Undertaker agent)
        {
            Debug.Log("Looking for some dead guys Pilgrim");
        }

        public override void Exit(Undertaker agent)
        {
            Debug.Log("Found my dead guys Pilgrim");
        }
    }

    public class DragBodyToCemetary : State<Undertaker>
    {
        public override void Enter(Undertaker agent)
        {
        }

        public override void Execute(Undertaker agent)
        {
            Debug.Log("Bring this dead guy to the cemetary Pilgrim");
        }

        public override void Exit(Undertaker agent)
        {
            Debug.Log("Time to go home Pilgrim");
        }
    }

    public class GlobalUndertakerState : State<Undertaker>
    {
        public override void Enter(Undertaker agent)
        {
        }

        public override void Execute(Undertaker agent)
        {
        }

        public override void Exit(Undertaker agent)
        {
        }
    }
}
