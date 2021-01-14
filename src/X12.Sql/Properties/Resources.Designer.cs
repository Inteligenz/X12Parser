﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace X12.Sql.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("X12.Sql.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Could not parse date of birth &apos;{1}&apos; to a DateTime for Entity ID: &apos;{0}&apos;.
        /// </summary>
        internal static string DateOfBirthParsingError {
            get {
                return ResourceManager.GetString("DateOfBirthParsingError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Element {2}{3:00} in position {1} of interchange {0} will be truncated because it exceeds the max length of {4}..
        /// </summary>
        internal static string ElementTruncatedWarning {
            get {
                return ResourceManager.GetString("ElementTruncatedWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FunctionalGroup control number &apos;{0}&apos; could not be parsed. Error: {1}.
        /// </summary>
        internal static string FunctionalGroupControlNumberParsingError {
            get {
                return ResourceManager.GetString("FunctionalGroupControlNumberParsingError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FunctionalGroup date &apos;{0}&apos; and time &apos;{1}&apos; could not be parsed. Error: {2}.
        /// </summary>
        internal static string FunctionalGroupDateTimeParsingError {
            get {
                return ResourceManager.GetString("FunctionalGroupDateTimeParsingError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FunctionalGroup version number &apos;{0}&apos; will be truncated because it exceeds the max length of 12..
        /// </summary>
        internal static string FunctionalGroupVersionNumberTruncatedWarning {
            get {
                return ResourceManager.GetString("FunctionalGroupVersionNumberTruncatedWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to FunctionalIdentifier code &apos;{0}&apos; will be truncated because it exceeds the max length of 2..
        /// </summary>
        internal static string FunctionalIdentifierTruncatedWarning {
            get {
                return ResourceManager.GetString("FunctionalIdentifierTruncatedWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Interchange date &apos;{0}&apos; and time &apos;{1}&apos; could not be parsed. Error: {2}.
        /// </summary>
        internal static string InterchangeDateTimeParsingError {
            get {
                return ResourceManager.GetString("InterchangeDateTimeParsingError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;identityType&apos; must be a value type.
        /// </summary>
        internal static string InvalidIdentityType {
            get {
                return ResourceManager.GetString("InvalidIdentityType", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loop could not be created for interchange &apos;{0}&apos; position &apos;{1}&apos;..
        /// </summary>
        internal static string LoopCreationError {
            get {
                return ResourceManager.GetString("LoopCreationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Segment &apos;{0}&apos; of interchange &apos;{1}&apos; in position &apos;{2}&apos; has already been deleted by &apos;{3}&apos; at &apos;{4}&apos;..
        /// </summary>
        internal static string SegmentAlreadyDeletedError {
            get {
                return ResourceManager.GetString("SegmentAlreadyDeletedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Segment {0} of interchange {1} in position {2} has already been revised by {3} at {4}..
        /// </summary>
        internal static string SegmentAlreadyRevisedError {
            get {
                return ResourceManager.GetString("SegmentAlreadyRevisedError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A segment does not exist for interchange &apos;{0}&apos; at position &apos;{1}&apos;..
        /// </summary>
        internal static string SegmentDoesNotExist {
            get {
                return ResourceManager.GetString("SegmentDoesNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Transaction control number &apos;{0}&apos; will be truncated because it exceeds the max length of 9..
        /// </summary>
        internal static string TransactionControlNumberTruncatedWarning {
            get {
                return ResourceManager.GetString("TransactionControlNumberTruncatedWarning", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Only &apos;Guid&apos;, &apos;Long&apos;, and &apos;Int&apos; identity types are supported.
        /// </summary>
        internal static string UnsupportedIdentityType {
            get {
                return ResourceManager.GetString("UnsupportedIdentityType", resourceCulture);
            }
        }
    }
}
