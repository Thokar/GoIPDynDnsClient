using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace GoIPDynDnsClient
{
  public class IPResolve
  {
    public string ResolveIP()
    {
      HttpWebRequest request = WebRequest.Create("http://checkip.dyndns.org") as HttpWebRequest;
      request.Method = "GET";
      HttpWebResponse response = request.GetResponse() as HttpWebResponse;
      Stream reply = response.GetResponseStream();
      StreamReader readReply = new StreamReader(reply);
      var result =  readReply.ReadToEnd();

      string[] a = result.Split(':');
      string a2 = a[1].Substring(1);
      string[] a3 = a2.Split('<');
      string a4 = a3[0];

      return a4;

    }
  }
}
