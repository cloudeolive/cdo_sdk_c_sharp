using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDO
{

    /**
     * 
     */
    public interface Responder<T>
    {
        /**
         * 
         */
        void resultHandler(T result);

        /**
         * 
         */
        void errHandler(int errCode, string errMessage);

    }



    public class ResponderAdapter<T> : Responder<T>
    {

        private ResultHandler<T> _rHandler;
        private ErrHandler _errHandler;

        public ResponderAdapter(ResultHandler<T> rHandler=null, ErrHandler errHandler=null)
        {
            this._rHandler = rHandler;
            this._errHandler = errHandler;
        }

        public void resultHandler(T result)
        {
            if (this._rHandler != null)
            {
                this._rHandler(result);
            }
            
        }

        public void errHandler(int errCode, string errMessage)
        {
            if (this._errHandler != null)
            {
                this._errHandler(errCode, errMessage);
            }
        }

 
    }


}
