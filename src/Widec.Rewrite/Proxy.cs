using System;

namespace Widec.Rewrite
{
    public class Proxy<TRequest, TResponse>
    {
        private Func<TRequest, TResponse> _legacyFunc;
        private Func<TRequest, TResponse> _newFunc;

        public Proxy(Func<TRequest, TResponse> legacyFunc, Func<TRequest, TResponse> newFunc)
        {
            _newFunc = newFunc ?? throw new ArgumentNullException(nameof(newFunc));
            _legacyFunc = legacyFunc ?? throw new ArgumentNullException(nameof(legacyFunc));
        }

        public TResponse Execute(TRequest request)
        {
            var newResponse = _newFunc(request);
            var legacyResponse = _legacyFunc(request);

            if (newResponse.GetHashCode() == legacyResponse.GetHashCode() && newResponse.Equals(legacyResponse))
            {
                return newResponse;
            }
            return legacyResponse;
        }
    }
}
