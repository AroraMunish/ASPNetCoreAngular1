using AutoMapper;
using GreatWorld.Models;
using GreatWorld.ViewModel;
using System;
namespace GreatWorld.Mappings
{
    public class FirstNameResolver : IValueResolver<Trip, TripViewModel, String>
    {

        public string Resolve(Trip source, TripViewModel destination, String destMember,
                                ResolutionContext context)
        {
            if (source.FullName == null) return null;

            int idx = source.FullName.IndexOf(" ");
            if (idx != -1)
            {
                return source.FullName.Substring(0, source.FullName.IndexOf(" "));
            }
            else
            {
                return source.FullName;
            }
        }
    }
}
