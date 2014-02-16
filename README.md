MicroProfiler
=============

[![Build status](https://ci.appveyor.com/api/projects/status?id=2fnmtwk3g75wc9dw)](https://ci.appveyor.com/project/microprofiler)

Framework agnostic clone of ASP.NET MVC MiniProfiler. If you like explicit profiling steps that you can run in production, but you're not using ASP.NET MVC, your options are limited. MicroProfiler gives you similar syntax in the form:

	[TestFixture]
	public class FullDemo
	{
		[Test]
		public void RunExampleCode()
		{
			// On application start
			MicroProfiler.Configure(new FakeStorage(), new DiagnosticsTraceListener());
			// end
	
			// On request start
			MicroProfiler.Current.Start();
			// end

			
			using (MicroProfiler.Current.Step("My outer loop"))
			{
				Thread.Sleep(5);
			
				using (MicroProfiler.Current.Step("My inner loop"))
				{
				    	Thread.Sleep(10);
				}
			
				using (MicroProfiler.Current.Step("some other thing"))
				{
			    		Thread.Sleep(5);
				}
			}
			
			// On request end	
			MicroProfiler.Current.Stop();
		}
	}


If the profiler isn't started, nothing happens, and once you stop the profiling session, all wired up instances of `IEmitDiagnostics` are executed.

Supporting extensibility, you can implement:

* IMicroProfilerStorage - to override the profiling session storage
* IEmitDiagnostics - to add additional diagnostic outputs

If you don't call configure, you'll default to `HttpProfilerPerRequestStorage` and `DiagnosticsTraceListener`.


Once you call `MicroProfiler.Current.Stop();` by default, the `DiagnosticsTraceListener` will produce output via Trace.WriteLine that looks like this:

	[Profiler] Session fab3bdf2-7834-4893-b513-8a0d814705da
	[Profiler] Clock          Time (inc. children)     Label
	[Profiler] +1ms           21.1355ms                My outer loop
	[Profiler] +7ms           10.7195ms                My inner loop
	[Profiler] +18ms          4.7646ms                 some other thing
	[Profiler] Total time: 23ms


## Why Do I Need It?

* You want to profile things and you'd like to push your output somewhere in a pluggable way.
* You prefer explicit rather than implicit profiling


## What This Isn't

* A MicroProfiler clone with a UI, at this point - The detatched nature of the implementation means providing a UI is left as an implementation of `IEmitDiagnostics`


## Containerless usage

You can:

* Use the static MicroProfiler.Current property 
* Create an instance of `MicroProfilerController`, handing it an appropriately "unit of work scoped" `IMicroProfilerStorage`. 

`IMicroProfilerStorage.Retrieve()` should return the same instance (or serialized instance) of `ProfiledOperations` for the duration of your unit of work. The default implementation uses HttpContext.Current.Items to achieve this, but in a Windows service or command line application, this will be insufficent.


## Configuring My Container

Bind `IMicroProfiler` to `MicroProfilerController` in transient scope.
Bind `IMicroProfilerStorage` to a suitable implementation.
`IMicroProfilerStorage` to `HttpProfilerPerRequestStorage` should be bound in request scope.


## Error Handling

None provided, all errors will throw.
