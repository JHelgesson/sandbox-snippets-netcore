using System;
using System.Collections.Generic;

namespace SandboxSnippets
{
    public class ExceptionFilter
    {
        private List<Type> _exceptionsToCatch;

        public ExceptionFilter Catch<TException>() where TException : Exception
        {
            if(_exceptionsToCatch == null)
                _exceptionsToCatch = new List<Type>();

            _exceptionsToCatch.Add(typeof(TException));
            return this;
        }

        public void Execute(Action func)
        {
            try
            {
                func.Invoke();
            }
            catch (Exception e)
            {
                if(!_exceptionsToCatch.Contains(e.GetType()))
                    throw;
            }
        }

        public T Execute<T>(Func<T> func)
        {
            try
            {
                return func.Invoke();
            }
            catch (Exception e)
            {
                if (!_exceptionsToCatch.Contains(e.GetType()))
                    throw;
            }

            return default(T);
        }
    }
}
