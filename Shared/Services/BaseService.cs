namespace Shared.Services
{
public class BaseService
{
    public string LastError { get; private set; } = "";

    protected void ClearLastError() => LastError = "";
    protected void SetLastError(string error) => LastError = error;

    public virtual void Initialize()
    {
    }

    public virtual void Uninitialize()
    {
    }
}
}
