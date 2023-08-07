using FCRA.Common;
using FCRA.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FCRA.Repository
{
    public static class ModelMapper
    {
        private static TTarget MapDTO<TSource, TTarget>(this TSource source, TTarget target, bool skipMapToAttribute = false)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

            var srcFields = (from PropertyInfo aProp in typeof(TSource).GetProperties(flags)
                             where aProp.CanRead
                             select new
                             {
                                 Name = aProp.Name,
                                 Type = Nullable.GetUnderlyingType(aProp.PropertyType) ?? aProp.PropertyType
                             }).ToList();
            var trgFields = (from PropertyInfo aProp in typeof(TSource).GetProperties(flags)
                             where aProp.CanWrite   //check if prop is writeable
                             select new
                             {
                                 Name = aProp.Name,
                                 Type = Nullable.GetUnderlyingType(aProp.PropertyType) ?? aProp.PropertyType
                             }).ToList();
            var commonFields = srcFields.Intersect(trgFields).ToList();
            foreach (var aField in commonFields)
            {
                var sourceProperty = source?.GetType().GetProperty(aField.Name);
                if (!skipMapToAttribute)
                    if (sourceProperty == null || !Attribute.IsDefined(sourceProperty, typeof(MapToDTOAttribute)))
                    {
                        continue;
                    }
                var value = sourceProperty?.GetValue(source, null);
                PropertyInfo? propertyInfo = target!.GetType().GetProperty(aField.Name);
                if (propertyInfo != null && value != null && propertyInfo.PropertyType == typeof(UserViewModel))
                    propertyInfo?.SetValue(target, Map<FCRA.Models.Account.UserMaster, UserViewModel>((FCRA.Models.Account.UserMaster)value), null);
                else
                    propertyInfo?.SetValue(target, value, null);
            }
            return target;
        }
        private static TTarget MapTo<TSource, TTarget>(this TSource source, TTarget target)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;

            var srcFields = (from PropertyInfo aProp in typeof(TSource).GetProperties(flags)
                             where aProp.CanRead
                             select new
                             {
                                 Name = aProp.Name,
                                 Type = Nullable.GetUnderlyingType(aProp.PropertyType) ?? aProp.PropertyType
                             }).ToList();
            var trgFields = (from PropertyInfo aProp in typeof(TSource).GetProperties(flags)
                             where aProp.CanWrite   //check if prop is writeable
                             select new
                             {
                                 Name = aProp.Name,
                                 Type = Nullable.GetUnderlyingType(aProp.PropertyType) ?? aProp.PropertyType
                             }).ToList();
            var commonFields = srcFields.Intersect(trgFields).ToList();
            foreach (var aField in commonFields)
            {
                var value = source?.GetType().GetProperty(aField.Name)?.GetValue(source, null);
                PropertyInfo? propertyInfo = target!.GetType().GetProperty(aField.Name);
                if (propertyInfo != null && value != null && propertyInfo.PropertyType == typeof(UserViewModel))
                    propertyInfo?.SetValue(target, Map<FCRA.Models.Account.UserMaster, UserViewModel>((FCRA.Models.Account.UserMaster)value), null);
                else if (propertyInfo != null && value != null)
                    propertyInfo?.SetValue(target, value, null);
            }
            return target;
        }

        public static TTarget Map<TSource, TTarget>(this TSource source) where TTarget : new()
        {
            return source.MapTo(new TTarget());
        }

        public static List<TTarget> Map<TSource, TTarget>(this List<TSource> source) where TTarget : new()
        {
            var target = new List<TTarget>();
            foreach (var a in source)
            {
                target.Add(a.MapTo(new TTarget()));
            }
            return target;
        }

        public static TTarget MapToDTO<TSource, TTarget>(this TSource source, TTarget target, bool skipMapToAttribute = false) where TTarget : class
        {
            return source.MapDTO(target, skipMapToAttribute);
        }
    }
}
