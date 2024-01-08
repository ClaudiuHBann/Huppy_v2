using MessagePack;

namespace Shared.Entities
{
[MessagePackObject]
public class SettingsEntity
{
    [Key(0)]
    public bool ShowProposedApps { get; set; } = false;

    public SettingsEntity()
    {
    }
}
}
