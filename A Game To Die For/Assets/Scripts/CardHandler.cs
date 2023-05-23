using UnityEngine;

public class CardHandler : MonoBehaviour, IRaycastable
{
    [SerializeField] private RPS type;
    private Animator anim;

    private void Start() 
    {
        anim = GetComponent<Animator>();
    }

    public void HandleRaycast(PlayerRaycast player)
    {
        anim.SetBool("HoveringOver", true);


        if (Input.GetMouseButtonDown(0))
        {
            // Send to board 
            GameManager.instance.SendToBoard(gameObject);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Card Handling", GetComponent<Transform>().position);
        }
    }

    public void HandleNullRay(PlayerRaycast player)
    {
        anim.SetBool("HoveringOver", false);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Card Handling", GetComponent<Transform>().position);
    }

    public RPS getChoice()
    {
        return type;
    }
}

public enum RPS 
{ 
    ROCK, 
    PAPER, 
    SCISSORS,
    LIFE,
    MYSTERY
}