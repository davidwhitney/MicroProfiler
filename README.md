MicroProfiler
=============

* Intro
* Why Do I Need It?
* What This Isn't
* Configuring My Container
* Error Handling

Framework agnostic clone of ASP.net MVC MiniProfiler. If you like explicit profiling steps that you can run in production, but you're not using ASP.NET MVC, your options are limited. MicroProfiler gives you similar syntax in the form:

	[TestFixture]
    	public class FullDemo
    	{
        	[Test]
	        public void RunExampleCode()
        	{
	            // Bootstrapping code
	            MicroProfiler.Configure(new FakeStorage(), new DiagnosticsTraceListener());
	            // end
	
	            MicroProfiler.Current.Start();


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

	
	            MicroProfiler.Current.Stop();
	        }
	    }

The idea is that you bind the type:

    IMicroProfilerStorage
  
in your IoC container per request scope (or whatever your unit of work is), to either the provided `HttpProfilerPerRequestStorage` class, that stores profiling data in HttpContext.Items, or you can provide your own storage implementation and wire it up using:

    MicroProfiler.Configure(new HttpProfilerPerRequestStorage(), new DiagnosticsTraceListener());

Then on Unit of work start / request start you should call `MicroProfiler.Current.Start();`

To profile, you then call :

	using (MicroProfiler.Current.Step("My outer loop"))
	{
		// Do stuff here
	}

and on the end of your request or unit of work call `MicroProfiler.Current.Stop();` And debug diagnostics will be output. You can implement your own `IEmitDiagnostics` implementations to push your traces to anywhere.

We also support extensibility, you can implement:

* IMicroProfilerStorage - to override the profiling session storage
* IEmitDiagnostics - to add additional diagnostic outputs

If you don't call configure, you'll default to `HttpProfilerPerRequestStorage` and `DiagnosticsTraceListener`.


Why Do I Need It?
=================

* You want to profile things and you'd like to push your output somewhere in a pluggable way.
* You prefer explicit rather than implicit profiling


What This Isn't
===============

* A MicroProfiler clone with a UI, at this point - The detatched nature of the implementation means providing a UI is left as an implementation of `IEmitDiagnostics`


Configuring My Container
========================

Bind `IMicroProfiler` to `MicroProfilerController` in transient scope.
Bind `IMicroProfilerStorage` to a suitable implementation.
`IMicroProfilerStorage` to `HttpProfilerPerRequestStorage` should be bound in request scope.


Error Handling
==============

None provided, all errors will throw.