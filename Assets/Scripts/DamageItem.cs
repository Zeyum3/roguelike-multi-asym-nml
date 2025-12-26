using UnityEngine;
using Photon.Pun;
public class DamageItem : MonoBehaviour
{
    public int currentDamageState;
    public PhotonView view;
    public Sprite[] spriteStates;
    public SpriteRenderer spriteRenderer;
    public int maxState = 3;

    [PunRPC]
    public void StateUpdate(int newDamageState)
    {
        currentDamageState = Mathf.Clamp(newDamageState, 0, maxState);
        spriteRenderer.sprite = spriteStates[currentDamageState];
        
    }

    public void ModifyState(int amount)
    {
        currentDamageState += amount;
        view.RPC("StateUpdate", RpcTarget.AllBuffered, currentDamageState);
    }

}
