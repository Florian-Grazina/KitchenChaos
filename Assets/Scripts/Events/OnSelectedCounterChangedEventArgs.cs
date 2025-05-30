using System;

namespace Assets.Scripts.Events
{
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;

        public OnSelectedCounterChangedEventArgs(ClearCounter selectedCounter)
        {
            this.selectedCounter = selectedCounter;
        }
    }
}
