public interface IRaycastable
{
    public string InteractionPrompt { get; }
    public void HandleRaycast(PlayerRaycast player);
}