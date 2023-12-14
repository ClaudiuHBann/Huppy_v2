using System.Linq;

using Huppy.Models;

namespace Huppy.ViewModels;

public class MainViewModel
(HuppyContext context) : ViewModelBase
{
    readonly HuppyContext _context = context;

    public string Greeting => _context.Apps.Count().ToString();
}
