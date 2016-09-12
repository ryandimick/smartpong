using SmartPong.Models;
using System;
using System.Web.Script.Serialization;

namespace SmartPong.Core
{
    internal class RatingFactory
    {
        private static JavaScriptSerializer _serializer = new JavaScriptSerializer();

        internal static object GetRatingObject(int ratingType, string ratingData)
        {
            switch (ratingType)
            {
                case 1:
                case 2:
                    // matches (singles and doubles)
                case 3:
                case 4:
                case 5:
                    TrueskillRating trueskillRating = _serializer.Deserialize<TrueskillRating>(ratingData);
                    return trueskillRating;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}