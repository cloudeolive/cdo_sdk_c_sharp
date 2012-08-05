
namespace CDO
{
    public interface PlatformInitListener
    {
         void onInitProgressChanged(InitProgressChangedEvent e);

         void onInitProgronInitStateChanged(InitStateChangedEvent e);
    }
}
