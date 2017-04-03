using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Agents
{
    public class Undertaker : Agent
    {
        public Undertaker()
        {
            _stateMachine = new StateMachine<Undertaker>();
            _stateMachine.Init(this, new HoverInUndertakersOffice(), new GlobalUndertakerState());
            Location = Location.Undertakers;
            Sheriff.OnOutlawKilled += LookForBodies;
        }

        public void LookForBodies()
        {
            _stateMachine.ChangeState(new LookForDeadBodies());
        }

        public void ChangeState(State<Undertaker> state)
        {
            this._stateMachine.ChangeState(state);
        }

        private StateMachine<Undertaker> _stateMachine;

        public override void Update()
        {
            this._stateMachine.Update();
        }
    }
}
