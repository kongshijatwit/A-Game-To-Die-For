using UnityEngine;

public class CardHandler : MonoBehaviour, IRaycastable
{
    [SerializeField] private Animator anim;
    [SerializeField] private float yPos;

    private void Start() 
    {
        anim = GetComponent<Animator>();
    }

    private void Update() 
    {
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }

    public void HandleRaycast(PlayerRaycast player)
    {
        anim.SetBool("HoveringOver", true);

        if (Input.GetMouseButtonDown(0))
        {
            // Send to board
            Debug.Log(gameObject.name + " has been selected");
        }
    }

    public void HandleNullRay(PlayerRaycast player)
    {
        anim.SetBool("HoveringOver", false);
    }
}
