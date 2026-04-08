using System;
using System.Collections.Generic;
using System.Text;

namespace CritiqlyNexusCore.Models
{
    public class DailyPageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NormalTemplate { get; set; }
        public DataTemplate SelectedTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var movie = item as Movie;

            if (movie.isSelectedDaily)
                return SelectedTemplate;

            return NormalTemplate;
        }
    }
}
