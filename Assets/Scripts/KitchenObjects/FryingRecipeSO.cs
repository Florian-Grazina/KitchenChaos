using UnityEngine;

[CreateAssetMenu()]    
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO inputKitchenObjectSO;
    public KitchenObjectSO outputKitchenObjectSO;
    public float fryingTimerMax;
}
