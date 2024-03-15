using UnityEngine;

public class CombatContinueButton : MonoBehaviour {
    
    
    public void OnContinueButton()
    {
        CombatManager.Instance.HandleTurn();
    }
}
