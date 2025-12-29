using UnityEngine;
using Photon.Pun;
using UnityEditor;
using Photon.Pun.Demo.Cockpit;
public class DamageItem : MonoBehaviour
{
    public int currentDamageState;
    public PhotonView view;
    public Sprite[] spriteStates;
    public SpriteRenderer spriteRenderer;
    public int maxState = 3;
    internal bool canGrab = false;
    internal bool isTaken = false;
    private int ownerID;
    

    [PunRPC]
    public void StateUpdate(int newDamageState)
    {
        currentDamageState = Mathf.Clamp(newDamageState, 0, maxState);
        spriteRenderer.sprite = spriteStates[currentDamageState];
        canGrab = currentDamageState == 0;
    }
    [PunRPC]
    public void AuthUpdate(int authID, bool newIsTaken)
    {
        ownerID = authID;
        isTaken = newIsTaken;

        if(ownerID == 0)
        {
            transform.parent = null;
        }
        else
        {
            var v = PhotonView.Find(ownerID);
            if (v == null) return;

            transform.parent= v.transform;
        }
    }

    public void ModifyState(int amount)
    {
        currentDamageState += amount;
        view.RPC("StateUpdate", RpcTarget.AllBuffered, currentDamageState);
    }

    public bool Take(PhotonView playerView)
    {
        if (isTaken && playerView.ViewID != ownerID) return false;

        if (isTaken)
        {
            transform.parent = null;
            view.RPC("AuthUpdate", RpcTarget.AllBuffered, 0, false);
        }
        else
        {
            transform.parent = playerView.transform;
            view.RPC("AuthUpdate", RpcTarget.AllBuffered, playerView.ViewID, true);
        }
        return true;
    }
}
