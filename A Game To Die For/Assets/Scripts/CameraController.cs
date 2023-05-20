using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Player fields and offset to set camera position
    [SerializeField] private Transform player;
    private readonly Vector3 offset = new(0, 1.3f, 0);

    // Mouse movement and sensitivity - Serialized Fields are for fine-tuning
    [SerializeField] float sensX = 5f;
    [SerializeField] float sensY = 5f;
    private bool mouseMoving;

    // Mouse rotation
    private float xRotation;
    private float yRotation;

    private void OnEnable() 
    {
        HideMouse();
    }

    private void Update()
    {
        // Get mouse inputs and adjust sensitivity
        float mouseX = Input.GetAxisRaw("Mouse X") * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY;

        // Calculate and clamp mouse positions. Make sure player can't look over -90 and 90 degrees
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // When the mouse is visible, no player controls
        if (mouseMoving)
        {
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            player.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }

    void LateUpdate() => transform.position = player.transform.position + offset;

    #region Helper Functions for Mouse States
    private void StopMouse()
    {
        mouseMoving = false;
    }

    private void HideMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mouseMoving = true;
    }

#if UNITY_EDITOR
    private void OnApplicationFocus(bool focus)
    {
        if (focus) HideMouse();
    }
#endif
    #endregion
}