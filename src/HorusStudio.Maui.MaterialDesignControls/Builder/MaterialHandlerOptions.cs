namespace HorusStudio.Maui.MaterialDesignControls;

[System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
public sealed class MaterialHandlerOptions : Dictionary<Type, Type>
{
	public void AddHandler(Type viewType, Type handlerType)
	{
        if (viewType is null || handlerType is null)
        {
            throw new ArgumentException("One of the configured handlers has a null ViewType or HandlerType");
        }

        Add(viewType, handlerType);
	}
}