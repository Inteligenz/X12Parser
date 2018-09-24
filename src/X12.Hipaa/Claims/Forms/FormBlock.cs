namespace X12.Hipaa.Claims.Forms
{
    using X12.Hipaa.Enums;

    /// <summary>
    /// Represents basic unit of a form
    /// </summary>
    public class FormBlock
    {
        /// <summary>
        /// Gets or sets the spacing between letters within the block
        /// </summary>
        public string LetterSpacing { get; set; }

        /// <summary>
        /// Gets or sets the text alignment within the block
        /// </summary>
        public TextAlign TextAlign { get; set; }

        public decimal Left { get; set; }

        public decimal Top { get; set; }

        public decimal Width { get; set; }

        public decimal Height { get; set; }

        public string Text { get; set; }
    }
}
