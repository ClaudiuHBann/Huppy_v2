using Shared.Entities;
using Shared.Services;

namespace Huppy.Services
{
public class SettingsService : BaseService
{
    private const string _storageKey = "Settings.json";
    private readonly StorageService _storage;

    public SettingsEntity Settings { get; set; }

    public SettingsService(StorageService storage)
    {
        _storage = storage;

        Settings = _storage.Load<SettingsEntity>(_storageKey) ?? new();
    }

    public override void Uninitialize()
    {
        _storage.Save(_storageKey, Settings);
    }
}
}
