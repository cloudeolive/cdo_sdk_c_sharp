
namespace CDO
{

    /**
     * 
     * 
     */ 
    class InitProgressChangedEvent
    {

        
        public InitProgressChangedEvent(int progress)
        {
            this._progress = progress;
        }

        private int _progress;

        public int progress
        {
            get { return this._progress; }
        }
    }
}
