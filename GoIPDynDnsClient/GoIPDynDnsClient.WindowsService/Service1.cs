using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Timers;

namespace GoIPDynDnsClient.WindowsService
{
  public partial class Service1 : ServiceBase
  {
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private System.Timers.Timer ServiceTimer { get; set; }
    public int TimerInterval { get; set; }

    public Service1()
    {
      InitializeComponent();
      TimerInterval = 60 * 60 * 1000; // The amount of time to delay before the invoking the callback method specified when the Timer was constructed, in milliseconds.
    }

    public void OnDebug()
    {
      OnStart(null);
    }

    protected override void OnStart(string[] args)
    {
      try
      {
        ServiceTimer = new System.Timers.Timer();
        ServiceTimer.Interval = 1 * 1000;
        ServiceTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.DoWork);
        ServiceTimer.AutoReset = false;  // makes it fire only once
        ServiceTimer.Enabled = true;
        ServiceTimer.Start(); // Start
      }
      catch (Exception)
      {
        throw;
      }
    }

    private void DoWork(object sender, ElapsedEventArgs e)
    {
      try
      {
        log.Info("Start logic");

        ServiceTimer.Interval = 60 * 60 * 1000;
        GoIPDynDnsClient.Program.MainLogic(log);
      }
      catch (Exception x)
      {
        log.Error("Exception" + e);
      }
      finally
      {

        if (null != ServiceTimer)
        {
          ServiceTimer.Start(); // re - enable the timer
        }
      }
    }

    protected override void OnStop()
    {
      ServiceTimer.Enabled = false;
      ServiceTimer.Dispose();
      ServiceTimer = null;
    }
  }
}
