using System;
using System.Collections;

namespace SmartConnectors.Components.Salesforce.Exceptions
{
    public interface IForceException
    {
        Exception GetBaseException();
        string ToString();
        IDictionary Data { get; }
        Exception InnerException { get; }
        string Message { get; }
        string StackTrace { get; }
    }
}