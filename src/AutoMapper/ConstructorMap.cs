using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoMapper
{
    /// <summary>
    /// 
    /// </summary>
    public class ConstructorMap
    {
        private readonly LateBoundParamsCtor _runtimeCtor;
        public ConstructorInfo Ctor { get; private set; }
        public IEnumerable<ConstructorParameterMap> CtorParams { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructorMap"/> class.
        /// </summary>
        /// <param name="ctor">The ctor.</param>
        /// <param name="ctorParams">The ctor params.</param>
        public ConstructorMap(ConstructorInfo ctor, IEnumerable<ConstructorParameterMap> ctorParams)
        {
            Ctor = ctor;
            CtorParams = ctorParams;

            _runtimeCtor = DelegateFactory.CreateCtor(ctor, CtorParams);
        }

        /// <summary>
        /// Resolves the value.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public object ResolveValue(ResolutionContext context)
        {
            var ctorArgs = CtorParams
                        .Select(p => p.ResolveValue(context))
                        .Select(result => result.Value)
                        .ToArray();

            return _runtimeCtor(ctorArgs);
        }
    }
}