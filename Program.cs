using Dotnet.Samples.QuartzNet;
using Quartz;
using Quartz.Impl;

var schedulerFactory = new StdSchedulerFactory();

var scheduler = await schedulerFactory.GetScheduler();
await scheduler.Start();

var job = JobBuilder.Create<GetUrlJob>()
    .WithIdentity("getUrlJob", "group1")
    .Build();

var trigger = TriggerBuilder.Create()
    .WithIdentity("getUrlTrigger", "group1")
    .StartNow()
    // https://crontab.guru/#0_9_*_*_*
    // .WithCronSchedule("0 9 * * *")
    .WithSimpleSchedule(x => x
        .WithIntervalInSeconds(10)
        .RepeatForever())
    .Build();

await scheduler.ScheduleJob(job, trigger);

Console.WriteLine("Press [Enter] to close the application.");
Console.ReadLine();
