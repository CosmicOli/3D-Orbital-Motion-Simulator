namespace _3D_Orbital_Motion_Simulation
{
    internal partial class InformationForm : Form
    {
        internal InformationForm()
        {
            InitializeComponent();
        }

        internal void SetLabels(string name, OrbitInformation orbitInformation)
        {
            NameLabel.Text = "Name: "                                            + name;
            LongitudeOfAscendingNodeLabel.Text = "Longitude Of Ascending Node: " + Math.Round(orbitInformation.LongitudeOfAscendingNode, 3) + " Radians";
            ArgumentOfPeriapsisLabel.Text =      "Argument Of Periapsis: "       + Math.Round(orbitInformation.ArgumentOfPeriapsis, 3)      + " Radians";
            LongitudeOfPeriapsisLabel.Text =     "Longitude Of Periapsis: "      + Math.Round(orbitInformation.LongitudeOfPeriapsis, 3)     + " Radians";
            InclinationLabel.Text =              "Inclination: "                 + Math.Round(orbitInformation.Inclination, 3)              + " Radians";
            EccentricityLabel.Text =             "Eccentricity: "                + Math.Round(orbitInformation.Eccentricity, 3)             + " Radians";
            SemiMajorAxisLabel.Text =            "Semi Major Axis: "             + Math.Round(orbitInformation.SemiMajorAxis)               + " Metres";
            SemiMinorAxisLabel.Text =            "Semi Minor Axis: "             + Math.Round(orbitInformation.SemiMinorAxis)               + " Metres";
            PeriapsisLabel.Text =                "Periapsis: "                   + Math.Round(orbitInformation.Periapsis)                   + " Metres";
            ApoapsisLabel.Text =                 "Apoapsis: "                    + Math.Round(orbitInformation.Apoapsis)                    + " Metres";
            AngularMomentumLabel.Text =          "Angular Momentum: "            + Math.Round(orbitInformation.AngularMomentum)             + " KgM^2/s";
            TotalEnergyLabel.Text =              "Total Energy: "                + Math.Round(orbitInformation.TotalEnergy)                 + " Joules";
            OrbitalPeriodLabel.Text =            "Orbital Period: "              + Math.Round(orbitInformation.OrbitalPeriod)               + " Seconds" ;
            HillSphereRadiusLabel.Text =         "Hill Sphere Radius: "          + Math.Round(orbitInformation.HillSphereRadius)            + " Metres";
        }
    }
}
