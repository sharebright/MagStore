using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;

namespace MagStore.Web
{
    public class TagPropertyModelBinder : DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor.Name == "Tags")
            {
                var tags = controllerContext.HttpContext.Request.Form["Tags"];
                propertyDescriptor.SetValue(bindingContext.Model, tags.Split(',').Select(x => x.Trim()));
            }
            else
                base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }
    }
}