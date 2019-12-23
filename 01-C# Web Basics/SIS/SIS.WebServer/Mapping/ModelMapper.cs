﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.MvcFramework.Mapping
{
    public static class ModelMapper
    {
        public static TDestination ProjectTo<TDestination>(object origin)
        {
            var destinationInstance = (TDestination) Activator.CreateInstance(typeof(TDestination));

            foreach (var originProperty in origin.GetType().GetProperties())
            {
                string propertyName = originProperty.Name;
                var destinationProperty = destinationInstance.GetType().GetProperty(propertyName);

                if (destinationProperty != null)
                {
                    if (destinationProperty.PropertyType == typeof(string))
                    {
                        destinationProperty.SetValue(destinationInstance, originProperty.GetValue(origin).ToString());
                    }
                    else
                    {
                        destinationProperty.SetValue(destinationInstance, originProperty.GetValue(origin));
                    }
                }
                
            }

            return destinationInstance;
        }
    }
}
