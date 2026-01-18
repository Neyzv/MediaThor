using System;

namespace MediaThor
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class MediaThorPipePriorityAttribute
        : Attribute
    {
        /// <summary>
        /// Attribute to order the pipelines execution priority.
        /// </summary>
        /// <param name="pipePriority">The pipeline priority order, the lower it is the higher its priority.</param>
        public MediaThorPipePriorityAttribute(uint pipePriority) { }
    }
}

