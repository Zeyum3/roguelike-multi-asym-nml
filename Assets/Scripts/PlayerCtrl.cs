using UnityEngine;
using Unity.Netcode;
public class PlayerCtrl : NetworkBehaviour
{
    public float movSpeed;
    float speedX, speedY;
    Rigidbody2D rb;
       
    // Start is called once before the first execution of Update after the NetworkBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        Move();
        


    }
    private void Move()
        {
        speedX = Input.GetAxisRaw("Horizontal") * movSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movSpeed;
        rb.linearVelocity = new Vector2(speedX, speedY);
    }
}
