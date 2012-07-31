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
        public void resultHandler(T result);

        /**
         * 
         */ 
        public void errHandler(int errCode, string errMessage);

    }
}
