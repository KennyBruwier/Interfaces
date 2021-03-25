using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    abstract class GeometricFigure : IComparable
    {
        public int Hoogte { get; set; }
        public int Breedte { get; set; }
        public int Oppervlakte { get; private set; }

        public abstract int BerekenOppervlakte();

        public override bool Equals(object obj)
        {
            return obj is GeometricFigure figure &&
                   Hoogte == figure.Hoogte &&
                   Breedte == figure.Breedte &&
                   Oppervlakte == figure.Oppervlakte;
        }

        public int CompareTo(object obj)
        {   
            if (obj is GeometricFigure geometric)
            {
                int compRes = Hoogte.CompareTo(geometric.Hoogte);
                if (compRes == 0) return Breedte.CompareTo(geometric.Breedte);
                return Hoogte.CompareTo(geometric.Hoogte);
            }
            return 0;
        }

        public GeometricFigure(int hoogte, int breedte)
        {
            Hoogte = hoogte;
            Breedte = breedte;
        }

    }
    class Rechthoek : GeometricFigure
    {
        public override int BerekenOppervlakte()
        {
            return Hoogte * Breedte;
        }
        public Rechthoek(int hoogte, int breedte) : base(breedte,hoogte)
        {
            Hoogte = hoogte;
            Breedte = breedte;
        }
    }

    class Vierkant : Rechthoek
    {

        public Vierkant(int hoogte, int breedte = 0) : base(breedte,hoogte)
        {
            Hoogte = hoogte;
            Breedte = hoogte;
        }
    }

    class Driehoek : GeometricFigure
    {
        public override int BerekenOppervlakte()
        {
            return base.Oppervlakte / 2;
        }
        public Driehoek(int hoogte, int breedte) : base(hoogte, breedte)
        {
            Hoogte = hoogte;
            Breedte = breedte;
        }
    }
}
