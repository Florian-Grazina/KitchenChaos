using static StoveCounter;

namespace Assets.Scripts.Events
{
    public class OnStateChangedEventArgs
    {
        public StateEnum state;

        public OnStateChangedEventArgs(StateEnum state)
        {
            this.state = state;
        }
    }
}
