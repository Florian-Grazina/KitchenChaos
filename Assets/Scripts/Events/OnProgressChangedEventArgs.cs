namespace Assets.Scripts.Events
{
    public class OnProgressChangedEventArgs
    {
        public float progressNormalized;

        public OnProgressChangedEventArgs(float progressNormalized)
        {
            this.progressNormalized = progressNormalized;
        }
    }
}
