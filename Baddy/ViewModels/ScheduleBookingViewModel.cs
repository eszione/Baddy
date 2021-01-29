using Baddy.Constants;
using Baddy.Enums;
using Baddy.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Baddy.ViewModels
{
    public class ScheduleBookingViewModel : BaseViewModel
    {
        public Command ScheduleCommand { get; set; }

        private DateTime dateNow;
        public DateTime DateNow
        {
            get => dateNow;
            set => SetProperty(ref dateNow, value);
        }

        private IEnumerable<Days> days;
        public IEnumerable<Days> Days
        {
            get => days;
            set => SetProperty(ref days, value);
        }

        private IEnumerable<int> courts;
        public IEnumerable<int> Courts
        {
            get => courts;
            set => SetProperty(ref courts, value);
        }

        private IEnumerable<int> durations;
        public IEnumerable<int> Durations
        {
            get => durations;
            set => SetProperty(ref durations, value);
        }

        private int selectedCourt;
        public int SelectedCourt 
        { 
            get => selectedCourt;
            set
            {
                SetProperty(ref selectedCourt, value);
                ScheduleCommand.ChangeCanExecute();
            }
        }

        private Days selectedDay;
        public Days SelectedDay
        {
            get => selectedDay;
            set => SetProperty(ref selectedDay, value);
        }

        private int selectedDuration;
        public int SelectedDuration 
        {
            get => selectedDuration;
            set
            {
                SetProperty(ref selectedDuration, value);
                ScheduleCommand.ChangeCanExecute();
            }
        }

        private bool isScheduled;
        public bool IsScheduled
        {
            get => isScheduled;
            set
            {
                SetProperty(ref isScheduled, value);
                if (!value)
                {
                    SelectedCourt = 0;
                    SelectedDuration = 0;
                    _ = Schedule();
                }
            }
        }

        public bool NavigateAway;

        public ScheduleBookingViewModel(
            IAppContext appContext,
            INavigationService navigationService,
            IStorageService storageService) : base(appContext, navigationService, storageService)
        {
            Title = "Schedule booking";

            DateNow = DateTime.Now;
            Courts = GetCourts();
            Days = GetDays();
            Durations = GetDurations();
            StartTimer();

            ScheduleCommand = new Command(async() => await Schedule(), () => CanSchedule);
            RefreshCommand = new Command(() => Refresh());
        }

        public void SetScheduledDefaults()
        {
            SelectedCourt = _storageService.ReadKey<int>(ScheduleConstants.Court);
            SelectedDay = _storageService.ReadKey<Days>(ScheduleConstants.Day);
            SelectedDuration = _storageService.ReadKey<int>(ScheduleConstants.Duration);
            IsScheduled = _storageService.ReadKey<bool>(ScheduleConstants.Toggle);
        }

        private bool CanSchedule => SelectedCourt > 0 && SelectedDuration > 0;

        private IEnumerable<int> GetCourts()
        {
            return new List<int>
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11
            };
        }
        
        private IEnumerable<Days> GetDays()
        {
            return new List<Days>
            {
                Enums.Days.Monday, 
                Enums.Days.Tuesday, 
                Enums.Days.Wednesday,
                Enums.Days.Thursday,
                Enums.Days.Friday,
                Enums.Days.Saturday,
                Enums.Days.Sunday
            };
        }

        private IEnumerable<int> GetDurations()
        {
            var durations = new List<int>();

            for (var x = 1; x <= 48; x++)
                durations.Add(x * 30);

            return durations;
        }

        private void StartTimer()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                DateNow = DateTime.Now;

                return !NavigateAway;
            });
        }

        private async Task Schedule()
        {
            if (IsScheduled)
            {
                await _storageService.SaveKey(ScheduleConstants.Court, SelectedCourt);
                await _storageService.SaveKey(ScheduleConstants.Day, SelectedDay);
                await _storageService.SaveKey(ScheduleConstants.Duration, SelectedDuration);
                await _storageService.SaveKey(ScheduleConstants.Toggle, IsScheduled);
            }
            else
            {
                await _storageService.DeleteKey(ScheduleConstants.Court);
                await _storageService.DeleteKey(ScheduleConstants.Day);
                await _storageService.DeleteKey(ScheduleConstants.Duration);
                await _storageService.DeleteKey(ScheduleConstants.Toggle);
            }
        }

        private void Refresh()
        {
            IsRefreshing = true;
            
            SetScheduledDefaults();

            IsRefreshing = false;
        }
    }
}