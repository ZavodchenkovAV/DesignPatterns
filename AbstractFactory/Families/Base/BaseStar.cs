﻿using System;
using System.Drawing;
using Common;

namespace AbstractFactory.Families.Base
{
    [PatternSourceCode]
    public abstract class BaseStar : BaseElement
    {
        public override string Text
        {
            get { return "Star"; }
        }

        /// <summary>
        /// Return an array of 10 points to be used in a Draw- or FillPolygon method
        /// </summary>
        /// <param name="Orig"> The origin is the middle of the star.</param>
        /// <param name="outerradius">Radius of the surrounding circle.</param>
        /// <param name="innerradius">Radius of the circle for the "inner" points</param>
        /// <returns>Array of 10 PointF structures</returns>
        protected PointF[] Calculate5StarPoints(PointF Orig, float outerradius, float innerradius)
        {
            // Define some variables to avoid as much calculations as possible
            // conversions to radians
            double Ang36 = Math.PI / 5.0;   // 36° x PI/180
            double Ang72 = 2.0 * Ang36;     // 72° x PI/180
            // some sine and cosine values we need
            float Sin36 = (float)Math.Sin(Ang36);
            float Sin72 = (float)Math.Sin(Ang72);
            float Cos36 = (float)Math.Cos(Ang36);
            float Cos72 = (float)Math.Cos(Ang72);
            // Fill array with 10 origin points
            PointF[] pnts = { Orig, Orig, Orig, Orig, Orig, Orig, Orig, Orig, Orig, Orig };
            pnts[0].Y -= outerradius;  // top off the star, or on a clock this is 12:00 or 0:00 hours
            pnts[1].X += innerradius * Sin36; pnts[1].Y -= innerradius * Cos36; // 0:06 hours
            pnts[2].X += outerradius * Sin72; pnts[2].Y -= outerradius * Cos72; // 0:12 hours
            pnts[3].X += innerradius * Sin72; pnts[3].Y += innerradius * Cos72; // 0:18
            pnts[4].X += outerradius * Sin36; pnts[4].Y += outerradius * Cos36; // 0:24 
            // Phew! Glad I got that trig working.
            pnts[5].Y += innerradius;
            // I use the symmetry of the star figure here
            pnts[6].X += pnts[6].X - pnts[4].X; pnts[6].Y = pnts[4].Y;  // mirror point
            pnts[7].X += pnts[7].X - pnts[3].X; pnts[7].Y = pnts[3].Y;  // mirror point
            pnts[8].X += pnts[8].X - pnts[2].X; pnts[8].Y = pnts[2].Y;  // mirror point
            pnts[9].X += pnts[9].X - pnts[1].X; pnts[9].Y = pnts[1].Y;  // mirror point
            return pnts;
        }
    }
}
