namespace HorusStudio.Maui.MaterialDesignControls
{
    public interface ICustomAnimation
    {
        Task SetAnimation(View view);

        Task RestoreAnimation(View view);
    }
}