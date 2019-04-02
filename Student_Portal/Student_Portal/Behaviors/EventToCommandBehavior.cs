using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Student_Portal.Behaviors
{
    public class EventToCommandBehavior : Behavior<ListView>
    {
        protected override void OnAttachedTo(ListView listView)
        {
            base.OnAttachedTo(listView);
            listView.ItemTapped += ListView_ItemTapped;
        }

        protected override void OnDetachingFrom(ListView listView)
        {
            base.OnDetachingFrom(listView);
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
