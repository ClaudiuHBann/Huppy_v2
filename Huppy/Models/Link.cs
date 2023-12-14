namespace Huppy.Models
{
public enum OS : byte
{
    NONE,
    WINDOWS,
    MAC
}

public enum Format : byte
{
    NONE,
    EXE,
    DMG
}

public enum Arch : byte
{
    NONE,
    x32,
    x64
}

class Link
{
    public int Id { get; set; } = 0;
    public int App { get; set; } = 0;
    public string Url { get; set; } = "";
    public string Name { get; set; } = "";
    public Format Format { get; set; } = Format.NONE;
    public OS OS { get; set; } = OS.NONE;
    public Arch Arch { get; set; } = Arch.NONE;
}
}
