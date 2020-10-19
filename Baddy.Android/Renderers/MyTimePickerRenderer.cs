using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Baddy.Android.Renderers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

//[assembly: ExportRenderer(typeof(Xamarin.Forms.TimePicker), typeof(MyTimePickerRenderer))]
namespace Baddy.Android.Renderers
{
    public class MyTimePickerRenderer : TimePickerRenderer
    {
        public MyTimePickerRenderer(Context context) : base(context) 
        {
        }       

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged(e);

            TimePickerDialogIntervals timePickerDlg = new TimePickerDialogIntervals(Context, new EventHandler<TimePickerDialog.TimeSetEventArgs>(UpdateDuration),
                Element.Time.Hours, Element.Time.Minutes, true);

            var control = new EditText(Context)
            {
                Focusable = false,
                FocusableInTouchMode = false,
                Clickable = false
            };

            control.Click += (sender, ea) => timePickerDlg.Show();
            control.Text = Element.Time.Hours.ToString("00") + ":" + Element.Time.Minutes.ToString("00");

            SetNativeControl(control);
        }

        void UpdateDuration(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            Element.Time = new TimeSpan(e.HourOfDay, e.Minute / 30, 0);
            Control.Text = Element.Time.Hours.ToString("00") + ":" + Element.Time.Minutes.ToString("00");
        }
    }

    public class TimePickerDialogIntervals : TimePickerDialog
    {
        public const int TimePickerInterval = 30;

        public TimePickerDialogIntervals(Context context, EventHandler<TimeSetEventArgs> callBack, int hourOfDay, int minute, bool is24HourView)
            : base(context, (sender, e) => {
                callBack(sender, new TimeSetEventArgs(e.HourOfDay, e.Minute * TimePickerInterval));
            }, hourOfDay, minute / TimePickerInterval, is24HourView)
        {
        }

        protected TimePickerDialogIntervals(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void SetView(View view)
        {
            SetupMinutePicker(view);
            base.SetView(view);
        }

        void SetupMinutePicker(View view)
        {
            var numberPicker = FindMinuteNumberPicker(view as ViewGroup);
            if (numberPicker != null)
            {
                numberPicker.MinValue = 0;
                numberPicker.MaxValue = 3;
                numberPicker.SetDisplayedValues(new string[] { "00", "30", "60" });
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            GetButton((int)DialogButtonType.Negative).Visibility = ViewStates.Gone;
            SetCanceledOnTouchOutside(false);
        }

        private NumberPicker FindMinuteNumberPicker(ViewGroup viewGroup)
        {
            for (var i = 0; i < viewGroup.ChildCount; i++)
            {
                var child = viewGroup.GetChildAt(i);
                var numberPicker = child as NumberPicker;
                if (numberPicker != null)
                {
                    if (numberPicker.MaxValue == 59)
                        return numberPicker;
                }

                var childViewGroup = child as ViewGroup;
                if (childViewGroup != null)
                {
                    var childResult = FindMinuteNumberPicker(childViewGroup);
                    if (childResult != null)
                        return childResult;
                }
            }

            return null;
        }
    }
}
