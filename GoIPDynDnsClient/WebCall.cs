using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GoIPDynDnsClient
{
  // We have to call https://www.goip.de/setip?username=<username>&password=<pass>&subdomain=<domain>&ip=<ipaddr> 
  public class WebCall
  {
    private string baseUrl = @"https://www.goip.de/setip?";
    private string ConstructUrl(string username, string password, string subdomain, string ip)
    {
      return baseUrl + string.Format("username={0}&password={1}&subdomain={2}&ip={3}", username, password, subdomain, ip);
    }

    public void Update(string username, string password, string subdomain, string ip)
    {
      var url = this.ConstructUrl(username, password, subdomain, ip);
      HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
      request.Method = "GET";
    }
  }
}
