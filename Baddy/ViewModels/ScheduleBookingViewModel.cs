using Baddy.Constants;
using Baddy.Enums;
using Baddy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Baddy.ViewModels
{
    public class ScheduleBookingViewModel : BaseViewModel
    {
        private DateTime dateNow;
        public DateTime DateNow
        {
            get => dateNow;
            set => SetProperty(ref dateNow, value);
        }

        private bool isScheduled;
        public bool IsScheduled
        {
            get => isScheduled;
            set
            {
                SetProperty(ref isScheduled, value);
                if (!value)             
                    ResetDefaultBookingSettings();
            }
        }

        private IEnumerable<Days> scheduleDays;
        public IEnumerable<Days> ScheduleDays
        {
            get => scheduleDays;
            set => SetProperty(ref scheduleDays, value);
        }

        private IEnumerable<int> durations;
        public IEnumerable<int> Durations
        {
            get => durations;
            set => SetProperty(ref durations, value);
        }

        private IEnumerable<int> courts;
        public IEnumerable<int> Courts
        {
            get => courts;
            set => SetProperty(ref courts, value);
        }

        private Days selectedScheduleDay;
        public Days SelectedScheduleDay
        {
            get => selectedScheduleDay;
            set => SetProperty(ref selectedScheduleDay, value);
        }

        private TimeSpan selectedScheduleTime;
        public TimeSpan SelectedScheduleTime
        {
            get => selectedScheduleTime;
            set => SetProperty(ref selectedScheduleTime, value);
        }

        private TimeSpan selectedBookingTime;
        public TimeSpan SelectedBookingTime
        {
            get => selectedBookingTime;
            set => SetProperty(ref selectedBookingTime, value);
        }

        private int selectedDuration;
        public int SelectedDuration 
        {
            get => selectedDuration;
            set => SetProperty(ref selectedDuration, value);
        }

        private int selectedCourt;
        public int SelectedCourt
        {
            get => selectedCourt;
            set => SetProperty(ref selectedCourt, value);
        }

        public Command ScheduleCommand { get; set; }

        public bool NavigateAway;

        public ScheduleBookingViewModel(
            IAppContext appContext,
            INavigationService navigationService,
            IStorageService storageService) : base(appContext, navigationService, storageService)
        {
            Title = "Schedule booking";

            DateNow = DateTime.Now;

            ScheduleDays = GetDays();
            Durations = GetDurations();
            Courts = GetCourts();
            StartTimer();

            ScheduleCommand = new Command(async() => await Schedule());
            RefreshCommand = new Command(() => Refresh());
        }

        public void SetScheduledDefaults()
        {
            IsScheduled = _storageService.ReadKey<bool>(ScheduleConstants.ScheduleToggleOnOff);
            SelectedScheduleDay = _storageService.ReadKey<Days>(ScheduleConstants.ScheduleDay);
            SelectedScheduleTime = _storageService.ReadKey<TimeSpan>(ScheduleConstants.ScheduleTime);
            SelectedBookingTime = _storageService.ReadKey<TimeSpan>(ScheduleConstants.BookingTime);
            SelectedDuration = GetDefaultInt(_storageService.ReadKey<int>(ScheduleConstants.BookingDuration), Durations.FirstOrDefault());
            SelectedCourt = GetDefaultInt(_storageService.ReadKey<int>(ScheduleConstants.Court), Courts.FirstOrDefault());
        }

        private int GetDefaultInt(int currentValue, int defaultValue)
        {
            return currentValue == 0 ? defaultValue : currentValue;
        }

        private IEnumerable<Days> GetDays()
        {
            return new List<Days>
            {
                Days.Monday, 
                Days.Tuesday, 
                Days.Wednesday,
                Days.Thursday,
                Days.Friday,
                Days.Saturday,
                Days.Sunday
            };
        }

        private IEnumerable<int> GetDurations()
        {
            var durations = new List<int>();

            for (var x = 1; x <= 48; x++)
                durations.Add(x * 30);

            return durations;
        }

        private IEnumerable<int> GetCourts() => new List<int>{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

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
                await _storageService.SaveKey(ScheduleConstants.ScheduleToggleOnOff, IsScheduled);
                await _storageService.SaveKey(ScheduleConstants.ScheduleDay, SelectedScheduleDay);
                await _storageService.SaveKey(ScheduleConstants.ScheduleTime, SelectedScheduleTime);
                await _storageService.SaveKey(ScheduleConstants.BookingTime, SelectedBookingTime);
                await _storageService.SaveKey(ScheduleConstants.BookingDuration, SelectedDuration);
                await _storageService.SaveKey(ScheduleConstants.Court, SelectedCourt);

                MessagingCenter.Send<object>(this, ScheduleConstants.StartScheduler);
            }
            else
            {
                await _storageService.DeleteKey(ScheduleConstants.ScheduleToggleOnOff);
                await _storageService.DeleteKey(ScheduleConstants.ScheduleDay);
                await _storageService.DeleteKey(ScheduleConstants.ScheduleTime);
                await _storageService.DeleteKey(ScheduleConstants.BookingTime);
                await _storageService.DeleteKey(ScheduleConstants.BookingDuration);
                await _storageService.DeleteKey(ScheduleConstants.Court);

                MessagingCenter.Send<object>(this, ScheduleConstants.StopScheduler);
            }
        }

        private void ResetDefaultBookingSettings()
        {
            SelectedBookingTime = default;
            SelectedDuration = Durations.FirstOrDefault();
            SelectedCourt = Courts.FirstOrDefault();
        }

        private void Refresh()
        {
            IsRefreshing = true;
            
            SetScheduledDefaults();

            IsRefreshing = false;
        }
    }
}