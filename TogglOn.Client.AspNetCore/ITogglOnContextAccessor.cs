namespace TogglOn.Client.AspNetCore
{
    public interface ITogglOnContextAccessor
    {
        TogglOnContext TogglOnContext { get; set; }
    }
}
