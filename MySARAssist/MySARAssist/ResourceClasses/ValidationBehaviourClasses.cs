using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using Xamarin.Forms;

namespace MySARAssist.ResourceClasses
{
	public class BaseValidationBehaviour : Behavior<Entry>
    {
        public static readonly BindableProperty ValidStyleProperty = BindableProperty.Create("ValidStyle", typeof(Style), typeof(NonBlankValidationBehavior), (Style)Application.Current.Resources["entryStyle"]);
        public static readonly BindableProperty InvalidStyleProperty = BindableProperty.Create("InvalidStyle", typeof(Style), typeof(NonBlankValidationBehavior), (Style)Application.Current.Resources["invalidEntryStyle"]);

       
    public Style ValidStyle
    {
        get { return (Style)GetValue(ValidStyleProperty); }
        set { SetValue(ValidStyleProperty, value); }
    }
    public Style InvalidStyle
        {
            get { return (Style)GetValue(InvalidStyleProperty); }
            set { SetValue(InvalidStyleProperty, value); }
        }

    }

    public class EmailValidatorBehavior : BaseValidationBehaviour
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";


        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

      

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsValid = false;
            IsValid = (Regex.IsMatch(e.NewTextValue, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
            //((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
            ((Entry)sender).Style = IsValid ? ValidStyle : InvalidStyle;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }

    public class PasswordValidationBehavior : Behavior<Entry>
    {
        const string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$";


        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsValid = false;
            IsValid = (Regex.IsMatch(e.NewTextValue, passwordRegex));
            ((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }

    public class NonBlankValidationBehavior : BaseValidationBehaviour
    {
       

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleTextChanged;
            base.OnAttachedTo(bindable);
        }

        void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            bool IsValid = false;
            IsValid = !string.IsNullOrEmpty(e.NewTextValue);
            //((Entry)sender).TextColor = IsValid ? Color.Default : Color.Red;
            ((Entry)sender).Style = IsValid ? ValidStyle : InvalidStyle;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleTextChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}

