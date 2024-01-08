using MessagePack;

namespace Shared.Entities
{
[MessagePackObject(true)]
public class SettingsEntity
{
    public bool ShowProposedApps { get; set; } = false;

    public SettingsEntity()
    {
    }
}
}
