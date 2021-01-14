﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace X12.Shared.Properties {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("X12.Shared.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to {0} and {1} cannot be converted into a date and time..
        /// </summary>
        internal static string DateTimeParsingError {
            get {
                return ResourceManager.GetString("DateTimeParsingError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No EDI Field Value found for &apos;{0}&apos;.
        /// </summary>
        internal static string EDIFieldNotFound {
            get {
                return ResourceManager.GetString("EDIFieldNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No EDIValue Attribute defined for &apos;{0}&apos;.
        /// </summary>
        internal static string EDIValueNotFound {
            get {
                return ResourceManager.GetString("EDIValueNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Element {0} is required..
        /// </summary>
        internal static string ElementRequiredError {
            get {
                return ResourceManager.GetString("ElementRequiredError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Element {0} cannot contain the value &apos;{1}&apos; with the segment terminator {2}.
        /// </summary>
        internal static string ElementSegmentTerminatorError {
            get {
                return ResourceManager.GetString("ElementSegmentTerminatorError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Element {0} cannot containe the value &apos;{1}&apos; because it must be a positive number between 1 and 999999999..
        /// </summary>
        internal static string ElementValueOutOfRange {
            get {
                return ResourceManager.GetString("ElementValueOutOfRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} Interchange Control Number must be a positive number between 1 and 999999999..
        /// </summary>
        internal static string InterchangeValueOutOfRange {
            get {
                return ResourceManager.GetString("InterchangeValueOutOfRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is not a valid element separator in position 4 of the file..
        /// </summary>
        internal static string InvalidElementSeparatorError {
            get {
                return ResourceManager.GetString("InvalidElementSeparatorError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot find specification for hierarichal loop {0}.
        /// </summary>
        internal static string InvalidHLSpecError {
            get {
                return ResourceManager.GetString("InvalidHLSpecError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is not a valid segment terminator in position 106 of the file..
        /// </summary>
        internal static string InvalidSegmentTerminatorError {
            get {
                return ResourceManager.GetString("InvalidSegmentTerminatorError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} is not a valid subelement separator in position 105 of the file..
        /// </summary>
        internal static string InvalidSubelementSeparatorError {
            get {
                return ResourceManager.GetString("InvalidSubelementSeparatorError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} and {1} in ISA09 and ISA10 cannot be converted into a date and time..
        /// </summary>
        internal static string IsaDateTimeParsingError {
            get {
                return ResourceManager.GetString("IsaDateTimeParsingError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ISA segment and terminator is expected to be exactly 106 characters..
        /// </summary>
        internal static string IsaLengthMismatchError {
            get {
                return ResourceManager.GetString("IsaLengthMismatchError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to First segment must start with ISA.
        /// </summary>
        internal static string IsaSegmentMissingPrefixError {
            get {
                return ResourceManager.GetString("IsaSegmentMissingPrefixError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Value cannot be null or whitespace.
        /// </summary>
        internal static string NullStringArgument {
            get {
                return ResourceManager.GetString("NullStringArgument", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} Transaction does not expect {2} level code value {3} that appears in transaction control number {1}..
        /// </summary>
        internal static string TransactionHLCodeError {
            get {
                return ResourceManager.GetString("TransactionHLCodeError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Hierarchical Loop ID {3} cannot be added to {0} transaction with control number {1} because it already exists..
        /// </summary>
        internal static string UnableToAddHLoop {
            get {
                return ResourceManager.GetString("UnableToAddHLoop", resourceCulture);
            }
        }
    }
}
