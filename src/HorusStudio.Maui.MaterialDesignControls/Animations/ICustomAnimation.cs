namespace HorusStudio.Maui.MaterialDesignControls
{
    public interface ICustomAnimation
    {
        Task SetAnimationAsync(View view);

        Task RestoreAnimationAsync(View view);
    }
}