using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;

namespace RSMTenon.Graph
{
    public abstract class Graph
    {
        private const Int64 EMUS_PER_CENTIMETRE = 360000L;

        public const Int64 LARGE_GRAPH_X = 5486400L;
        public const Int64 LARGE_GRAPH_Y = 3200400L;

        public const Int64 SMALL_GRAPH_X = (Int64)14.85 * EMUS_PER_CENTIMETRE;
        public const Int64 SMALL_GRAPH_Y = (Int64)6.71 * EMUS_PER_CENTIMETRE;

        public const Int64 DEFAULT_GRAPH_X = (Int64)14.90 * EMUS_PER_CENTIMETRE;
        public const Int64 DEFAULT_GRAPH_Y = (Int64)6.80 * EMUS_PER_CENTIMETRE;

        protected const int TITLE_FONT_SIZE = 1100;
        protected const int DEFAULT_FONT_SIZE = 1100;

        public static Int64 Cx { get { return DEFAULT_GRAPH_X; } }
        public static Int64 Cy { get { return DEFAULT_GRAPH_X; } }
    }

}
