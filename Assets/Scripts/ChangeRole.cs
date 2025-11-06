using UnityEngine;

public class ChangeRole : MonoBehaviour
{
    [SerializeField] Sprite[] playerSprites;
    private Sprite newSprite;
    
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.tag == "ChangeToPrincess")
        {
            Debug.Log("Princess");
            newSprite = playerSprites[1];
            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }

        if (collision.gameObject.tag == "ChangeToSeeker")
        {
            Debug.Log("Seeker");
            newSprite = playerSprites[0];
            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
    }
}
