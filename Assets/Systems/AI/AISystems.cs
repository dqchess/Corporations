﻿public class AISystems : Feature
{
    public AISystems(Contexts contexts) : base("AI Systems")
    {
        // Start point of all our systems

        Add(new AIProductSystems(contexts));
    }
}
