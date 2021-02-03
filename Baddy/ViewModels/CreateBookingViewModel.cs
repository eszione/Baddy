using Baddy.Interfaces;
using Baddy.Models;
using Baddy.PopupModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Baddy.ViewModels
{
    public class CreateBookingViewModel : BaseViewModel
    {
        private readonly IBookingService _bookingService;

        public Command BookCommand { get; set; }

        private DateTime _dateNow;
        public DateTime DateNow
        {
            get => _dateNow;
            set => SetProperty(ref _dateNow, value);
        }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        private TimeSpan _selectedTime;
        public TimeSpan SelectedTime
        {
            get => _selectedTime;
            set => SetProperty(ref _selectedTime, value);
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

        public int SelectedCourt { get; set; }

        private int selectedDuration;
        public int SelectedDuration 
        {
            get => selectedDuration;
            set
            {
                selectedDuration = value;
                BookCommand.ChangeCanExecute();
            }
        }

        public bool NavigateAway;

        public CreateBookingViewModel(
            IAppContext appContext,
            INavigationService navigationService,
            IStorageService storageService,
            IBookingService bookingService) : base(appContext, navigationService, storageService)
        {
            _bookingService = bookingService;

            Title = "Create booking";

            DateNow = DateTime.Now;
            SelectedDate = DateNow;
            SelectedTime = DateNow.TimeOfDay;
            Courts = GetCourts();
            Durations = GetDurations();
            StartTimer();

            BookCommand = new Command(async () => await Book(), () => CanBook);
        }

        private bool CanBook => SelectedCourt > 0;

        private async Task Book()
        {
            EventHandler<bool> eventHandler = async (_, confirm) => await OnConfirmation(confirm);

            await _navigationService.ShowPopup<ConfirmationPopupModel>(false, "Are you sure you want to create this booking?", eventHandler);
        }

        private async Task OnConfirmation(bool confirm)
        {
            if (!confirm)
                return;

            try
            {
                var bookingConfirmed = await _bookingService.Create(new List<CreateBookingInfo>
                {
                    new CreateBookingInfo
                    {
                        Date = SelectedDate.Date + SelectedTime,
                        Court = SelectedCourt,
                        Duration = SelectedDuration
                    }
                });

                if (bookingConfirmed.Result == "1" && bookingConfirmed.Bookings.Find(b => b.Result.Value) != null)
                {
                    _ = _navigationService.ShowPopup<ToastPopupModel>(true, "Booking created");

                    await _navigationService.NavigateTo<BookingsViewModel>();
                }
            }
            catch
            {
                await _navigationService.DisplayError("Error occurred while creating the booking");
            }
        }

        private IEnumerable<int> GetCourts()
        {
            return new List<int>
            {
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11
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
    }
}