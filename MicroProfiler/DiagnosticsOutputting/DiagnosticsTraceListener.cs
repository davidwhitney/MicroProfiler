namespace MicroProfiler.DiagnosticsOutputting
{
    public class DiagnosticsTraceListener : IEmitDiagnostics
    {
        public void WriteLine(string message)
        {
            System.Diagnostics.Trace.WriteLine("[MicroProfiler] " + message);
        }
    }
}