namespace Model
{
    /// <summary>
    /// Represents a bank client that is in legal terms a legal entity.
    /// </summary>
    public class LegalEntity : Client
    {
        /// <summary>
        /// Gets or sets the owner of the legal entity.
        /// </summary>
        public string Owner { get; set; }
    }
}