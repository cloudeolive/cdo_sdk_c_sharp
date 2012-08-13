
namespace CDO
{
    public class InitProgressChangedEvent
    {
        private int _progress;

        public int progress
        {
            get { return this._progress; }
        }


        public InitProgressChangedEvent(int progress)
        {
            this._progress = progress;
        }
    }
}
