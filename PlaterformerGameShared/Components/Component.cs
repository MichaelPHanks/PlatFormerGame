using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerGameShared.Components
{
    /// <summary>
    /// Abstract base class for components in an Entity Component System architecture.
    /// <para>This class contains no members. It only exists to indicate that a derived class is a type of ECS component.</para>
    /// <para>In an ECS system, a component should contain only data, and no behavior other than basic accessors for the data.</para>
    /// </summary>
    public abstract class Component
    {
        /*
         * Intentionally empty class
         */
    }
}
