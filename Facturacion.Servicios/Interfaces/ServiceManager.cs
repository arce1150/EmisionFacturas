using System; 
namespace Facturacion.Servicios.Interfaces
{
    public abstract partial class ServiceManager<T> where T : ServiceProvider
    {
        protected static bool s_Initialized;
        protected static Exception s_InitializeException;
        protected static object s_lock;
        private static T s_Provider;

        static ServiceManager()
        {
            s_lock = new object();
            s_Initialized = false;
            s_InitializeException = null;
        }

        private static void Initialize()
        {
            if (s_Initialized)
            {
                if (s_InitializeException != null)
                {
                    throw s_InitializeException;
                }
            }
            else
            {
                if (s_InitializeException != null)
                {
                    throw s_InitializeException;
                }
                lock (s_lock)
                {
                    if (s_Initialized)
                    {
                        if (s_InitializeException != null)
                        {
                            throw s_InitializeException;
                        }
                    }
                    else
                    {
                        try
                        {
                            s_Provider = (T)Activator.CreateInstance(Type.GetType(typeof(T).ToString().Replace("Interfaces", "Business").Replace("Svc", "Provider")));
                        }
                        catch (Exception exception)
                        {
                            s_InitializeException = exception;
                            throw;
                        }
                        s_Initialized = true;
                    }
                }
            }
        }

        public static T Provider
        {
            get
            {
                Initialize();
                return s_Provider;
            }
        }
    }
}
