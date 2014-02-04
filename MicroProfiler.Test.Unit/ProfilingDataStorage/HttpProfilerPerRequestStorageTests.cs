using MicroProfiler.Profiling;
using MicroProfiler.ProfilingDataStorage;
using MicroProfiler.Test.Unit.Fakes;
using NUnit.Framework;

namespace MicroProfiler.Test.Unit.ProfilingDataStorage
{
    [TestFixture]
    public class HttpProfilerPerRequestStorageTests
    {
        private FakeContext _context;
        private HttpProfilerPerRequestStorage _storage;

        [SetUp]
        public void Setup()
        {
            _context = new FakeContext();
            _storage = new HttpProfilerPerRequestStorage(_context);
        }

        [Test]
        public void Store_StoresInHttpContextItems()
        {
            var op = new ProfiledOperations();

            _storage.Store(op);

            Assert.That(_context.Items["profiler"], Is.Not.Null);
            Assert.That(_context.Items["profiler"], Is.EqualTo(op));
        }

        [Test]
        public void Retrieve_ReturnsStoredItemFromContextItems()
        {
            var op = new ProfiledOperations();
            _context.RealItems.Add("profiler", op);

            var item = _storage.Retrieve();

            Assert.That(item, Is.Not.Null);
            Assert.That(item, Is.EqualTo(op));
        }

        [Test]
        public void Retrieve_ContextIsNull_ReturnsNull()
        {
            _storage = new HttpProfilerPerRequestStorage(null);

            var item = _storage.Retrieve();

            Assert.That(item, Is.Null);
        }

        [Test]
        public void Retrieve_ContextItemsDontHaveStoredItem_ReturnsNull()
        {
            _storage = new HttpProfilerPerRequestStorage(new FakeContext());

            var item = _storage.Retrieve();

            Assert.That(item, Is.Null);
        }
    }
}
