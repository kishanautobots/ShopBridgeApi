using Ninject;

namespace Core.Helper
{
    /// <summary>
    /// Factory for all the data class.
    /// </summary>
    public class GetInstance
    {
        /// <summary>
        /// The kernel
        /// </summary>
        public static readonly IKernel Kernel;

        /// <summary>
        /// Initializes the <see cref="GetInstance"/> class.
        /// </summary>
        static GetInstance()
        {
            if (Kernel == null) Kernel = new StandardKernel(new DependencyResolver());
        }

        /// <summary>
        /// Generic method to get any type of db class from factory.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}