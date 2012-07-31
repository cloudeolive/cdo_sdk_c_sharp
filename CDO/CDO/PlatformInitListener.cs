
namespace CDO
{
    interface PlatformInitListener
    {
        public void onInitProgressChanged(InitProgressChangedEvent e);

        public void onInitProgronInitStateChanged(InitStateChangedEvent e);
    }
}
