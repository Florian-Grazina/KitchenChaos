
using Assets.Scripts.Events;
using System;

public interface IHasProgress
{
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
}
