namespace TeracromModels
{
    public class Usuario
    {
        public int idUsuario { get; set; }
    }
    public class HuellaUsuario
    {
        public byte[] fmdBytes { get; set; }
        public int fmdFormat { get; set; }
        public string fmdVersion { get; set; }
        public int idUsuario { get; set; }
    }
}
