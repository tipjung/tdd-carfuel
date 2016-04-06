using CarFuel.Models;
using Should;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarFuel.Facts
{
    public class FillUpFacts
    {
        public class General
        {
            [Fact]
            public void NewFillUp()
            {
                // arrange
                FillUp f = new FillUp();
                f.Odometer = 1000;
                f.Liters = 40;

                // act

                // assert
                f.Odometer.ShouldEqual(1000);
                f.Liters.ShouldEqual(40);
            }
        }

        public class KmLProperty
        {
            [Fact]
            public void SingleFillUp()
            {
                var f = new FillUp();
                f.Odometer = 1000;
                f.Liters = 40;

                double? kml = f.KmL;

                kml.ShouldBeNull();
            }

            [Fact]
            public void TwoFillUps()
            {
                var f = new FillUp();
                f.Odometer = 1000;
                f.Liters = 40;

                f.NextFillUp = new FillUp();
                f.NextFillUp.Odometer = 1600;
                f.NextFillUp.Liters = 50;

                double? kml = f.KmL;

                kml.ShouldEqual(12.0);
            }

            [Fact]
            public void ThreeFillUps()
            {
                var f = new FillUp();
                f.Odometer = 1600;
                f.Liters = 50;

                f.NextFillUp = new FillUp();
                f.NextFillUp.Odometer = 2000;
                f.NextFillUp.Liters = 40;

                double? kml = f.KmL;

                f.KmL.ShouldEqual(10.0);
            }
        }
    }
}
