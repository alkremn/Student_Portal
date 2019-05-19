using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using Xamarin.Forms;

namespace Student_Portal.Behaviors
{
    class EmailValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += Entry_TextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= Entry_TextChanged;
            base.OnDetachingFrom(entry);
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string emailAddess = e.NewTextValue;
            bool isValid = false;
            if (emailAddess.Length != 0)
            {
                try
                {
                    MailAddress m = new MailAddress(emailAddess);
                    isValid = true;
                }
                catch (FormatException)
                {
                    isValid = false;
                }

                Entry entry = sender as Entry;
                entry.TextColor = isValid ? Color.Default : Color.Red;
            }
        }
    }
}
