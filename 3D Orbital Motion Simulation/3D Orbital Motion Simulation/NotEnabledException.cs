namespace _3D_Orbital_Motion_Simulation
{
    [Serializable]
    internal class NotEnabledException : Exception
    {
        // I decided to make this a custom exception as a future coder may find it useful to be able to handle specifically the tree not being enabled and not a generic exception in the code.
        // It stops exception handling from dealing with 'too much'.
        public NotEnabledException() : base("This tree is currently disabled. Try adding a new reference body.") { }
    }
}
