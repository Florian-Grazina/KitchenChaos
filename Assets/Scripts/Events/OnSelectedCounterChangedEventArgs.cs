using System;

namespace Assets.Scripts.Events
{
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;

        public OnSelectedCounterChangedEventArgs(BaseCounter selectedCounter)
        {
            this.selectedCounter = selectedCounter;
        }
    }
}
