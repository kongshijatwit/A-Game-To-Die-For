using UnityEngine;

public class CardHandler : MonoBehaviour, IRaycastable
{
    public void HandleRaycast(PlayerRaycast player)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(gameObject.name + " has been selected");
        }
    }
}
