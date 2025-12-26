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
    public GameObject containerTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCam.enabled = view.IsMine;
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
        if (Input.GetKey(KeyCode.E))
        {
            containerTimer.SetActive(true);
            interactTimer -= Time.deltaTime;
            fillTimer.fillAmount = interactTimer/interactionDuration;
            if (interactTimer > 0) return;
            interactTimer = interactionDuration;

            var result = Physics2D.OverlapCircleAll(transform.position, interactRadius, interactMask);
            foreach (var c in result) 
            {
                if (c.GetComponent<DamageItem>())
                {
                    c.GetComponent<DamageItem>().ModifyState(amount);
                }
            }
        }
        else
        {
            containerTimer.SetActive(false);
            interactTimer = interactionDuration;
            fillTimer.fillAmount = interactTimer / interactionDuration;
        }
    }
}
