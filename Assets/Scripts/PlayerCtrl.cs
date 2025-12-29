using UnityEngine;
using Photon;
using Photon.Pun;
using System.Threading;
using UnityEngine.UI;
public class PlayerCtrl : MonoBehaviour
{
    public float movSpeed;
    float speedX, speedY;
    Rigidbody2D rb;
    public PhotonView view;
    public Camera playerCam;
    public float interactRadius;
    public LayerMask interactMask;
    public int amount;
    public float interactionDuration = 2f;
    float interactTimer;
    public Image fillTimer;
    public GameObject containerTimer, containerUI;
    DamageItem heldItem;
    public Role[] roles;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCam.enabled = view.IsMine;
        containerUI.SetActive(view.IsMine);
        rb.simulated = view.IsMine;
        interactTimer = interactionDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            Move();
            Interact();
        }
            
    }
    private void Move()
        {
        speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movSpeed;
        rb.linearVelocity = new Vector2(speedX, speedY);
    }
    private void Interact()
    {
        
        
        var result = Physics2D.OverlapCircleAll(transform.position, interactRadius, interactMask);
        foreach (var c in result)
        {
            if (!c.GetComponent<DamageItem>()) continue;
            var item = c.GetComponent<DamageItem>();
            if (item.canGrab)
            {
                Grab(item);
            }
            else
            {
                ChangeItemState(item);
            }
            
        }
        
            
        
    }
    private void Grab(DamageItem item)
    {
        if (Input.GetKeyDown(KeyCode.E) && gameObject.CompareTag("Seeker"))
        {
            if (heldItem != null)
            {
                if (heldItem.Take(view))
                {
                    heldItem = null;
                }
            }
            else
            {               
                if (item.Take(view))
                {
                    heldItem = item;
                }
            }
            
        }
        
    }
    private void ChangeItemState(DamageItem item)
    {
        if (Input.GetKey(KeyCode.E))
        {
            containerTimer.SetActive(true);
            interactTimer -= Time.deltaTime;
            fillTimer.fillAmount = interactTimer / interactionDuration;
            if (interactTimer > 0) return;
            interactTimer = interactionDuration;
            item.ModifyState(amount);
        }
        else 
        {
            containerTimer.SetActive(false);
            interactTimer = interactionDuration;
            fillTimer.fillAmount = interactTimer / interactionDuration;
        }
    }

    public void RoleChange(int roleIndex)
    {
        view.RPC("UpdateRole", RpcTarget.AllBuffered, roleIndex);
    }

    [PunRPC]
    void UpdateRole(int roleIndex)
    {
        spriteRenderer.sprite = roles[roleIndex].newSprite;
        amount = roles[roleIndex].amount;
        gameObject.tag = roles[roleIndex].roleTag;
    }
}

[System.Serializable]
public struct Role 
{
    public string roleTag;
    public Sprite newSprite;
    public int amount;
}