using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace GoIPDynDnsClient.WindowsService
{
  public partial class Service1 : ServiceBase
  {

    private AutoResetEvent AutoEventInstance { get; set; }
    private StatusChecker StatusCheckerInstance { get; set; }
    private Timer StateTimer { get; set; }
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
      AutoEventInstance = new AutoResetEvent(false);
      StatusCheckerInstance = new StatusChecker();

      // Create the delegate that invokes methods for the timer.
      TimerCallback timerDelegate =
          new TimerCallback(StatusCheckerInstance.CheckStatus);

      // Create a timer that signals the delegate to invoke 
      // 1.CheckStatus immediately, 
      // 2.Wait until the job is finished,
      // 3.then wait 5 minutes before executing again. 
      // 4.Repeat from point 2.
      Console.WriteLine("{0} Creating timer.\n",
          DateTime.Now.ToString("h:mm:ss.fff"));
      //Start Immediately but don't run again.
      StateTimer = new Timer(timerDelegate, AutoEventInstance, 0, Timeout.Infinite);
      while (StateTimer != null)
      {
        //Wait until the job is done
        AutoEventInstance.WaitOne();
        //Wait for 5 minutes before starting the job again.
        StateTimer.Change(TimerInterval, Timeout.Infinite);
      }
      //If the Job somehow takes longer than 5 minutes to complete then it wont matter because we will always wait another 5 minutes before running again.
    }

    protected override void OnStop()
    {
      StateTimer.Dispose();
    }


  }
}
