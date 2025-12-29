using UnityEngine;

public class ChangeRole : MonoBehaviour
{
    public int newRole;
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerCtrl>().RoleChange(newRole);
    }
}
