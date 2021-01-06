using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 2f;
    bool isFocused = false;
    Transform player;
    bool isInteracted = false;
    public Transform interactableTransform;
    

    public virtual void Interact()
    {
        UnityEngine.Debug.Log("Interacting with " + interactableTransform.name);
    }

    void Update()
    {
        if (isFocused && !isInteracted)
        {
            float distance = Vector3.Distance(player.position, interactableTransform.position);
            if (distance <= radius)
            {
                Interact();
                isInteracted = true;
            }
            
            
        }
    }

    private void OnDrawGizmosSelected()
    {

        if (interactableTransform == null)
            interactableTransform = transform;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactableTransform.position, radius);
    }

  public void OnFocused(Transform playerTransform)
    {
        isFocused = true;
        player = playerTransform;
        isInteracted = false;
    }
    public void OnDefocused()
    {
        isFocused = false;
        player = null;
        isInteracted = false;
    }

    

}
