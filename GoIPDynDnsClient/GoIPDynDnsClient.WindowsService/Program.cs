using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace GoIPDynDnsClient.WindowsService
{
  static class Program
  {

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    /// <summary>
    /// Der Haupteinstiegspunkt für die Anwendung.
    /// </summary>
    static void Main()
    {

      string path = System.Reflection.Assembly.GetExecutingAssembly().Location;
      path = System.IO.Path.GetDirectoryName(path);
      //Directory.SetCurrentDirectory(path);
      //Environment.CurrentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
      Environment.CurrentDirectory = path;

#if DEBUG

      log.Info("Starting debug mode");
      Service1 service = new Service1();
      service.OnDebug();
      System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else
      log.Info("Starting release mode");
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
