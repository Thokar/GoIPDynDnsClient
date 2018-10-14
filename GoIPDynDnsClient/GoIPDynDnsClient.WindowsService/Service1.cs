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

    private AutoResetEvent AutoEventInstance { get; set; }
    private StatusChecker StatusCheckerInstance { get; set; }
    private System.Timers.Timer ServiceTimer { get; set; }
    public int TimerInterval { get; set; }
    private bool TimerTaskSuccess;


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
        //
        // Create and start a timer.
        //
        ServiceTimer = new System.Timers.Timer();
        ServiceTimer.Interval = 60 * 60 * 1000;
        ServiceTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.m_mainTimer_Elapsed);
        ServiceTimer.AutoReset = false;  // makes it fire only once
        ServiceTimer.Enabled = true;
        ServiceTimer.Start(); // Start
        TimerTaskSuccess = false;
      }
      catch (Exception)
      {

        throw;
      }

    }

    private void m_mainTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        GoIPDynDnsClient.Program.MainLogic();
      }
      catch (Exception x)
      {
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
