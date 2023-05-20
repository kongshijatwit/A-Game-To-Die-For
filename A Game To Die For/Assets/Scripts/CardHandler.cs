using UnityEngine;

public class CardHandler : MonoBehaviour, IRaycastable
{
    [SerializeField] private RPS type;
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
            GameManager.instance.SendToBoard(type);
        }
    }

    public void HandleNullRay(PlayerRaycast player)
    {
        anim.SetBool("HoveringOver", false);
    }
}

public enum RPS 
{ 
    ROCK, 
    PAPER, 
    SCISSORS 
}