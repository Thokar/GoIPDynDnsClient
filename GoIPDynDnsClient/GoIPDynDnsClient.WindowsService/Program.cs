using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace GoIPDynDnsClient.WindowsService
{
  static class Program
  {
    /// <summary>
    /// Der Haupteinstiegspunkt für die Anwendung.
    /// </summary>
    static void Main()
    {

#if DEBUG
      Service1 service = new Service1();
      service.OnDebug();
      System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else
      ServiceBase[] ServicesToRun;
      ServicesToRun = new ServiceBase[]
      {
                new Service1()
      };
      ServiceBase.Run(ServicesToRun);

#endif
    }
  }
}
