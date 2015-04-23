
using BusinessManagementSystem.HelperClasses;
namespace BusinessManagementSystem
{
    public interface IPageViewModel
    {
        string Name { get; }
        Enums.PageType PageType { get; }
    }
}
