﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RNPC.Core.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class KnowledgeAccuracyParameters {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal KnowledgeAccuracyParameters() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("RNPC.Core.Resources.KnowledgeAccuracyParameters", typeof(KnowledgeAccuracyParameters).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 50.
        /// </summary>
        internal static string AccuracyLowerBound {
            get {
                return ResourceManager.GetString("AccuracyLowerBound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 80.
        /// </summary>
        internal static string AccuracyUpperBound {
            get {
                return ResourceManager.GetString("AccuracyUpperBound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 5.
        /// </summary>
        internal static string PersonalValueCuriosityBonus {
            get {
                return ResourceManager.GetString("PersonalValueCuriosityBonus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 10.
        /// </summary>
        internal static string PersonalValueKnowledgeBonus {
            get {
                return ResourceManager.GetString("PersonalValueKnowledgeBonus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 5.
        /// </summary>
        internal static string PersonalValueTruthBonus {
            get {
                return ResourceManager.GetString("PersonalValueTruthBonus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 83.
        /// </summary>
        internal static string StrongPointsAccuracy {
            get {
                return ResourceManager.GetString("StrongPointsAccuracy", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 48.
        /// </summary>
        internal static string WeakPointsAccuracy {
            get {
                return ResourceManager.GetString("WeakPointsAccuracy", resourceCulture);
            }
        }
    }
}