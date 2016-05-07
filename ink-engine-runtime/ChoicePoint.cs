﻿using System.ComponentModel;

namespace Ink.Runtime
{
    /// <summary>
    /// The ChoicePoint represents the point within the Story where
    /// a Choice instance gets generated. The distinction is made
    /// because the text of the Choice can be dynamically generated.
    /// </summary>
	internal class ChoicePoint : Runtime.Object
	{
        internal Path pathOnChoice { get; set; }

        internal Container choiceTarget {
            get {
                return this.ResolvePath (pathOnChoice) as Container;
            }
        }

        internal string pathStringOnChoice {
            get {
                return CompactPathString (pathOnChoice);
            }
            set {
                pathOnChoice = new Path (value);
            }
        }

        internal bool hasCondition { get; set; }
        internal bool hasStartContent { get; set; }
        internal bool hasChoiceOnlyContent { get; set; }
        internal bool onceOnly { get; set; }
        internal bool isInvisibleDefault { get; set; }

        internal int flags {
            get {
                int flags = 0;
                if (hasCondition)         flags |= 1;
                if (!hasStartContent)     flags |= 2; // invert due to default
                if (hasChoiceOnlyContent) flags |= 4;
                if (isInvisibleDefault)   flags |= 8;
                if (!onceOnly)            flags |= 16; // invert due to default
                return flags;
            }
            set {
                hasCondition = (value & 1) > 0;
                hasStartContent = !((value & 2) > 0); // invert due to default
                hasChoiceOnlyContent = (value & 4) > 0;
                isInvisibleDefault = (value & 8) > 0;
                onceOnly = !((value & 16) > 0); // invert due to default
            }
        }

        internal ChoicePoint (bool onceOnly)
		{
            this.onceOnly = onceOnly;
		}

        public ChoicePoint() : this(true) {}

        public override string ToString ()
        {
            int? targetLineNum = DebugLineNumberOfPath (pathOnChoice);
            string targetString = pathOnChoice.ToString ();

            if (targetLineNum != null) {
                targetString = " line " + targetLineNum;
            } 

            return "Choice: -> " + targetString;
        }
	}
}

