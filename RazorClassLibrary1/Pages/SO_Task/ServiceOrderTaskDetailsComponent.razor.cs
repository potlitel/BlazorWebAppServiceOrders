using Microsoft.AspNetCore.Components;
using Radzen;
using RazorClassLibrary1.Dtos;
using System.Globalization;

//https://xakpc.info/lets-build-a-simple-countdown-timer-with-dotnet-blazor
//https://components.d20tek.com/span-timer
//https://github.com/d20Tek/blazor-components
//https://www.nuget.org/packages/D20Tek.BlazorComponents.Timer

namespace RazorClassLibrary1.Pages.SO_Task
{
    public partial class ServiceOrderTaskDetailsComponent
    {
        [Parameter]
        public ServiceOrderTaskDto ServiceOrderTask { get; set; } = new();

        [Parameter]
        public bool IsSideDialog { get; set; } = false;

        TimeSpan value;

        //string timeSpanFormat => (value < TimeSpan.Zero ? "'-'" : "") + "d'd 'h'h 'm'min 's's'";

        //protected override async Task OnInitializedAsync()
        //{
        //    //value = new(1, 12, 30, 0);
        //    //value = new(0, 0, 0, 15);

        //    //value = TimeSpan.FromTicks(ServiceOrderTask.ExecutionDate.Ticks);

        //    //var dateTime1 = new DateTime(1970, 1, 1, 0, 0, 0);
        //    //var dateTime2 = DateTime.Now;
        //    //TimeSpan span = dateTime1 - dateTime2;
        //    //Console.WriteLine(span.ToString());
        //    //Console.WriteLine(span.ToString(@"hh\:mm\:ss"));

        //    //value = DateTime.Now.TimeOfDay;
        //    //value = DateTimeToTimeSpan(ServiceOrderTask.ExecutionDate);
        //    //var result = span.ToString(@"hh\:mm\:ss");
        //    //value = span.LocalizedTimeFormat(new CultureInfo("en-US"));
        //}

        TimeSpan DateTimeToTimeSpan(DateTime? ts)
        {
            int days = CalculateDaysUntilVacation(ts, DateTime.Now);
            if (!ts.HasValue) return TimeSpan.Zero;
            else {
                return new TimeSpan(12, ts.Value.Hour, ts.Value.Minute, ts.Value.Second, ts.Value.Millisecond);
            } 
        }

        public int CalculateDaysUntilVacation(DateTime? summerVacationStart, DateTime currentDate)
        {
            if (!summerVacationStart.HasValue) return 0;
            else {
                TimeSpan daysUntilVacation = (TimeSpan)(summerVacationStart - currentDate);
                return daysUntilVacation.Days;
            }
            
        }
    }
}
