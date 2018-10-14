using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GoIPDynDnsClient
{
  public class Program
  {
    public static void Main(string[] args)
    {

      try
      {
        var c = new Config();
        c.ip = "1.1.1.";
        c.password = "testpw";
        c.subdomain = "testdom";
        c.username = "testuser";

        var xs = new XmlSerializer(typeof(Config));

        // Write
        if (!File.Exists("Config.xml"))
        {
          // we create a dummy for filling that out, which runs on the server
          using (var fs = System.IO.File.OpenWrite("Config.xml"))
          {
            xs.Serialize(fs, c);
          }
        }

        Config result;
        // Read
        using (var fs = System.IO.File.OpenRead("Config.xml"))
        {
          result = (Config)xs.Deserialize(fs);
        }

        if (result != null)
        {
          var wc = new WebCall();
          var log = wc.Update(result.username, result.password, result.subdomain, result.ip);
          Console.WriteLine(log);
        }
      }
      catch (Exception e)
      {

        Console.WriteLine(e);
      }
     
    }
  }
}
