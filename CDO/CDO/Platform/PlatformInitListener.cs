
namespace CDO
{
    public interface PlatformInitListener
    {
         void onInitProgressChanged(InitProgressChangedEvent e);

         void onInitStateChanged(InitStateChangedEvent e);
    }

    
}
