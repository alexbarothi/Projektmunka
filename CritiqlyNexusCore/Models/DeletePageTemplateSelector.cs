using System;
using System.Collections.Generic;
using System.Text;

namespace CritiqlyNexusCore.Models
{
    public class DeletePageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate NormalTemplate { get; set; }
        public DataTemplate DeletedTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var movie = item as Movie;

            if (movie.IsDeleted)
                return DeletedTemplate;

            return NormalTemplate;
        }
    }
}
