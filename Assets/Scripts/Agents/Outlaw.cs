using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Agents
{
    public class Outlaw : Agent
    {
        public delegate void bankRobbery();
        public static event bankRobbery OnBankRobbery;

        private int _waitedTime = 0;
        private int _timeToWait;

        public Outlaw()
        {
            _stateMachine = new StateMachine<Outlaw>();
            _stateMachine.Init(this, new LurkInOutlawCamp(), new GlobalOutlawState());
        }

        private StateMachine<Outlaw> _stateMachine;

        private int _gold;
        public int Gold
        {
            get { return _gold; }
            set { _gold = value; }
        }

        private int _changeStateCountdown;
        public int ChangeStateCountdown
        {
            get { return _changeStateCountdown; }
            set { _changeStateCountdown = value; }
        }

        public void ChangeState(State<Outlaw> state)
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
            this._timeToWait=timeToWait;
            this._waitedTime = 0;
        }

        public override void Update()
        {
            this._waitedTime++;
            this._stateMachine.Update();
        }

    }
}
