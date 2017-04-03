using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Agents
{
    public class Sheriff : Agent
    {
        public delegate void outlawKilled();
        public static event outlawKilled OnOutlawKilled;

        private int _waitedTime = 0;
        private int _timeToWait;
        public Sheriff()
        {
            _stateMachine = new StateMachine<Sheriff>();
            _stateMachine.Init(this, new PatrolRandomLocation(), new GlobalSheriffState());
            Location = Location.SheriffsOffice;
        }

        private StateMachine<Sheriff> _stateMachine;

        private bool _outlawSpotted;
        public bool OutlawSpotted
        {
            get { return _outlawSpotted; }
            set { _outlawSpotted = value; }
        } 

        public void ChangeState(State<Sheriff> state)
        {
            this._stateMachine.ChangeState(state);
            this._waitedTime = 0;
        }

        public bool WaitedLongEnough()
        {
            return this._waitedTime >= _timeToWait;
        }

        public void SetTimeToWait(int timeToWait)
        {
            this._timeToWait = timeToWait;
            this._waitedTime = 0;
        }

        public override void Update()
        {
            this._waitedTime++;
            this._stateMachine.Update();
        }

        static Random rand = new Random();
        public Location PickRandomLocation()
        {
            Location nextLocation = Location;
            while (nextLocation == Location.OutlawCamp || nextLocation == Location)
                nextLocation = (Location)rand.Next(Enum.GetNames(typeof(Location)).Length);

            return nextLocation;
        }

    }
}
