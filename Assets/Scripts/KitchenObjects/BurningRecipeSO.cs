using UnityEngine;

[CreateAssetMenu()]    
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO inputKitchenObjectSO;
    public KitchenObjectSO outputKitchenObjectSO;
    public float burningTimerMax;
}
