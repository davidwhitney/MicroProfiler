using System.Web;
using MicroProfiler.Profiling;

namespace MicroProfiler.ProfilingDataStorage
{
    public class HttpProfilerPerRequestStorage : IMicroProfilerStorage
    {
        private readonly HttpContextBase _context;

        public HttpProfilerPerRequestStorage(HttpContextBase context)
        {
            _context = context;
        }

        public void Store(ProfiledOperations ops)
        {
            _context.Items.Add("profiler", ops);
        }

        public ProfiledOperations Retrieve()
        {
            if (_context == null)
            {
                return null;
            }

            if (!_context.Items.Contains("profiler"))
            {
                return null;
            }

            return _context.Items["profiler"] as ProfiledOperations;
        }
    }
}