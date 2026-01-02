using UnityEngine;

public class Trap : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Seeker"))
        {
            return;
        }

        collision.GetComponent<PlayerCtrl>().Slow();
    }
}
