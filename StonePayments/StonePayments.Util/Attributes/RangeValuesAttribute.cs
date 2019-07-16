namespace StonePayments.Util.Attributes
{

    /// <summary>
    /// Classe de validação para propriedades de IBaseEntity que estejam dentro do intervalo dos 
    /// valores MinLengthValue e MaxLengthValue.. Se for um valor que precisa ser descriptografado para ser validado,
    /// então IsDecryptValue deve ser true;
    /// </summary>
    public class RangeLengthValuesAttribute: BaseValidatorAttribute
    {
        public bool IsDecryptValue { get; set; } = false;
        public long MinLengthValue { get; set; }
        public long MaxLengthValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isDecryptValue">True se o valor precisar ser descriptografado para validação.</param>
        /// <param name="minLengthValue">Mínimo de dígitos permitidos para o valor.</param>
        /// <param name="maxLengthValue">Mánimo de dígitos permitidos para o valor.</param>
        /// <param name="errorMessage">Mensagem de erro.</param>
        public RangeLengthValuesAttribute(
            bool isDecryptValue, 
            long minLengthValue, 
            long maxLengthValue, 
            string errorMessage) : this(minLengthValue, maxLengthValue, errorMessage)
        {
            this.IsDecryptValue = isDecryptValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="minLengthValue">Mínimo de dígitos permitidos para o valor.</param>
        /// <param name="maxLengthValue">Máximo de dígitos permitidos para o valor.</param>
        /// <param name="errorMessage">Mensagem de erro.</param>
        public RangeLengthValuesAttribute(
            long minLengthValue, 
            long maxLengthValue, 
            string errorMessage): base(errorMessage)
        {
            this.MinLengthValue = minLengthValue;
            this.MaxLengthValue = maxLengthValue;
        }
    }
}
