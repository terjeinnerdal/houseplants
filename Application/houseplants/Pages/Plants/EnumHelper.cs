using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using HousePlants.Domain.Models.Requirements;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HousePlants.Pages.Plants
{
    public static class EnumHelper
    {
        // Get the value of the description attribute if the   
        // enum has one, otherwise use the value.  
        public static string GetDescription<TEnum>(this TEnum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            if (fi != null)
            {
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
            }

            return value.ToString();
        }

        /// <summary>
        /// Build a select list for an enum
        /// </summary>
        public static MultiSelectList MultiSelectListFor<T>(IEnumerable<T> selectedValues) where T : struct
        {
            Type t = typeof(T);
            return !t.IsEnum ? null
                : new MultiSelectList(BuildSelectListItems(t), selectedValues);
        }

        /// <summary>
        /// Build a select list for an enum with a particular value selected 
        /// </summary>
        //public static SelectList SelectListFor<T>(IEnumerable<Enum> selected) where T : struct
        //{
        //    Type t = typeof(T);
        //    return !t.IsEnum ? null
        //        : new SelectList(BuildSelectListItems(t), "Value", "Text", selected.ToString());
        //}

        private static IEnumerable<SelectListItem> BuildSelectListItems(Type t)
        {
            List<LightRequirement> Selected = new List<LightRequirement>(new [] {LightRequirement.IndirectSunlight, LightRequirement.FullSunlight});
            return Enum.GetValues(t)
                .Cast<Enum>()
                .Select(e => new SelectListItem { Value = e.ToString(), Text = e.GetDescription() });
        }
    }

    //public class LightRequirementModelBinder : IModelBinder
    //{
    //    public Task BindModelAsync(ModelBindingContext bindingContext)
    //    {
    //        if(bindingContext  == null)
    //        {
    //            throw new ArgumentException(nameof(bindingContext));
    //        }

    //        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
    //        if (valueProviderResult == ValueProviderResult.None)
    //        {
    //            return Task.CompletedTask;
    //        }

    //        var modelState = bindingContext.ModelState;
    //        modelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

    //        var metadata = bindingContext.ModelMetadata;
    //        var type = metadata.UnderlyingOrModelType;
    //        try
    //        {
    //            //var value = valueProviderResult.FirstValue;
    //            var rawValues = valueProviderResult.Values.ToArray();
            
    //            if (rawValues != null)
    //            {
    //                LightRequirement result;
    //                if (Enum.TryParse<LightRequirement>(string.Join(",", rawValues), out result))
    //                {
    //                    return result;
    //                }
    //            }
            
    //            //return base.BindModel(controllerContext, bindingContext);

 
    //            object model;
    //            if (string.IsNullOrWhiteSpace(value))
    //            {
    //                model = null;
    //            }
    //            else if (type == typeof(DateTime))
    //            {
    //                var week = value.Split("-W");
    //                model = ISOWeek.ToDateTime(Convert.ToInt32(week[0]), Convert.ToInt32(week[1]), DayOfWeek.Monday);
    //            }
    //            else
    //            {
    //                throw new NotSupportedException();
    //            }
 
    //            if (model == null && !metadata.IsReferenceOrNullableType)
    //            {
    //                modelState.TryAddModelError(
    //                    modelName,
    //                    metadata.ModelBindingMessageProvider.ValueMustNotBeNullAccessor(
    //                        valueProviderResult.ToString()));
    //            }
    //            else
    //            {
    //                bindingContext.Result = ModelBindingResult.Success(model);
    //            }
    //        }
    //        catch (Exception exception)
    //        {
    //            // Conversion failed.
    //            modelState.TryAddModelError(modelName, exception, metadata);
    //        }
    //        return Task.CompletedTask;
    //    }

    //        var rawValues = value.Values.ToArray();
            
    //        if (rawValues != null)
    //        {
    //            NotificationDeliveryType result;
    //            if (Enum.TryParse<NotificationDeliveryType>(string.Join(",", rawValues), out result))
    //            {
    //                return result;
    //            }
    //        }
            
    //        return base.LightRequirementModelBinder(controllerContext, bindingContext);
    //    }
    //}
}