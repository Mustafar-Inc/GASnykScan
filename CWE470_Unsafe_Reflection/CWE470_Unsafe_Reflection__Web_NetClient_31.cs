/* TEMPLATE GENERATED TESTCASE FILE
Filename: CWE470_Unsafe_Reflection__Web_NetClient_31.cs
Label Definition File: CWE470_Unsafe_Reflection__Web.label.xml
Template File: sources-sink-31.tmpl.cs
*/
/*
 * @description
 * CWE: 470 Use of Externally-Controlled Input to Select Classes or Code ('Unsafe Reflection')
 * BadSource: NetClient Read data from a web server with WebClient
 * GoodSource: Set data to a hardcoded class name
 * Sinks:
 *    BadSink : Instantiate class named in data
 * Flow Variant: 31 Data flow: make a copy of data within the same method
 *
 * */

using TestCaseSupport;
using System;

using System.Web;

using System.IO;
using System.Net;

namespace testcases.CWE470_Unsafe_Reflection
{

class CWE470_Unsafe_Reflection__Web_NetClient_31 : AbstractTestCaseWeb
{
#if (!OMITBAD)
    /* uses badsource and badsink */
    public override void Bad(HttpRequest req, HttpResponse resp)
    {
        string dataCopy;
        {
            string data;
            data = ""; /* Initialize data */
            /* read input from WebClient */
            {
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        using (StreamReader sr = new StreamReader(client.OpenRead("http://www.example.org/")))
                        {
                            /* POTENTIAL FLAW: Read data from a web server with WebClient */
                            /* This will be reading the first "line" of the response body,
                            * which could be very long if there are no newlines in the HTML */
                            data = sr.ReadLine();
                        }
                    }
                }
                catch (IOException exceptIO)
                {
                    IO.Logger.Log(NLog.LogLevel.Warn, exceptIO, "Error with stream reading");
                }
            }
            dataCopy = data;
        }
        {
            string data = dataCopy;
            /* POTENTIAL FLAW: Instantiate object of class named in data (which may be from external input) */
            var container = Activator.CreateInstance(null, data);
            Object tempClassObj = container.Unwrap();
            IO.WriteLine(tempClassObj.GetType().ToString()); /* Use tempClassObj in some way */
        }
    }
#endif //omitbad
#if (!OMITGOOD)
    public override void Good(HttpRequest req, HttpResponse resp)
    {
        GoodG2B(req, resp);
    }

    /* goodG2B() - use goodsource and badsink */
    private void GoodG2B(HttpRequest req, HttpResponse resp)
    {
        string dataCopy;
        {
            string data;
            /* FIX: Use a hardcoded class name */
            data = "Testing.test";
            dataCopy = data;
        }
        {
            string data = dataCopy;
            /* POTENTIAL FLAW: Instantiate object of class named in data (which may be from external input) */
            var container = Activator.CreateInstance(null, data);
            Object tempClassObj = container.Unwrap();
            IO.WriteLine(tempClassObj.GetType().ToString()); /* Use tempClassObj in some way */
        }
    }
#endif //omitgood
}
}
