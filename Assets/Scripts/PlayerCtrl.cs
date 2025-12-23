using UnityEngine;
using Photon;
using Photon.Pun;
public class PlayerCtrl : MonoBehaviour
{
    public float movSpeed;
    float speedX, speedY;
    Rigidbody2D rb;
    public PhotonView view;
    public Camera playerCam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCam.enabled = view.IsMine;
    }

    // Update is called once per frame
    void Update()
    {
        if (view.IsMine)
        {
            Move();
        }
            
    }
    private void Move()
        {
        speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movSpeed;
        rb.linearVelocity = new Vector2(speedX, speedY);
    }
}
